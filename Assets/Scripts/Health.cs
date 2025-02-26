using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;

    [SerializeField] private int currentHealth;

    [SerializeField] private HealthBar bar;

    private void Start()
    {
        currentHealth = maxHealth;
        bar.SetHealth(maxHealth);
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        bar.takeDamage(damage);
    }
}
