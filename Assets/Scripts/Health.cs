using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private int currentHealth;
    [SerializeField] private AudioClip hurtSound;
    

    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;
    private AudioSource _audioSource;
    
   protected virtual void Start()
    {
        currentHealth = maxHealth;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
        _audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        _audioSource.PlayOneShot(hurtSound);

        if (currentHealth <= 0)
        {
            Die();
        }

        StartCoroutine(FlashRed());
    }

   protected virtual void Die()
    {
        StartCoroutine(DieWithDelay());
    }

    private IEnumerator DieWithDelay()
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }

    private IEnumerator FlashRed()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        _spriteRenderer.color = _originalColor;

    }

}




 