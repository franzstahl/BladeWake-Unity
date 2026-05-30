using Unity.VisualScripting;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private AudioClip keySound;

    public static bool hasKey = false; // Static variable to track if the player has collected the key


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(keySound, transform.position); // Play the key collection sound at the position of the key
            hasKey = true;
            Destroy(gameObject);
        }
    }


}
