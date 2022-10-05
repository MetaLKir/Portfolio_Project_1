using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DestroyOnAudioEnds : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip soundToPlay;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.PlayOneShot(soundToPlay);
        StartCoroutine(WaitCoroutine());
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(soundToPlay.length);
        Destroy(gameObject);
    }
}
