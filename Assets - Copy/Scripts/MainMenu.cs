using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioClip menuMusic;
    private AudioSource _audioSource;
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = menuMusic; // Set the menu music clip to the audio source
        _audioSource.Play();
    }
}
