using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    [SerializeField] private AudioClip doorOpenSound;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private TMP_Text message;

    private AudioSource _audioSource;
    private bool _playerNearby; 
    private bool _isOpen;
   
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _playerNearby && Key.hasKey && !_isOpen) // Check if the player is nearby, has the key, and the door is not already open
        {
            _audioSource.PlayOneShot(doorOpenSound);
            doorAnimator.SetTrigger("Open");
            _isOpen = true;
            GetComponent<Collider2D>().enabled = false; // Disable the collider to allow passage
             
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !_isOpen)
        {
            message.gameObject.SetActive(true);
            _playerNearby = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            message.gameObject.SetActive(false);
            _playerNearby = false;
        }
    }
}
