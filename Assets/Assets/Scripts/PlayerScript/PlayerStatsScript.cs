using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState { GAMEPLAY, BATTLE, PAUSE }
public class PlayerStatsScript : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Slider healthbarSlider;

    void Start()
    {
        currentHealth = maxHealth;
        healthbarSlider.maxValue = maxHealth;
    }

    void Update()
    {
        healthbarSlider.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("Health: " + currentHealth);
    }

    void Die()
    {
        Debug.Log("Player Died!");

        // Destroy(gameObject);
    }
}
