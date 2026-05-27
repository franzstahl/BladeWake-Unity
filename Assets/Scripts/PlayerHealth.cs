using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    [SerializeField] private HealthUI healthUI;
    void Start()
    {
        base.Start();
    }

    public override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);
        healthUI.DisplayHealth(maxHealth, currentHealth);
    }
    protected override void Die()
    {
        StartCoroutine(PlayerFadeAndDie());
    }

    private IEnumerator PlayerFadeAndDie()
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
}
