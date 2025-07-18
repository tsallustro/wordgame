using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LetterTile : MonoBehaviour
{
    [SerializeField] private TMP_Text letterText;

    public LetterData LetterData { get; set; }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void SetLetter(LetterData data)
    {
        LetterData = data;
        letterText.text = data.Letter.ToString();
        
    }

    private void OnClick()
    {
        LetterManagerController.Instance.OnLetterClicked(this);
    }
}