using System;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLetterSet", menuName = "Letter System/Letter Set")]
public class LetterSet : ScriptableObject
{
    [SerializeField]
    private List<LetterData> letterDatas = new();

    public List<LetterData> LetterDatas => letterDatas;

    private double TotalGenWeight { get; set; } = -1;
    public double TotalGenerationWeight => TotalGenWeight;
    private readonly System.Random random = new();

    private void OnEnable()
    {
        // Only populate if empty (so it doesn't overwrite existing data)
        if (letterDatas.Count == 0)
        {
            for (char c = 'A'; c <= 'Z'; c++)
            {
                letterDatas.Add(new LetterData(c, 1f, 1));  // default weight=1, score=1
            }
        }
        CalculateTotalWeight();
    }

    public LetterData GetDataForLetter(char letter)
    {
        return LetterDatas[(int)Char.ToUpper(letter) - 65];
    }

    public LetterData GetRandomLetter()
    {
        double randValue = random.NextDouble() * 100;  // since total weight is 100
        double cumulative = 0;

        foreach (var letter in letterDatas)
        {
            cumulative += letter.GenerationWeight;
            if (randValue <= cumulative)
            {
                return letter;
            }
        }

        // Fallback in case of floating point issues (shouldn't happen if total weight is 100)
    return letterDatas[^1];
    }
    private void CalculateTotalWeight()
    {
        TotalGenWeight = 0;
        foreach (var data in letterDatas)
        {
            TotalGenWeight += data.GenerationWeight;
        }
    }
}

