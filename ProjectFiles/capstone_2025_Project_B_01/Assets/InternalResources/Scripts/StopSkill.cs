using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StopSkill : MonoBehaviour
{
    public bool IsPressed = false;
    public bool IsAvailable
    {
        get => isAvailable;
        private set => isAvailable = value;
    } // 스킬 사용 가능 여부 체크
    private bool isAvailable = true;
    public float time;
    [SerializeField] float maxTime;
    [SerializeField] Image image;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI textUp;
    [SerializeField] TextMeshProUGUI textDown;
    bool isReachedEnd = false; // 만약에 이게 true면 -> time가 maxTime에 도달할 때까지 스킬 사용 불가

    private void Awake()
    {
        time = maxTime;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isReachedEnd)
        {
            Color imageColor = image.color;
            imageColor = new Color(0.1f, 0.1f, 0.1f, 0.4f);
            image.color = imageColor;
            Color txt1Color = textUp.color;
            txt1Color = new Color(0.3f, 0.3f, 0.3f, 0.4f);
            textUp.color = txt1Color;
            Color txt2Color = textDown.color;
            txt2Color = new Color(0.1f, 0.1f, 0.1f, 0.4f);
            textDown.color = txt2Color;
        }
        else
        {
            image.color = Color.white;
            textUp.color = Color.white;
            textDown.color = Color.black;
        }

        slider.value = 0.2f + (time / maxTime) * 0.8f;

        if (time > maxTime)
        {
            isReachedEnd = false;
            time = maxTime;
        }

        if (isReachedEnd)
        {
            // 스킬 다 채울 때까지 대기
            time += Time.deltaTime;
            isAvailable = false;

            IsPressed = false;
            return;
        }
        else
        {
            // 여기서는 스킬 사용을 허락해 줌
            isAvailable = true;

            if (Input.GetKey(KeyCode.DownArrow))
            {
                time -= Time.deltaTime;
            }
            else
            {
                time += Time.deltaTime;
            }

            IsPressed = Input.GetKey(KeyCode.DownArrow);

            if (time < 0f)
            {
                
                isAvailable = false;
                isReachedEnd = true;
            }
        }
    }
}
