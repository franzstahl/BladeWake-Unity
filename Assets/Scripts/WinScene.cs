using UnityEngine;

public class WinScene : MonoBehaviour
{
    [SerializeField] private AudioClip winSound;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.PlayOneShot(winSound);
    }

    void Update()
    {
        
    }
}
