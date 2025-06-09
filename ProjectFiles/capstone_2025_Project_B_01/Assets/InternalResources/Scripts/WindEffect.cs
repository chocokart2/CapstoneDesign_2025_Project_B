using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class WindEffect : MonoBehaviour
{
    public static List<WindEffect> instances;
    public static float windTime = 1.0f;
    public Coroutine currentWindCoroutine;
    public ParticleSystem effectLeftWind;
    public ParticleSystem effectRightWind;
    public ParticleSystem.EmissionModule effectLeftWindEmission;
    public ParticleSystem.EmissionModule effectRightWindEmission;

    static WindEffect()
    {
        instances = new List<WindEffect>();
    }

    public static void LeftWind()
    {
        IEnumerator mCoroutine(WindEffect component)
        {
            component.effectLeftWindEmission.rateOverTime = 1000;
            yield return new WaitForSeconds(windTime);
            component.effectLeftWindEmission.rateOverTime = 0;
        }

        foreach (WindEffect one in instances)
        {
            if (one.currentWindCoroutine != null) one.Stop();

            one.StartCoroutine(mCoroutine(one));
        }
    }

    public static void RightWind()
    {
        IEnumerator mCoroutine(WindEffect component)
        {
            component.effectRightWindEmission.rateOverTime = 1000;
            yield return new WaitForSeconds(windTime);
            component.effectRightWindEmission.rateOverTime = 0;
        }

        foreach (WindEffect one in instances)
        {
            if (one.currentWindCoroutine != null) one.Stop();

            one.StartCoroutine(mCoroutine(one));
        }
    }


    private void Awake()
    {
        if (instances != null)
        {
            // 여기 정적 생성자 호출을 어떻게 합니까?
            instances = new List<WindEffect>();
        }

        instances.Add(this);
        effectLeftWindEmission = effectLeftWind.emission;
        effectRightWindEmission = effectRightWind.emission;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Stop()
    {
        StopCoroutine(currentWindCoroutine);
        effectLeftWindEmission.rateOverTime = 0;
        effectRightWindEmission.rateOverTime = 0;
        //effectLeftWind.SetActive(false);
        //effectRightWind.SetActive(false);
    }
}
