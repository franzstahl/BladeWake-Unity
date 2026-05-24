using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private int currentHealth;
    [SerializeField] private AudioClip hurtSound;
    

    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;
    protected AudioSource _audioSource;
    
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
        CameraFollow.Instance.TriggerShake();
    }

   protected virtual void Die()
    {
        StopAllCoroutines();
        _spriteRenderer.color = _originalColor;
        StartCoroutine(FadeAndDie());
    }

    private IEnumerator FadeAndDie()
    {
        float duration = 0.8f;
        float elapsed = 0f;
        Color color = _spriteRenderer.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            _spriteRenderer.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
        Destroy(gameObject);
    }

    private IEnumerator FlashRed()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        _spriteRenderer.color = _originalColor;

    }

   

}




 