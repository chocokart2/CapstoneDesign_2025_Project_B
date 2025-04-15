using UnityEngine;

public class Sender : MonoBehaviour
{
    [SerializeField] Receiver[] targets;
    private bool isOverlaped = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && isOverlaped) SendSign();
    }

    void SendSign()
    {
        foreach (Receiver one in targets) one.DoSomething();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerController.IsPlayer(other)) isOverlaped = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (PlayerController.IsPlayer(other)) isOverlaped = false;
    }
}
