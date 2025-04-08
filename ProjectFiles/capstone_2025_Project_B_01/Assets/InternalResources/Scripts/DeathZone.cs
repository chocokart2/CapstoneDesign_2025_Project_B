using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (PlayerController.IsPlayer(other) == false) return;

        Debug.Log($">> DeathZone : �÷��̾� ���.");

        IEnumerator m_coroutine()
        {
            PlayerController.instance.HideImage();
            PlayerController.instance.KillPlayer();
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        StartCoroutine(m_coroutine());
    }
}
