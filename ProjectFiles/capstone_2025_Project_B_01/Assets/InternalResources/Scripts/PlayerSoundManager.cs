using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Audio Sources")]
    public AudioSource effectSource; // 점프용 OneShot
    public AudioSource walkSource;   // 걷기용 Loop 재생
    public AudioSource fallingSource; // 두루마리 떨어지는 Loop 재생

    [Header("Sound Clips")]
    public AudioClip jumpClip;
    public AudioClip walkClip;
    public AudioClip fallingClip;

    // 점프 사운드  
    public void PlayJump()
    {
        if (jumpClip != null && effectSource != null)
        {
            effectSource.PlayOneShot(jumpClip);
        }
    }

    // 걷기 사운드 시작
    public void PlayWalk()
    {
        if (!walkSource.isPlaying && walkClip != null)
        {
            walkSource.clip = walkClip;
            walkSource.loop = true;
            walkSource.Play();
        }
    }

    // 걷기 사운드 중지
    public void StopWalk()
    {
        if (walkSource.isPlaying)
        {
            walkSource.Stop();
        }
    }

    public void PlayFalling()
    {
        if (fallingClip != null && fallingSource != null)
        {
            fallingSource.PlayOneShot(fallingClip);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
