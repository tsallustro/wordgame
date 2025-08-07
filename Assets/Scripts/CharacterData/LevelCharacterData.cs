using UnityEngine;

public class LevelCharacterData : MonoBehaviour
{
    public static LevelCharacterData Instance;

    public CharacterData characterData;
    public int currentHP = 1;

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

    public void SetCharacterData(CharacterData newData)
    {
        this.characterData = newData;
        currentHP = newData.HP;
    }
}
