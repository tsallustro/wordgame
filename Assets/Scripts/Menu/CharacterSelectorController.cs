using UnityEngine;

public class CharacterSelectorController : MonoBehaviour
{
    [SerializeField]
    private CharacterData characterData;

    public void HandleClick()
    {
        Debug.Log($"Starting new game with character {characterData.Name}");
    }
}
