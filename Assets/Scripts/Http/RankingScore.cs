using System;

namespace Http
{
    [Serializable]
    public class RankingScore : IComparable<RankingScore>
    {
        public int score;
        public string nickname;
        public string date;

        public int CompareTo(RankingScore other)
        {
            if (this.score > other.score)
            {
                return -1;
            }
            if(this.score < other.score)
            {
                return 1;
            }
            return 0;
        }

        public override string ToString()
        {
            return $"RankingScore{{score={score}, nickname={nickname}, date={date}}}";
        }
    }
}