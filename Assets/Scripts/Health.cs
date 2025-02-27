using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;

    [SerializeField] private int currentHealth;

    [SerializeField] private HealthBar bar;

    [SerializeField] private float invulnTime = 0.1f;

    private float invulnTimer;
    private bool invuln;

    [SerializeField] private ParticleSystem particles;

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

        if (invuln)
        {
            invulnTimer += Time.deltaTime;
            if (invulnTimer >= invulnTime)
            {
                invuln = false;
                invulnTimer = 0;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (!invuln)
        {
            currentHealth -= damage;
            bar.takeDamage(damage);
            particles.Play();
        }
    }
}
