using UnityEngine;

public class ReceiverDoor : Receiver
{
    public override void DoSomething()
    {
        gameObject.SetActive(false);
    }

    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
