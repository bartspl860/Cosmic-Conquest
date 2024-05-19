using System;

namespace Http
{
    [Serializable]
    public class RankingScore
    {
        public int score;
        public string nickname;
        public string date;
        
        public override string ToString()
        {
            return $"RankingScore{{score={score}, nickname={nickname}, date={date}}}";
        }
    }
}