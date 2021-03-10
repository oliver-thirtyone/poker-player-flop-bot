using System.Collections.Generic;

namespace Nancy.Simple
{
    public class PokerCard
    {
        public readonly int Rank;
        public readonly Suit Suit;

        private static readonly Dictionary<string, int> CardRank = new Dictionary<string, int>
        {
            {"1", 1},
            {"2", 2},
            {"3", 3},
            {"4", 4},
            {"5", 5},
            {"6", 6},
            {"8", 7},
            {"9", 8},
            {"10", 9},
            {"J", 10},
            {"Q", 11},
            {"K", 12},
            {"A", 13}
        };

        public PokerCard(Card card)
        {
            Rank = MapRankToInt(card.rank);
            Suit = card.suit;
        }

        private static int MapRankToInt(string rank)
        {
            var mappedRank = 0;
            CardRank.TryGetValue(rank, out mappedRank);
            return mappedRank;
        }
    }
}