using System.Collections;
using System.Linq;
using Effects;
using Http;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controllers
{
    public class GameOverController : MonoBehaviour
    {
        [SerializeField] 
        private RequestSender _requestSender;

        [SerializeField] 
        private Player.Player _player;

        [SerializeField] 
        private GameObject[] _controlButtons; 
        
        [SerializeField]
        private GameObject _Top10Text;

        public void SubmitScore(TMP_InputField inputField)
        {
            StartCoroutine(IESubmitScore(inputField.text, _player.GetScore()));
            _Top10Text.SetActive(false);
        }

        public void RestartGame()
        {
            SceneManager.LoadScene("Space");
        }

        public void ReturnToMainMenu()
        {
            SceneManager.LoadScene("Menu");
        }
        
        public void CheckIfScoreTop10()
        {
            StartCoroutine(IEFetchRankingScores());
        }

        private IEnumerator IEFetchRankingScores()
        {
            yield return _requestSender.GetRankingScores(scores =>
            {
                if(scores == null)
                    return;
                var smallest = scores.Min(rs => rs.score);
                if (_player.GetScore() > smallest || scores.Length < 10)
                {
                    //show score add screen
                    _Top10Text.SetActive(true);
                    foreach (var controlButton in _controlButtons)
                    {
                        controlButton.SetActive(true);
                    }
                    _Top10Text.GetComponent<Twinkling>().StartTwinkling(5);
                }
            });
        }
        
        private IEnumerator IESubmitScore(string nickname, int score)
        {
            yield return _requestSender.AddRankingScore(nickname, score);
        }
    }
}