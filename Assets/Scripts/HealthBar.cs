using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;

    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int currentHealth = 10;


    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSlider.value != currentHealth) 
        {
            healthSlider.value = currentHealth;
        }
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public void SetHealth(int health)
    {
        maxHealth = health;
        healthSlider.maxValue = maxHealth;
        currentHealth = maxHealth;
    }
}
