using UnityEngine;

[System.Serializable]
public class LetterData
{
    [SerializeField] private string letter = "A";  // must be length 1
    public double GenerationWeight;
    public int Score;

    public char Letter => string.IsNullOrEmpty(letter) ? ' ' : letter[0];

    public LetterData(char letter, double generationWeight, int score)
    {
        this.letter = letter.ToString();
        GenerationWeight = generationWeight;
        Score = score;
    }

    public override bool Equals(object obj)
    {
        return obj is LetterData other &&
               Letter == other.Letter;
    }

    public override int GetHashCode()
    {
        return Letter.GetHashCode();
    }
}
