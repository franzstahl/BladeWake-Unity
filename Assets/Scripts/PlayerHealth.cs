using UnityEngine;

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
       
    }
   
}
