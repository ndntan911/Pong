using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void SetScore(int value)
    {
        text.text = value.ToString();
    }
}
