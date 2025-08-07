using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectorController : MonoBehaviour
{
    [SerializeField]
    private CharacterData characterData;

    public void HandleClick()
    {
        MainMenuPanelController.Instance.LoadLevel(characterData);
    }
}
