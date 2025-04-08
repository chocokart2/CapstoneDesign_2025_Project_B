using System.Collections;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    bool isPlayerThere = false;
    [SerializeField] float windPercentage;
    [SerializeField] float windForce;
    [SerializeField] float windTime;

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerController.IsPlayer(other)) isPlayerThere = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (PlayerController.IsPlayer(other)) isPlayerThere = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(WindCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WindCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);

            if (isPlayerThere == false) continue;
            float random = Random.Range(0, 1f);
            float direction = (Random.Range(0f, 1f) < 0.5f) ? 1 : -1;
            if (random <= windPercentage)
            {
                Debug.Log($">> 바람 {windForce * direction} 만큼 불었음");
                // 바람 불게 함
                PlayerController.instance.ApplyWind(windForce * direction, windTime);
            }
        }
    }
}
