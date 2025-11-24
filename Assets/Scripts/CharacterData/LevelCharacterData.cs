using UnityEngine;

public class LevelCharacterData : MonoBehaviour
{
    public static LevelCharacterData Instance;

    public CharacterData characterData;

    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
