using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Http;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private RequestSender _requestSender;

    [SerializeField] private TMP_Text _rankingList;
    [SerializeField] private GameObject _aboutPage;
    private void Start()
    {
        FetchRankingScores();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Space");
    }

    public void ShowAboutPage(bool show)
    {
        _aboutPage.SetActive(show);
    }

    public void FetchRankingScores()
    {
        StartCoroutine(IEFetchRankingScores());
    }

    private IEnumerator IEFetchRankingScores()
    {
        yield return _requestSender.GetRankingScores(rankingScores =>
        {
            List<RankingScore> list = rankingScores.ToList();
            list.Sort();
            rankingScores = list.ToArray();
            
            _rankingList.text = "";
            _rankingList.color = Color.white;
            if (rankingScores != null)
            {
            
                for(var i = 0; i < rankingScores.Length; i++)
                {
                    _rankingList.text +=
                        $"{i + 1}. {rankingScores[i].nickname} - {rankingScores[i].score} - {rankingScores[i].date}\n";
                }
            }
            else
            {
                _rankingList.color = Color.red;
                _rankingList.text = "Failed to fetch ranking scores. \nCheck internet connection.";
            }
        });
    }
}
