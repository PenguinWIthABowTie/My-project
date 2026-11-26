using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState { GAMEPLAY, BATTLE, PAUSE }
public class PlayerStatsScript : MonoBehaviour
{
    public PlayerState state;
    private MovementController movementController;
    private PlayerBattleScript playerBattleScript;
    private InteractableDetector interactableDetector;
    private ThirdPersonCamera thirdPersonCamera;

    public float maxHealth = 100f;
    public float currentHealth;
    public Slider healthbarSlider;


    void Start()
    {
        interactableDetector = GetComponent<InteractableDetector>();
        movementController = GetComponent<MovementController>();
        playerBattleScript = GetComponent<PlayerBattleScript>();

        currentHealth = maxHealth;
        healthbarSlider.maxValue = maxHealth;
    }

    void Update()
    {
        healthbarSlider.value = currentHealth;
    }

    public void Battle()
    {
        state = PlayerState.BATTLE;

        interactableDetector.enabled = false;
        movementController.enabled = false;
        playerBattleScript.enabled = true;

    }

    public void GamePlay()
    {
        state = PlayerState.GAMEPLAY;

        interactableDetector.enabled = true;
        movementController.enabled = true;
        playerBattleScript.enabled = false;
    }

    public void Pause()
    {
        state = PlayerState.PAUSE;

        interactableDetector.enabled = false;
        movementController.enabled = false;
        playerBattleScript.enabled = false;
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
        interactableDetector.enabled = false;
        movementController.enabled = false;
        playerBattleScript.enabled = false;
        Debug.Log("Player Died!");

        // Destroy(gameObject);
    }
}
