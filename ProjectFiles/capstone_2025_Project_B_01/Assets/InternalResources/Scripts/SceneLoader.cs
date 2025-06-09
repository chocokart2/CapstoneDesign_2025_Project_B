using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void SceneMain()
    {
        SceneManager.LoadScene("LobbyScene_Final");
    }

    public void SceneIntro()
    {
        SceneManager.LoadScene("IntroScene");
    }

    public void SceneLoad()
    {
        SceneManager.LoadScene("LoadScene");
    }

    public void SceneStage1()
    {
        SceneManager.LoadScene("GeunohTest1");
    }

    public void SceneStage2()
    {
        SceneManager.LoadScene("GameScene2");
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
