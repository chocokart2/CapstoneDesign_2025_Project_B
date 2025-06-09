using System.Collections;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    static public Scroll instance;
    static public bool CanPick => Time.time > instance.restrictPickEndTime;

    [SerializeField] float angleDivide = 4.0f;
    [SerializeField] float dropUpForce = 3f;
    [SerializeField] float dropSideForce = 1f;
    [SerializeField] float particleTime;
    [SerializeField] float restrictPickTime;
    [SerializeField] AudioClip Falling;  // 두루마리가 떨어질 때 재생할 효과음
    AudioSource audioSource;              // 효과음을 재생할 오디오 소스
    PlayerSoundManager soundManager; // 효과음 불러오기
    float particleEndTime = 0f;
    float restrictPickEndTime = 0f;
    bool isHolding = true;

    ParticleSystem.MinMaxCurve emissionPrevRate;
    // this gameobject component

    Rigidbody rigidBody;

    // other gameobject;
    [SerializeField] ParticleSystem particle;
    [SerializeField] GameObject quad;
    Transform playerPosition;
    PlayerController player;
    ParticleSystem.EmissionModule emission;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.useGravity = false;

        emission = particle.emission;
        emissionPrevRate = emission.rateOverTime;
        emission.rateOverTime = 0f;

        player = PlayerController.instance;
        playerPosition = player.transform;
        player.scroll = this;

        soundManager = FindFirstObjectByType<PlayerSoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.IsHoldingScroll == false && transform.position.y < -100)
        {
            transform.position = player.transform.position + new Vector3(0, 3, 0);
        }


        if (player.IsHoldingScroll)
        {
            float angle = player.ScrollBalance * Mathf.PI / angleDivide;

            float posX = Mathf.Sin(angle);
            float posY = Mathf.Cos(angle);

            transform.position = playerPosition.position + new Vector3(posX, posY, 0);
            transform.eulerAngles = new Vector3(0, 0, -angle * Mathf.Rad2Deg);
        }
        if (Time.time < particleEndTime)
        {
            emission.rateOverTime = emissionPrevRate;
        }
        else
        {
            emission.rateOverTime = 0.0f;
        }

        if (CanPick)
        {

        }
        else
        {

        }

        //Debug.Log($">> 밸런스 : {player.ScrollBalance}");
    }

    public void ChangeHoldingState(bool isOnHead)
    {

        // 리지드바디
        rigidBody.useGravity = !isOnHead;
        if (isOnHead)
        {
            rigidBody.constraints |= RigidbodyConstraints.FreezeRotationZ;
        }
        else
        {
            Debug.Log($">> 놓쳤습니다");

            rigidBody.constraints &= ~RigidbodyConstraints.FreezeRotationZ;
            rigidBody.linearVelocity = Vector3.zero;
            rigidBody.AddForce(Vector3.up * dropUpForce + (transform.position - PlayerController.instance.transform.position).normalized * dropSideForce, ForceMode.VelocityChange);
            restrictPickEndTime = Time.time + restrictPickTime;

            StartCoroutine(Coroutine());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.name == "Player") return;

        if (!player.IsHoldingScroll)
        {
            particleEndTime = Time.time + particleTime;

            if (soundManager != null)
            {
                soundManager.PlayFalling();
            }
        }
    }

    IEnumerator Coroutine()
    {
        while (CanPick == false)
        {
            quad.SetActive(false);
            yield return new WaitForSeconds(0.05f);
            quad.SetActive(true);
            yield return new WaitForSeconds(0.05f);
        }
        quad.SetActive(true);
    }
}
