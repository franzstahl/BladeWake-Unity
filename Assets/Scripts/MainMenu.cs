using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioClip buttonSound;
    private AudioSource _audioSource;

    public void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void PlayGame()
    {
        _audioSource.PlayOneShot(_audioSource.clip);
        SceneManager.LoadScene("Game");
    }

    
}
