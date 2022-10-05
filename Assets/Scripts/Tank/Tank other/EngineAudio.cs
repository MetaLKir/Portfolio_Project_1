using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineAudio : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] float minVolume = 0.1f, maxVolume = 0.3f, volumeChange = 0.05f, currentVolume;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        currentVolume = minVolume;
    }

    void Start()
    {
        audioSource.volume = currentVolume;
    }

    public void ControlEngineVolume(float tankSpeed)
    {
        if (tankSpeed > 0)
        {
            if (currentVolume < maxVolume)
                currentVolume += volumeChange * Time.deltaTime;
        }
        else
        {
            if (currentVolume > minVolume)
                currentVolume -= volumeChange * Time.deltaTime;
        }

        currentVolume = Mathf.Clamp(currentVolume, minVolume, maxVolume);
        audioSource.volume = currentVolume;
    }


}
