using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Rendering;
using JetBrains.Annotations;

public class NPC_BattleSystem : MonoBehaviour
{

    [SerializeField] private float preparationTime = 2f;
    [SerializeField] private float enemyDamage = 20f;
    [SerializeField] private TMP_Text keyPrompt;
    [SerializeField] private GameObject keyPrefab;
    [SerializeField] private float perfectWindow = 0.05f;
    [SerializeField] private float goodWindow = 0.2f;
    [SerializeField] private float lateWindow = 0.5f;
    [SerializeField] private float missWindow = 1f;

    private GameObject player;
    private PlayerStatsScript playerStatsScript;
    private PlayerBattleScript playerBattleScript;

    private KeyCode[] possibleKeys = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };

    private bool isInBattle;
    private bool EnemiesTurn;

    public void StartBattle()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStatsScript = player.GetComponent<PlayerStatsScript>();
        playerBattleScript = player.GetComponent<PlayerBattleScript>();



        keyPrompt = Instantiate(keyPrefab, transform.position, Quaternion.identity).GetComponent<TMP_Text>();

        if (isInBattle)
            return;

        isInBattle = true;


        StartCoroutine(PlayerTurn());
    }

    private IEnumerator PlayerTurn()
    {

        EnemiesTurn = false;
        playerBattleScript.PlayersTurn = true;

        yield return new WaitForSeconds(preparationTime);

        yield return StartCoroutine(EnemyTurn());

    }

    private IEnumerator EnemyTurn()
    {
        EnemiesTurn = true;
        playerBattleScript.PlayersTurn = false;

        KeyCode requireKey = possibleKeys[Random.Range(0, possibleKeys.Length)];

        keyPrompt.text = requireKey.ToString();
        keyPrompt.gameObject.SetActive(true);

        float startTime = Time.time;

        bool responded = false;

        while(Time.time - startTime < lateWindow)
        {
            foreach (KeyCode key in possibleKeys)
            {
                if (Input.GetKeyDown(key))
                {
                    responded = true;

                    float reactionTime = Time.time - startTime;
                    if (key != requireKey)
                    {
                        ApplyDamage(enemyDamage);
                    }
                    else
                    {
                        CalculateDamage(reactionTime);
                    }
                    break;
                }
            }

            if(responded) break;

            yield return null;
        }

        if(!responded)
        {
            ApplyDamage(enemyDamage);
        }

        keyPrompt.gameObject.SetActive(false);
    }

    private void CalculateDamage(float reactionTime)
    {
        if (reactionTime <= perfectWindow)
        {
            Debug.Log("Perfect timing");
            ApplyDamage(0);
        }
        else if (reactionTime <= goodWindow)
        {
            Debug.Log("Good timing");
            ApplyDamage(0);
        }
        else if (reactionTime <= lateWindow)
        {
            Debug.Log("Late timing");
            ApplyDamage(enemyDamage / 2);
        }
        else
        {
            Debug.Log("Missed timing");
            ApplyDamage(enemyDamage);
        }
    }

    private void ApplyDamage (float damage)
    {
        if (damage <= 0) return;

        Debug.Log($"Player takes {damage} damage.");

        playerStatsScript.TakeDamage(damage);
    }



}
