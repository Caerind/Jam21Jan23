using Cinemachine;
using UnityEngine;

public class CinemachineCameraShake : Singleton<CinemachineCameraShake>
{
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin noise;

    private float startingIntensity;
    private float timer;
    private float timerTotal;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake(float intensity, float time)
    {
        noise.m_AmplitudeGain = intensity;
        startingIntensity = intensity;
        timer = time;
        timerTotal = time;
    }

    private void Update()
    {
        if (timer > 0.0f)
        {
            timer -= Time.deltaTime;
            noise.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0.0f, 1 - (timer / timerTotal));
        }
    }
}
