using UnityEngine;

public class UiManager : MonoBehaviour
{
    static public UiManager instance;

    [SerializeField] GameObject winText;
    [SerializeField] GameObject pauseImage;
    [SerializeField] GameObject saveImage;
    [SerializeField] GameObject tutorial1Image;
    [SerializeField] GameObject tutorial2Image;

    static public void ExitPause()
    {
        instance.pauseImage.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public bool HasPaused()
    {
        return
            tutorial1Image.activeInHierarchy ||
            tutorial2Image.activeInHierarchy ||
            pauseImage.activeInHierarchy;
    }

    public void ShowWin()
    {
        winText.SetActive(true);
    }

    public void ShowTutorial1()
    {
        tutorial1Image.SetActive(true);
        Time.timeScale = 0.01f;
    }





    //public void ShowTutorial2()
    //{
    //    tutorial2Image.SetActive(true);
    //    Time.timeScale = 0.01f;
    //}

    private void ClearTutorial()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (tutorial1Image.activeInHierarchy)
            {
                tutorial1Image.SetActive(false);
                tutorial2Image.SetActive(true);
            }
            else
            {
                tutorial2Image.SetActive(false);
                Time.timeScale = 1.0f;
            }

            //tutorial1Image.SetActive(false);
            //tutorial2Image.SetActive(false);
            //Time.timeScale = 1.0f;
        }
    }

    private void PressPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) == false) return;
        if (tutorial1Image.activeInHierarchy) return;
        if (tutorial2Image.activeInHierarchy) return;
        pauseImage.SetActive(true);
        Time.timeScale = 0.01f;
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowTutorial1();
        winText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ClearTutorial();
        PressPause();
    }


    

}
