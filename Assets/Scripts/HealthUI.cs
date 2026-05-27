using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image healthImage;
    [SerializeField] private Sprite[] healthSprites;
    void Start()
    {
        
    }

    public void DisplayHealth(int maxHealth, int currentHealth) // Healthbar is updated by changing the sprite based on the current health
    {
        healthImage.sprite = healthSprites[maxHealth - currentHealth];
    }
   
    void Update()
    {
        
    }
}
