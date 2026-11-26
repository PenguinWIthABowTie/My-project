using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health = 100;
    private NPC_BattleScript2 battleScript2;


    public void Start()
    {
        battleScript2 = GetComponent<NPC_BattleScript2>();
    }

    public void TakeDamage(int damage)
    {
        //Debug.Log($"Enemy Takes {damage} Damage");
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            battleScript2.EndBattle();
        }
    }
}
