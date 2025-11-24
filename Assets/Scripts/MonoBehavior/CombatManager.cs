using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CombatManager : MonoBehaviour
{
    [SerializeField]
    GameObject playerObj;

    [SerializeField]
    GameObject enemyObj;

    [SerializeField]
    UnityEvent OnTurnEnd;
    private Animator heroAnimator;
    private Animator enemyAnimator;
    private bool waitingForHero = false;
    private string heroAnimState;
    private bool waitingForEnemy = false;
    private string enemyAnimState;
    private void Awake()
    {
        heroAnimator = playerObj.GetComponent<Animator>();
        enemyAnimator = enemyObj.GetComponent<Animator>();
    }

    private void Update()
    {
        if (waitingForHero)
        {
            var info = heroAnimator.GetCurrentAnimatorStateInfo(0);

            if (info.IsName(heroAnimState) && info.normalizedTime >= 1f)
            {
                waitingForHero = false;
                EnemyAttack();
            }
        }

        if (waitingForEnemy)
        {
            var info = enemyAnimator.GetCurrentAnimatorStateInfo(0);

            if (info.IsName(enemyAnimState) && info.normalizedTime >= 1f)
            {
                waitingForEnemy = false;
                OnTurnEnd?.Invoke();
            }
        }
    }

    public void PlayerAttack(int score)
    {
        Debug.Log("Playing attack animation");
        heroAnimState = "LowAttack"; // the actual state name in your Animator
        waitingForHero = true;
        heroAnimator.SetTrigger("IdleToLowAttackTrigger");
        // Handle damage here
    }

    public void EnemyAttack()
    {
        Debug.Log("Playing enemy attack animation");
        enemyAnimState = "Attack"; // the actual state name in your Animator
        waitingForEnemy = true;
        enemyAnimator.SetTrigger("LightBanditIdleToAttack");
    }
}
