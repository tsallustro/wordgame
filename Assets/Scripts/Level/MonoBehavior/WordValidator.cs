using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WordValidator : MonoBehaviour
{
    private HashSet<string> validWords;

    void Awake()
    {
        LoadWordList();
    }

    private void LoadWordList()
    {
        TextAsset wordFile = Resources.Load<TextAsset>("words"); // No file extension
        if (wordFile == null)
        {
            Debug.LogError("Word list could not be loaded!");
            return;
        }

        validWords = new HashSet<string>(
            wordFile.text.Split('\n').Select(w => w.Trim()).Where(w => w.Length > 0),
            System.StringComparer.Ordinal
        );

        Debug.Log($"Loaded {validWords.Count} uppercase words.");
    }

    public bool IsValid(string word)
    {
        bool value =
            !string.IsNullOrEmpty(word) && validWords != null && validWords.Contains(word.Trim());
        Debug.Log($"Word {word} is valid? {value}");
        return value;
    }
}
