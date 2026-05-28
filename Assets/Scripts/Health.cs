using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int currentHealth;
    [SerializeField] private AudioClip hurtSound;
    

    protected SpriteRenderer _spriteRenderer;
    private Color _originalColor;
    protected AudioSource _audioSource;
    
   protected virtual void Start()
    {
        currentHealth = maxHealth;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
        _audioSource = GetComponent<AudioSource>();
    }

    public virtual void TakeDamage(int damageAmount) // Handle taking damage 
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

   protected virtual void Die() // Handle death, can be overridden by player and enemy for different behaviour
    {
        StopAllCoroutines();
        _spriteRenderer.color = _originalColor;
        StartCoroutine(EnemyFadeAndDie());
    }

    private IEnumerator EnemyFadeAndDie() // Fade out the sprite and its children before destroying the object
    {
        float duration = 0.8f;
        float elapsed = 0f;
        Color color = _spriteRenderer.color;
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);

            foreach (SpriteRenderer sr in sprites)
            {
                sr.color = new Color(color.r, color.g, color.b, alpha);
            }
           
            yield return null;
        }
        Destroy(gameObject);
    }

    private IEnumerator FlashRed() // Flash red briefly when taking damage
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        _spriteRenderer.color = _originalColor;

    }

   

}




 