using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace Controllers
{
    public class UIController : MonoBehaviour
    {
        [SerializeField, Header("Properties")]
        private string healthPointGraySpriteAssetName;
        [SerializeField]
        private string healthPointSpriteAssetName;
        [SerializeField, Header("Dependencies")]
        private TextMeshProUGUI healthBar;
        [SerializeField]
        private TextMeshProUGUI score;

        public void DisplayHealthPoints(int nPoints, int maxNPoints)
        {
            Assert.IsTrue(nPoints <= maxNPoints);
            healthBar.text = string.Empty;
            for (int i = 0; i < maxNPoints - nPoints; i++)
            {
                healthBar.text += $"<sprite name=\"{healthPointGraySpriteAssetName}\">\n";
            }
            for (int i = 0; i < nPoints; i++)
            {
                healthBar.text += $"<sprite name=\"{healthPointSpriteAssetName}\">\n";
            }        
        }

        public void DisplayScore(int score)
        {
            this.score.text = $"score: {score}";
        }
    }
}
