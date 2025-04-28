using UnityEngine;

public class UiManager : MonoBehaviour
{
    static public UiManager instance;

    [SerializeField] GameObject winText;
    [SerializeField] GameObject pauseImage;

    public void ShowWin()
    {
        winText.SetActive(true);
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        winText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
