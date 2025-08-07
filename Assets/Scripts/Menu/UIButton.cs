using UnityEngine;

public class UIButton : MonoBehaviour
{
    [SerializeField] private SoundEffectID soundToPlay;

    public void TriggerSound()
    {
        SoundController.Instance.PlaySFX(soundToPlay);
    }
}
