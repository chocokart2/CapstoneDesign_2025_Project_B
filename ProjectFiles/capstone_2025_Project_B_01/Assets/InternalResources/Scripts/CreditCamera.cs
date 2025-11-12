using System.Collections;
using UnityEngine;

public class CreditCamera : MonoBehaviour
{
    public float time = 10f;
    public float t = 0f;
    public Transform startPos;
    public Transform endPos;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(End());
    }

    IEnumerator End()
    {
        while (t < time)
        {
            t += Time.deltaTime;
            transform.position =
                Vector3.Lerp(startPos.position, endPos.position, t / time);
            yield return null;
        }
    }
}
