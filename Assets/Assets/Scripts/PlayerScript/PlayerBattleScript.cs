using UnityEngine;

public class PlayerBattleScript : MonoBehaviour
{
    [SerializeField] public bool PlayersTurn;
    [SerializeField] public int damage;
    [SerializeField] public NPC_BattleScript2 npc_BattleScript2;
    private int turnsAmount;
    private int maxAmountOfTurns;

    private void Update()
    {
        if(PlayersTurn)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                npc_BattleScript2.EnemyDamage(damage);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Q");
                npc_BattleScript2.defenceUp = true;
            }
        }
    }



}
