using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public bool IsClimbing = false;
    public bool IsHoldingScroll { get => isHoldingScroll; }
    [SerializeField] float jumpForce = 20.0f;
    [SerializeField] float climbForce = 5.0f;
    [SerializeField] float moveBalanceDefault = 0.1f;
    [SerializeField] float moveBalanceBonus = 0.05f;
    [SerializeField] float scrollSpeed = 1.3f;
    bool isHoldingScroll = true;
    bool isDead = false;
    bool isLookingLeft = false;
    bool isCurrentGround = false;
    float speed = 3.0f;
    float jumpTerm = 0.5f;
    float nextJumpTime;
    float staticAmplyfyBalance = 0.2f;
    public float ScrollBalance { get => scrollBalance; }
    float scrollBalance = 0.0f;
    string animatorParameterNameOnGround = "OnGround";
    string animatorParameterNameIsMoving = "IsMoving";
    string animatorParameterNamePressJump = "PressJump";
    List<Coroutine> windCoroutines = new List<Coroutine>();
    ParticleSystem.MinMaxCurve emissionPrevRate;

    // gameObject Component
    Rigidbody rigidBody;
    Animator animator;
    SpriteRenderer spriteRenderer;

    // related Gameobject
    public Scroll scroll;
    public GameObject Warning;
    public GameObject quad;
    public GameObject particles;
    private MeshRenderer warningRenderer;
    private MeshRenderer quadRenderer;

    // related Gameobject Component
    ParticleSystem childParticleSystem;
    ParticleSystem.EmissionModule emission;

    public static bool IsPlayer(Collider other) => other.gameObject.name == "Player";

    public void ApplyWind(float wind, float time)
    {
        IEnumerator m_Coroutine()
        {
            float endTime = Time.time + time;
            while (Time.time < endTime)
            {
                scrollBalance += wind * Time.deltaTime;
                yield return null;
            }
        }
        windCoroutines.Add(StartCoroutine(m_Coroutine()));
    }

    public void HideImage()
    {
        warningRenderer.enabled = false;
        //quadRenderer.enabled = false;
        spriteRenderer.enabled = false;
    }

    public void SetClimbing(bool value)
    {
        IsClimbing = value;
        rigidBody.useGravity = !value;
    }

    public void DropScroll()
    {
        isHoldingScroll = false;
        scrollBalance = 0;
        scroll.ChangeHoldingState(false);
        foreach (Coroutine one in windCoroutines)
        {
            StopCoroutine(one);
        }
    }

    public void KillPlayer()
    {
        DropScroll();
        isDead = true;
    }

    private void Awake()
    {
        instance = this;

        nextJumpTime = Time.time;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        warningRenderer = Warning.GetComponent<MeshRenderer>();
        quadRenderer = quad.GetComponent<MeshRenderer>();

        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        animator = transform.GetChild(0).GetComponent<Animator>();

        childParticleSystem = particles.GetComponent<ParticleSystem>();
        emission = childParticleSystem.emission;
        emissionPrevRate = emission.rateOverTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (UiManager.instance.HasPaused()) return;

        UpdateGroundState();
        Move();
        Jump();
        Climb();
        BalanceScroll();
        UpdateBalance();
    }

    void UpdateGroundState()
    {
        RaycastHit hit;
        Vector3 start = transform.position - new Vector3(0, 0.3f, 0);
        isCurrentGround = Physics.Raycast(start, Vector3.down, out hit, 0.4f);

        animator.SetBool(animatorParameterNameOnGround, isCurrentGround);
    }

    void Move()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.A)) direction += Vector3.left;
        if (Input.GetKey(KeyCode.D)) direction += Vector3.right;
        
        if (direction.x > 0.1f) spriteRenderer.flipX = false;
        if (direction.x < -0.1f) spriteRenderer.flipX = true;

        animator.SetBool(animatorParameterNameIsMoving, Mathf.Abs(direction.x) > 0.1f);

        transform.Translate(direction * speed * Time.deltaTime);
        
        if ((isCurrentGround == false) || Mathf.Abs(direction.x) < 0.1f)
        {
            emission.rateOverTime = 0f;
            return;
        }
        emission.rateOverTime = emissionPrevRate;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) == false)
        {
            return;
        }
        if (nextJumpTime > Time.time)
        {
            return;
        }
        if (isCurrentGround == false)
        {
            return;
        }

        rigidBody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.VelocityChange);
        nextJumpTime = Time.time + jumpTerm;
        animator.SetTrigger(animatorParameterNamePressJump);
    }

    void Climb()
    {
        if (IsClimbing == false) return;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, climbForce * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -climbForce * Time.deltaTime, 0);
        }
    }

    void BalanceScroll()
    {
        if (isHoldingScroll == false) return;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            scrollBalance *= (scrollBalance > 0) ? (1.0f + moveBalanceBonus) * (1.0f + Time.deltaTime) : (1.0f - moveBalanceBonus) * (1.0f - Time.deltaTime);
            scrollBalance += moveBalanceDefault * Time.deltaTime * scrollSpeed;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            scrollBalance *= (scrollBalance < 0) ? (1.0f + moveBalanceBonus) * (1.0f + Time.deltaTime) : (1.0f - moveBalanceBonus) * (1.0f - Time.deltaTime);
            scrollBalance += -moveBalanceDefault * Time.deltaTime;
        }
    }

    void UpdateBalance()
    {
        float delta = staticAmplyfyBalance * Time.deltaTime * scrollBalance * scrollSpeed;
        scrollBalance += delta;

        // Debug.Log($"변화량 : {delta} \n결과 : {scrollBalance}");
        Warning.SetActive(Mathf.Abs(scrollBalance) > 0.5f && Mathf.Abs(scrollBalance) < 1f);
        // 만약 너무 벗어나면 아웃
        if (scrollBalance > 1 || scrollBalance < -1)
        {
            DropScroll();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isDead) return;

        if (collision.gameObject.name == "ScrollHitBox" && (isHoldingScroll == false))
        {
            isHoldingScroll = true;
            scrollBalance = 0;
            scroll.ChangeHoldingState(true);
        }
    }
}
