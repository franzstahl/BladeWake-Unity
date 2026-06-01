using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private AudioClip pauseSound;
    private bool _isPaused = false;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource= GetComponent<AudioSource>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_isPaused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f; // Pause the game
            _isPaused = true;
            _audioSource.PlayOneShot(pauseSound);
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
        _audioSource.PlayOneShot(pauseSound);
    }
}
