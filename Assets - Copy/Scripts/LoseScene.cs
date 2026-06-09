using UnityEngine;
using UnityEngine.SceneManagement;
public class LoseScene : MonoBehaviour
{
    [SerializeField] private AudioClip loseSound;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.PlayOneShot(loseSound);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
       
            SceneManager.LoadScene("Menu");
        }


    }
}
    