using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Http
{
    public class RequestSender : MonoBehaviour
    {
        [SerializeField] private string _apiUrl;
        [SerializeField] private string _secret;
        
        public IEnumerator GetRankingScores(Action<RankingScore[]> callback)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(_apiUrl + "fetch"))
            {
                yield return webRequest.SendWebRequest();
                if (webRequest.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"Failed to fetch ranking scores: {webRequest.error}");
                    callback(null);
                }
                else
                {
                    string json = webRequest.downloadHandler.text;
                    RankingScore[] rankingScores = JsonHelper.FromJson<RankingScore>(json);
                    callback(rankingScores);
                }
            }
        }
        
        public IEnumerator AddRankingScore(string nickname, int score)
        {
            var payload = new UserScoreTokenPayload()
            {
                nickname = nickname,
                score = score
            };
                
            var token = Http.JWTGenerator.Builder()
                .Secret(_secret)
                .Expiration(DateTimeOffset.Now.AddSeconds(15))
                .Payload(payload)
                .Generate();
            

            using (UnityWebRequest www = UnityWebRequest.Post(_apiUrl + "submit", token, "application/json"))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                }
                else
                {
                    Debug.Log("Form upload complete!");
                }
            }
        }
    }
}