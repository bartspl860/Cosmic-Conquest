using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class UIController : MonoBehaviour
{
    [SerializeField, Header("Properties")]
    private string _healthPointGraySpriteAssetName;
    [SerializeField]
    private string _healthPointSpriteAssetName;
    [SerializeField, Header("Dependencies")]
    private TextMeshProUGUI _healtBar;
    [SerializeField]
    private TextMeshProUGUI _score;


    public void DisplayHealthPoints(int nPoints, int maxNPoints)
    {
        Assert.IsTrue(nPoints <= maxNPoints);

        _healtBar.text = string.Empty;
        for (int i = 0; i < maxNPoints - nPoints; i++)
        {
            _healtBar.text += $"<sprite name=\"{_healthPointGraySpriteAssetName}\">\n";
        }
        for (int i = 0; i < nPoints; i++)
        {
            _healtBar.text += $"<sprite name=\"{_healthPointSpriteAssetName}\">\n";
        }        
    }

    public void DisplayScore(int score)
    {
        _score.text = $"score: {score}";
    }
}
