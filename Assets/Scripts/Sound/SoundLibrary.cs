using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SFXEntry
{
    public SoundEffectID type;
    public AudioClip clip;
}

[CreateAssetMenu(menuName = "Audio/Sound Library")]
public class SoundLibrary : ScriptableObject
{
    public List<SFXEntry> soundEffects;

    private Dictionary<SoundEffectID, AudioClip> lookup;

    public AudioClip GetClip(SoundEffectID type)
    {
        if (lookup == null)
        {
            lookup = new();
            foreach (var entry in soundEffects)
                lookup[entry.type] = entry.clip;
        }

        return lookup.TryGetValue(type, out var clip) ? clip : null;
    }
}
