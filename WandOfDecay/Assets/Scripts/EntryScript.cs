using TMPro;
using UnityEngine;

public class EntryScript : MonoBehaviour
{
    [SerializeField]
    private TMP_Text position;
    [SerializeField]
    private TMP_Text playerName;
    [SerializeField]
    private TMP_Text score;
    public int Position
    {
        get => int.Parse(position.text);
        set => position.text = "" + value;
    }
    public string Name
    {
        get => playerName.text;
        set => playerName.text = value;
    }
    public int Score
    {
        get => int.Parse(score.text);
        set => score.text = "" + value;
    }
}