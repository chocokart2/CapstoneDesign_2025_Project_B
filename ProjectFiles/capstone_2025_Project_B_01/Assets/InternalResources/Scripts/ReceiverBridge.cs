using UnityEngine;

public class ReceiverBridge : Receiver
{
    public override void DoSomething()
    {
        gameObject.SetActive(true);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);
    }
}
