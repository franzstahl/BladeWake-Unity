using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    [SerializeField] private Image healthImage;
    [SerializeField] private Sprite[] healthSprites;

    public void DisplayHealth(int maxHealth, int currentHealth) // Healthbar is updated by changing the sprite based on the current health, with 6 health per sprite
    {
        int index = (maxHealth - currentHealth) / 6; // Calculate the index for the healthSprites array based on the current health, with 6 health points per sprite
        index = Mathf.Clamp(index, 0, healthSprites.Length - 1); // Ensure index is within bounds of the healthSprites array
        healthImage.sprite = healthSprites[index]; // Update the health image sprite to reflect the current health of the boss
    }
}
