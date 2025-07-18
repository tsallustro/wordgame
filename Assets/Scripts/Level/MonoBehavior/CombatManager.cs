using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField]
    GameObject playerObj;

    private Animator heroAnimator;

    private void Awake()
    {
        heroAnimator = playerObj.GetComponent<Animator>();
    }

    public void PlayerAttack(int score)
    {
        Debug.Log("Playing attack animation");
        heroAnimator.SetTrigger("IdleToLowAttackTrigger");
    }
}
