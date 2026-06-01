using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] private AudioClip healthSound;
    private int _healAmount = 6; // Amount of health the potion restores

    private Vector3 _startPosition;
    private float _speed = 3f;
    private float _amplitude = 0.5f;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        transform.position = _startPosition + Vector3.up * Mathf.Sin(Time.time * _speed) * _amplitude;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(healthSound, transform.position); // Play the health potion sound effect
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>(); // Get the PlayerHealth component from the player
            playerHealth.Heal(_healAmount); // Heal the player by the specified amount
            Destroy(gameObject);
        }
    }



}
