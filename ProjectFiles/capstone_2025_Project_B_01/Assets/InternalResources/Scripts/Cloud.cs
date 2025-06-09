using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Cloud : MonoBehaviour
{
    [SerializeField] float maxLength;
    [SerializeField] float minLength;
    [SerializeField] float maxTime;
    [SerializeField] float minTime;
    [SerializeField] UnityEngine clould1;
    [SerializeField] Image clould2;
    [SerializeField] Image clould3;
    [SerializeField] Image clould4;
    [SerializeField] Image sr;


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
        sr.sprite = 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
