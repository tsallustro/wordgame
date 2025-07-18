using System;
using System.Collections.Generic;
using UnityEngine;

public class LetterManagerController : MonoBehaviour
{
    public static LetterManagerController Instance { get; private set; }
    
    [SerializeField] private LetterSet ActiveLetterSet;
    [SerializeField] GameObject letterTilePrefab;
    [SerializeField] private Transform selectedPanel;
    [SerializeField] private Transform selectablePanel;

    private List<ManagedLetter> SelectedLetters = new();
    private List<ManagedLetter> SelectableLetters = new();
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
        Debug.Log("Starting Letter Manager Controller with total weight: " + ActiveLetterSet.TotalGenerationWeight);
        foreach (char c in "EXAMPLE")
        {
            var obj = Instantiate(letterTilePrefab, selectablePanel);
            var tile = obj.GetComponent<LetterTile>();
            tile.SetLetter(ActiveLetterSet.GetDataForLetter(c));
            SelectableLetters.Add(new ManagedLetter(tile, obj));
        }
    }

    public void OnLetterClicked(LetterTile tile)
    {
        // Move tile to other panel
        if (tile.transform.parent == selectablePanel)
        {
            tile.transform.SetParent(selectedPanel, false);
            // HERE
        }
        else if (tile.transform.parent == selectedPanel)
        {
            tile.transform.SetParent(selectablePanel, false);
            // HERE
        }
    }

     private class ManagedLetter
    {
        public LetterTile Tile;
        public GameObject GameObject;

        public ManagedLetter(LetterTile tile, GameObject obj)
        {
            Tile = tile;
            GameObject = obj;
        }
    }

}
