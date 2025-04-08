using System.Collections;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public Transform start;
    public Transform end;
    float term = 3.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(m_coroutine());
    }

    IEnumerator m_coroutine()
    {
        float startTime;
        float nextTime;
        float i = 0;
        while (true)
        {
            startTime = Time.time;
            nextTime = Time.time + term;
            transform.position = start.position;
            while (Time.time < nextTime)
            {
                i = (Time.time - startTime) / term;

                transform.position = Vector3.Lerp(start.position, end.position, i);
                yield return null;
            }
            transform.position = end.position;
            startTime = Time.time;
            nextTime = Time.time + term;
            while (Time.time < nextTime)
            {
                i = (Time.time - startTime) / term;

                transform.position = Vector3.Lerp(end.position, start.position, i);
                yield return null;
            }

        }
    }
}
