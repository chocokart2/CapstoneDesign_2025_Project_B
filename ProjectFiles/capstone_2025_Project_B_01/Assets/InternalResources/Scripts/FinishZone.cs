using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (PlayerController.IsPlayer(other) == false) return;

        UiManager.instance.ShowWin();

        IEnumerator Coroutine()
        {
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("GeunohTest2");
        }

        StartCoroutine(Coroutine());
    }
}
