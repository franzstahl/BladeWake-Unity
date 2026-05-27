using UnityEngine;

public class LoseScene : MonoBehaviour
{
    [SerializeField] private AudioClip loseSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(loseSound);
    }

    
    void Update()
    {
        
    }
}
