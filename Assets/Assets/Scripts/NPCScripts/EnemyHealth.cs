using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int currentHealth = 100;
    [SerializeField] private int maxHealth = 100;
    private NPC_BattleScript2 battleScript2;

    public Slider healthbarSlider;


    public void Start()
    {
        battleScript2 = GetComponent<NPC_BattleScript2>();

        currentHealth = maxHealth;
        healthbarSlider.maxValue = maxHealth;
    }

    private void Update()
    {
        healthbarSlider.value = currentHealth;
    }
    public void TakeDamage(int damage)
    {
        //Debug.Log($"Enemy Takes {damage} Damage");
        currentHealth -= damage;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            battleScript2.EndBattle();
            gameObject.SetActive(false);
        }
    }
}
