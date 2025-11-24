using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LetterController : MonoBehaviour
{
    public static LetterController Instance { get; private set; }

    [SerializeField]
    private Boolean ExampleMode = false;

    [SerializeField]
    private LetterSet ActiveLetterSet;

    [SerializeField]
    GameObject letterTilePrefab;

    [SerializeField]
    private Transform selectedPanel;

    [SerializeField]
    private Transform selectablePanel;

    [SerializeField]
    private Button submitButton;

    [SerializeField]
    private WordValidator wordValidator;

    private List<ManagedLetter> SelectedLetters = new();
    private List<ManagedLetter> SelectableLetters = new();

    [SerializeField]
    UnityEvent<int> OnSubmit;

    private static readonly int NUMTOGENERATE = 16;

    Boolean isEnabled = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (ExampleMode)
        {
            Debug.Log("Starting Letter Manager Controller in EXAMPLE mode");

            int i = 0;
            foreach (char c in "EXAMPLE")
            {
                var obj = Instantiate(letterTilePrefab, selectablePanel);
                var tile = obj.GetComponent<LetterTile>();
                tile.SetLetter(ActiveLetterSet.GetDataForLetter(c));
                SelectableLetters.Add(new ManagedLetter(tile, obj, i++));
            }
        }
        else
        {
            Debug.Log(
                "Starting Letter Manager Controller with total weight: "
                    + ActiveLetterSet.TotalGenerationWeight
            );
            for (int i = 0; i < NUMTOGENERATE; i++)
            {
                GenerateNewLetter(i);
            }
        }
    }

    public void OnLetterClicked(LetterTile tile)
    {
        if (isEnabled)
        { // Move tile to other panel
            if (tile.transform.parent == selectablePanel)
            {
                ManagedLetter managed = SelectableLetters.Find(m => m.Tile == tile);
                tile.transform.SetParent(selectedPanel, false);
                SelectedLetters.Add(managed);
                SelectableLetters.Remove(managed);
            }
            else if (tile.transform.parent == selectedPanel)
            {
                ManagedLetter managed = SelectedLetters.Find(m => m.Tile == tile);
                int currentIndex = SelectedLetters.IndexOf(managed);

                // Remove the clicked tile and everything *after* it
                for (int i = SelectedLetters.Count - 1; i >= currentIndex; i--)
                {
                    var toDeselect = SelectedLetters[i];

                    // Move it back to selectable panel
                    toDeselect.Tile.transform.SetParent(selectablePanel, false);
                    toDeselect.Tile.transform.SetSiblingIndex(toDeselect.originalIndex);

                    // Update lists
                    SelectableLetters.Add(toDeselect);
                    SelectedLetters.RemoveAt(i);
                }

                // Reparent and reorder based on new sorted list
                ReSortSelectableLetters();
            }

            if (SelectedLetters.Count > 0 && wordValidator.IsValid(SelectedToString()))
                submitButton.interactable = true;
            else
                submitButton.interactable = false;
        }
    }

    public void HandleSubmit()
    {
        int score = CalculateScore();
        Debug.Log("Score: " + score);
        SelectedLetters.Clear();
        ClearSelected();
        GenerateFreshLetters();
        submitButton.interactable = false;
        ToggleEnabled();
        OnSubmit?.Invoke(score);
    }

    private int CalculateScore()
    {
        int totalScore = 0;
        foreach (ManagedLetter letter in SelectedLetters)
        {
            totalScore += letter.Tile.LetterData.Score;
        }

        return totalScore;
    }

    private void ReSortSelectableLetters()
    {
        SelectableLetters = SelectableLetters.OrderBy(m => m.originalIndex).ToList();
        for (int i = 0; i < SelectableLetters.Count; i++)
        {
            SelectableLetters[i].Tile.transform.SetSiblingIndex(i);
        }
    }

    private void GenerateFreshLetters()
    {
        for (int i = 0; i < SelectableLetters.Count; i++)
        {
            SelectableLetters[i].originalIndex = i;
        }

        for (int i = SelectableLetters.Count; i < NUMTOGENERATE; i++)
        {
            GenerateNewLetter(i);
        }

        ReSortSelectableLetters();
    }

    private void GenerateNewLetter(int idx)
    {
        LetterData data = ActiveLetterSet.GetRandomLetter();
        var obj = Instantiate(letterTilePrefab, selectablePanel);
        var tile = obj.GetComponent<LetterTile>();
        tile.SetLetter(data);
        SelectableLetters.Add(new ManagedLetter(tile, obj, idx));
    }

    public void HandleScramble()
    {
        if (isEnabled && SelectedLetters.Count == 0)
        {
            Debug.Log("Scrambling");
            ClearSelectable();
            GenerateFreshLetters();
        }
    }

    private void ClearSelected()
    {
        SelectedLetters.Clear();
        foreach (Transform child in selectedPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void ClearSelectable()
    {
        SelectableLetters.Clear();
        foreach (Transform child in selectablePanel.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private string SelectedToString()
    {
        StringBuilder stringBuilder = new();
        for (int i = 0; i < SelectedLetters.Count; i++)
        {
            stringBuilder.Append(SelectedLetters[i].Tile.LetterData.Letter);
        }

        return stringBuilder.ToString();
    }

    public void ToggleEnabled()
    {
        isEnabled = !isEnabled;
        if (isEnabled)
        {
            Debug.Log("Enabling UI");
            submitButton.interactable = true;
        }
        else
        {
            Debug.Log("Disabling UI");
            submitButton.interactable = false;
        }
    }

    private class ManagedLetter
    {
        public LetterTile Tile;
        public GameObject GameObject;

        public int originalIndex;

        public ManagedLetter(LetterTile tile, GameObject obj, int idx)
        {
            Tile = tile;
            GameObject = obj;
            originalIndex = idx;
        }
    }
}
