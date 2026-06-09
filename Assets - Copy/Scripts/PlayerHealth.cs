using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    [SerializeField] private HealthUI healthUI;

    public bool isDying;
    private bool isInvulnerable; // Flag to prevent taking damage during invulnerability frames
    void Start()
    {
        base.Start();
    }

    public override void TakeDamage(int damageAmount) // Update health and health UI when taking damage
    {
        if (isDying) return; // Prevent taking damage if already dying
        if (isInvulnerable) return; // Prevent taking damage during invulnerability frames
        base.TakeDamage(damageAmount);
        healthUI.DisplayHealth(maxHealth, currentHealth);

        StartCoroutine(InvulnerabilityFrames());
    }
    protected override void Die()
    {
        isDying = true;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero; // Stop player movement immediately
        StartCoroutine(PlayerFadeAndDie());
    }

    private IEnumerator PlayerFadeAndDie() // Fade out the player sprite and its children before loading the lose scene
    {
        float duration = 1.5f;
        float elapsed = 0f;
        Color color = _spriteRenderer.color;
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            elapsed += Time.deltaTime;

            foreach (SpriteRenderer sr in sprites)
            {
                sr.color = new Color(color.r, color.g, color.b, alpha);
            }
          
            yield return null;
        }
        SceneManager.LoadScene("Lose");
    }

    private IEnumerator InvulnerabilityFrames() // Coroutine to handle invulnerability frames after taking damage
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(1f);
        isInvulnerable = false;
    }

    public void Heal (int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        healthUI.DisplayHealth(maxHealth, currentHealth);

    
    }
}
