using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image healthImage;
    [SerializeField] private Sprite[] healthSprites;

    public void DisplayHealth(int maxHealth, int currentHealth) // Healthbar is updated by changing the sprite based on the current health
    {
        healthImage.sprite = healthSprites[maxHealth - currentHealth]; // Calculate the index for the healthSprites array based on the current health and max health, and update the healthImage sprite to reflect the current health visually
    }
   
}
