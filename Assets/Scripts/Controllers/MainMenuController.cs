using System.Collections;
using Http;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private RequestSender _requestSender;

    [SerializeField] private TMP_Text _rankingList;
    [SerializeField] private RectTransform _aboutPage;
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
        StartCoroutine(IEShowPage(show));
    }

    private IEnumerator IEShowPage(bool show)
    {
        float x = _aboutPage.localScale.x;
        float z = _aboutPage.localScale.z;
        if (show)
        {
            while (_aboutPage.localScale.y <= 1f)
            {
                _aboutPage.localScale =
                    new Vector3(
                        x,
                        _aboutPage.localScale.y + 0.1f,
                        z
                    );
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            while (_aboutPage.localScale.y >= 0f)
            {
                _aboutPage.localScale =
                    new Vector3(
                        x,
                        _aboutPage.localScale.y - 0.1f,
                        z
                    );
                yield return null;
            }
        }
    }

    public void FetchRankingScores()
    {
        StartCoroutine(IEFetchRankingScores());
    }

    private IEnumerator IEFetchRankingScores()
    {
        yield return _requestSender.GetRankingScores(OnRankingScoresReceived);
    }

    private void OnRankingScoresReceived(RankingScore[] rankingScores)
    {
        _rankingList.text = "";
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
            _rankingList.text = "Failed to fetch ranking scores. \nCheck internet connection.";
        }
    }
}
