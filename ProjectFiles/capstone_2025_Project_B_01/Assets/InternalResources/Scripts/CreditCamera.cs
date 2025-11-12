using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class CreditCamera : MonoBehaviour
{
    public float time = 10f;
    public float t = 0f;
    public Transform startPos;
    public Transform endPos;
    public List<string> lines = new List<string>();
    public List<float> linesKey = new List<float>();
    public TextMeshProUGUI textMeshPro;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(End());
    }

    IEnumerator End()
    {
        //Debug.Log($">> {lines.Count}");
        for (int index = 0; index < lines.Count; ++index)
        {
            linesKey.Add((time / lines.Count) * index);
        }
        linesKey.Add(time);

        while (t < time)
        {
            t += Time.deltaTime;
            transform.position =
                Vector3.Lerp(startPos.position, endPos.position, t / time);

            for (int index = 1; index <= lines.Count; ++index)
            {
                //Debug.Log($">> {t} + {linesKey[index]}");

                if (t < linesKey[index])
                {
                    textMeshPro.text = lines[index - 1];
                    break;
                }
            }

            yield return null;
        }

        new WaitForSeconds(1f);

        SceneManager.LoadScene("LobbyScene_Final");
    }
}
