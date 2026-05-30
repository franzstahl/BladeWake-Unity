using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private AudioClip doorOpenSound;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private TMP_Text message;

    private AudioSource _audioSource;
   
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }
}
