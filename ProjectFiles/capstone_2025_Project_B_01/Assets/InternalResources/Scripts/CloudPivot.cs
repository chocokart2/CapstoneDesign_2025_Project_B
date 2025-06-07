using System.Collections;
using System.Xml.Serialization;
using UnityEngine;

public class CloudPivot : MonoBehaviour
{
    [SerializeField] GameObject prefabCloud;
    [SerializeField] float SpawnPerMinute;
    [SerializeField] float spawnSquareRange;
    [SerializeField] int startSpawnCount;
    float time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < startSpawnCount; i++)
        {
            Spawn();
        }

        time = 60f / SpawnPerMinute;

        IEnumerator mCoroutine()
        {
            while (true)
            {
                Spawn();

                yield return new WaitForSeconds(time);
            }
        }

        StartCoroutine(mCoroutine());
    }

    void Spawn()
    {
        float x = Random.Range(-spawnSquareRange, spawnSquareRange); 
        float y = Random.Range(-spawnSquareRange, spawnSquareRange);

        GameObject mCloud = Instantiate(prefabCloud, new Vector3(x, transform.position.y, y), Quaternion.identity);
        mCloud.GetComponent<Cloud>().Init(new Vector3(x, transform.position.y, y));
    }
}
