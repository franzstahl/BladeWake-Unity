using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScene : MonoBehaviour
{
    [SerializeField] private AudioClip winSound;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.PlayOneShot(winSound);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {

            SceneManager.LoadScene("Menu");
        }

    }
}
