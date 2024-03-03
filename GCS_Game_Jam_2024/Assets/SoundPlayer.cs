using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> clipList;

    private void Awake()
    {
        if (!TryGetComponent(out audioSource))
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayRandom()
    {
        int idx = UnityEngine.Random.Range(0, clipList.Count);
        audioSource.clip = clipList[idx];
        audioSource.Play();
    }

    public void PlaySoundIndex(int idx)
    {
        audioSource.clip = clipList[idx];
        audioSource.Play();
    }
}
