using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/CharacterData")]
[Serializable]
public class CharacterData : ScriptableObject
{
    [SerializeField]
    private string characterName;

    [SerializeField]
    private int hp;

    [SerializeField]
    private int def;

    [SerializeField]
    private int off;

    public string Name => characterName;
    public int HP => hp;
    public int Def => def;
    public int Off => off;
}
