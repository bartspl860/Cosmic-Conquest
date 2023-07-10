using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private string _healthPointSpriteAssetName;
    [SerializeField, Header("Dependencies")]
    private TextMeshProUGUI _healtBar;
    [SerializeField]
    private TextMeshProUGUI _score;


    public void DisplayHealthPoints(int nPoints)
    {
        _healtBar.text = string.Empty;
        for(int i = 0; i < nPoints; i++)
        {
            _healtBar.text += $"<sprite name=\"{_healthPointSpriteAssetName}\">\n";
        }
    }

    public void DisplayScore(int score)
    {
        _score.text = $"score: {score}";
    }
}
