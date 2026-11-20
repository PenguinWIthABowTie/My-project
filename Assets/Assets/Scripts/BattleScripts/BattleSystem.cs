using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN }

public class BattleSystem : MonoBehaviour
{

    public GameObject player;
    private GameObject currentEnemy;
    private NPC_Controller npc_Controller;

    //public Transform playerBattlePos;
    //public Transform enemyBattlePos;
    public Button QTIbutton;
    public Transform parentCanvas;

    public BattleState state;
    public void StartBattle(GameObject enemy)
    {
        currentEnemy = enemy;
        npc_Controller = currentEnemy.GetComponent<NPC_Controller>();


        state = BattleState.START;
        SetupBattle();
    }

    void SetupBattle()
    {
        //player.transform.position = playerBattlePos.position;
        //currentEnemy.transform.position = enemyBattlePos.position;

        state = BattleState.ENEMYTURN;
        EnemyTurn();
    }

    void EnemyTurn()
    {
        Instantiate(QTIbutton, parentCanvas);
    }
}
