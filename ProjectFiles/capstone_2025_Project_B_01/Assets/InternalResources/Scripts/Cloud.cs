using System.Collections;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] float maxLength;
    [SerializeField] float minLength;
    [SerializeField] float maxTime;
    [SerializeField] float minTime;

    float length;
    float time;
    float elapsedTime = 0.0f;
    Vector3 startPos;
    Vector3 endPos;

    public void Init(Vector3 _startPos)
    {
        length = Random.Range(minLength, maxLength);
        time = Random.Range(maxTime, minTime);
        startPos = _startPos;
        endPos = startPos + new Vector3(length, 0, 0);

        IEnumerator m_Coroutine()
        {
            while (elapsedTime < time)
            {
                elapsedTime += Time.deltaTime;
                transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / time);
                yield return null;
            }
            Destroy(gameObject);
        }

        StartCoroutine(m_Coroutine());
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
