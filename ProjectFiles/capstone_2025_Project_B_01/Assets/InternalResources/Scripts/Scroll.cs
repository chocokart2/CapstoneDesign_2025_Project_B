using UnityEngine;

public class Scroll : MonoBehaviour
{
    static public Scroll instance;

    [SerializeField] float angleDivide = 4.0f;
    [SerializeField] float dropUpForce = 3f;
    [SerializeField] float dropSideForce = 1f;
    // this gameobject component

    Rigidbody rigidBody;

    // other gameobject;

    Transform playerPosition;
    PlayerController player;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.useGravity = false;

        player = PlayerController.instance;
        playerPosition = player.transform;
        player.scroll = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.IsHoldingScroll)
        {
            float angle = player.ScrollBalance * Mathf.PI / angleDivide;

            float posX = Mathf.Sin(angle);
            float posY = Mathf.Cos(angle);

            transform.position = playerPosition.position + new Vector3(posX, posY, 0);
            transform.eulerAngles = new Vector3(0, 0, -angle * Mathf.Rad2Deg);
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
        }
    }
}
