using System.Collections;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] float maxLength;
    [SerializeField] float minLength;
    [SerializeField] float maxTime;
    [SerializeField] float minTime;
    [SerializeField] Sprite Image1;
    [SerializeField] Sprite Image2;
    [SerializeField] Sprite Image3;
    [SerializeField] Sprite Image4;
    [SerializeField] SpriteRenderer sr;


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
        switch(Random.Range(0, 4))
        {
            case 0: sr.sprite = Image1;
                break;
            case 1: 
                sr.sprite = Image2;
                break;
            case 2:
                sr.sprite = Image3;
                break;
            case 3:
                sr.sprite = Image4;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
