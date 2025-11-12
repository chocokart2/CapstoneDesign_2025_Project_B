using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishZone : MonoBehaviour
{
    public string scene;

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerController.IsPlayer(other) == false) return;
        if (PlayerController.instance.IsHoldingScroll == false) return;

        UiManager.instance.ShowWin();

        IEnumerator Coroutine()
        {
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(scene);
        }

        StartCoroutine(Coroutine());
    }
}
