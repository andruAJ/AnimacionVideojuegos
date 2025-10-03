using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class RecoilCameraKick : MonoBehaviour
{
    [SerializeField] private CinemachineCamera[] cameras;

    CinemachineBasicMultiChannelPerlin[] perlins;

    float[] baseamplitude;

    private void Awake()
    {
        perlins = new CinemachineBasicMultiChannelPerlin[cameras.Length];

        baseamplitude = new float[cameras.Length];
        for (int i = 0; i < cameras.Length; i++)
        {
            perlins[i] = cameras[i].GetComponent<CinemachineBasicMultiChannelPerlin>();
            if (perlins[i]) baseamplitude[i] = perlins[i].AmplitudeGain;
        }
    }

    public void Kick(float strenght, float peak, float recover)
    { 
        StopAllCoroutines();
        StartCoroutine(KickRoutine(strenght, peak, recover));

    }
    IEnumerator KickRoutine(float strenght, float peak, float recover)
    {
        float t = 0;
        while (t < peak)
        {
            t += Time.deltaTime;
            float k = t/ Mathf.Max(0.001f, peak);
            for (int i = 0; i < perlins.Length; i++)
            {
                if (perlins[i]) perlins[i].AmplitudeGain = Mathf.Lerp(baseamplitude[i], baseamplitude[i] + strenght, k);
            }
            yield return null;
        }

        t = 0f;
        while (t < recover)
        {
            t += Time.deltaTime;
            float k = t / Mathf.Max(0.001f, recover);
            for (int i = 0; i < perlins.Length; i++)
            {
                if (perlins[i]) perlins[i].AmplitudeGain = Mathf.Lerp(strenght, baseamplitude[i] + strenght, k);
            }
            yield return null;
        }

        for (int i = 0; i < perlins.Length; i++)
        {
            if (perlins[i]) perlins[i].AmplitudeGain = baseamplitude[i];
        }
    }
}
