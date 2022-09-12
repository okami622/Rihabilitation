using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE : MonoBehaviour
{
    [SerializeField] private AudioClip play;
    [SerializeField] private AudioClip ok;
    [SerializeField] private AudioClip ng;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        audioSource.PlayOneShot(play);
    }

    public void OK()
    {
        audioSource.PlayOneShot(ok);
    }

    public void NG()
    {
        audioSource.PlayOneShot(ng);
    }
}
