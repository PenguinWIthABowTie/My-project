using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections;

public class NPC_BattleScript2 : MonoBehaviour
{
    public UnityEvent OnBattleEnd;

    [System.Serializable]
    public class QTEPrompt
    {
        [Tooltip("Delay before this prompt appears")]
        public float spawnDelay = 0.2f;

        [Tooltip("Position inside the QTE Canvas")]
        public Vector2 position;

        [Tooltip("How long the prompt remains active")]
        public float duration = 1f;
    }

    [System.Serializable]
    public class QTEPattern
    {
        public string patternName;

        public QTEPrompt[] prompts;
    }

    [Header("Battle Settings")]
    [SerializeField] private float preparationTime = 4f;
    [SerializeField] private float enemyDamage = 20f;

    [Header("QTE Settings")]
    [SerializeField] private GameObject keyPrefab;
    [SerializeField] private RectTransform qteParent;

    [SerializeField] private float perfectWindow = 0.05f;
    [SerializeField] private float goodWindow = 0.2f;
    [SerializeField] private float okayWindow = 0.5f;

    [SerializeField]
    private KeyCode[] possibleKeys =
    {
        KeyCode.W,
        KeyCode.A,
        KeyCode.S,
        KeyCode.D
    };

    [Header("QTE Patterns")]
    [SerializeField] private QTEPattern[] patterns;

    [Header("Player")]
    [SerializeField] private PlayerStatsScript playerStatsScript;

    private bool isInBattle;
    private bool isAttacking;

    public void StartBattle()
    {
        playerStatsScript.Battle();
        if (isInBattle)
            return;

        if (patterns == null || patterns.Length == 0)
        {
            Debug.LogError("No QTE patterns assigned!");
            return;
        }

        if (keyPrefab == null || qteParent == null)
        {
            Debug.LogError("QTE Prefab or QTE Parent is missing!");
            return;
        }

        isInBattle = true;

        StartCoroutine(PlayerTurn());
    }

    private IEnumerator PlayerTurn()
    {

        Debug.Log("Enemy is preparing an attack...");

        yield return new WaitForSeconds(preparationTime);

        StartCoroutine(EnemyTurn());
    }




    private IEnumerator EnemyTurn()
    {
        // Randomly choose one of the predefined patterns
        int randomPattern = Random.Range(0, patterns.Length);

        QTEPattern selectedPattern = patterns[randomPattern];

        Debug.Log("Enemy selected: " + selectedPattern.patternName);

        isAttacking = true;

        // Execute the selected pattern
        foreach (QTEPrompt prompt in selectedPattern.prompts)
        {
            // Wait before spawning the next prompt
            yield return new WaitForSeconds(prompt.spawnDelay);

            yield return StartCoroutine(PerformQTE(prompt));
        }
        isAttacking = false;
        //isInBattle = false;

        Debug.Log("Enemy attack finished!");
        StartCoroutine (PlayerTurn());
        
    }

    private IEnumerator PerformQTE(QTEPrompt prompt)
    {
        // Randomize the required key
        KeyCode requiredKey =
            possibleKeys[Random.Range(0, possibleKeys.Length)];

        //Debug.Log("Randomly selected QTE key: " + requiredKey);

        // Spawn the QTE prefab inside the Canvas
        GameObject spawnedKey = Instantiate(
            keyPrefab,
            qteParent
        );

        // Set its position
        RectTransform rect =
            spawnedKey.GetComponent<RectTransform>();

        if (rect != null)
        {
            rect.anchoredPosition = prompt.position;
        }

        // Set the displayed key
        TMP_Text keyText = spawnedKey.GetComponentInChildren<TMP_Text>(true);

        if (keyText != null)
        {
            keyText.text = requiredKey.ToString();        }
        else
        {
            Debug.LogError("No TMP_Text component found in QTE prefab!");
        }

        spawnedKey.SetActive(true);

        float startTime = Time.time;

        bool responded = false;

        // Wait until the prompt expires
        while (Time.time - startTime < prompt.duration)
        {
            foreach (KeyCode key in possibleKeys)
            {
                if (Input.GetKeyDown(key))
                {
                    responded = true;

                    float reactionTime = Time.time - startTime;

                    if (key != requiredKey)
                    {
                        Debug.Log("Wrong key!");
                        ApplyDamage(enemyDamage);
                    }
                    else
                    {
                        CalculateDamage(reactionTime);
                    }

                    break;
                }
            }

            if (responded)
                break;

            yield return null;
        }

        // If no key was pressed before expiration
        if (!responded)
        {
            Debug.Log("QTE missed!");
            ApplyDamage(enemyDamage);
        }

        // Destroy the prompt after its timing period
        Destroy(spawnedKey);
    }

    private void CalculateDamage(float reactionTime)
    {
        if (reactionTime <= perfectWindow)
        {
            Debug.Log("PERFECT!");
            ApplyDamage(0);
        }
        else if (reactionTime <= goodWindow)
        {
            Debug.Log("GOOD!");
            ApplyDamage(0);
        }
        else if (reactionTime <= okayWindow)
        {
            Debug.Log("OKAY!");
            ApplyDamage(enemyDamage / 2);
        }
        else
        {
            Debug.Log("TOO LATE!");
            ApplyDamage(enemyDamage);
        }
    }

    private void ApplyDamage(float damage)
    {
        if (damage <= 0)
            return;

        Debug.Log("Player takes " + damage + " damage.");

        playerStatsScript.TakeDamage(damage);
    }

    public void EndBattle()
    {
        playerStatsScript.GamePlay();
        OnBattleEnd.Invoke();
        isInBattle = false;
        StopAllCoroutines();
    }
}