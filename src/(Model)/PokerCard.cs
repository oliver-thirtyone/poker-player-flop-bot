using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nancy.Simple
{
    public class PokerCard
    {
        public int rank;

        public string suit;

        private Dictionary<string, int> cardRank;

        public PokerCard(int rank, string suit)
        {
            SetupCardRankDictionary();
            this.rank = rank;
            this.suit = suit;
        }

        public PokerCard(string rank, string suit)
        {
            var mappedRank = MapRankToInt(rank);
            this.rank = mappedRank;
            this.suit = suit;
        }

        public PokerCard(Card card)
        {
            var mappedRank = 0;
            cardRank.TryGetValue(card.rank, out mappedRank);
            this.rank = mappedRank;
            this.suit = card.suit.ToString();
        }

        private int MapRankToInt(string rank)
        {
            SetupCardRankDictionary();
            var mappedRank = 0;
            cardRank.TryGetValue(rank, out mappedRank);
            return mappedRank;
        }

        private void SetupCardRankDictionary()
        {
            cardRank = new Dictionary<string, int>();
            cardRank.Add("1", 1);
            cardRank.Add("2", 2);
            cardRank.Add("3", 3);
            cardRank.Add("4", 4);
            cardRank.Add("5", 5);
            cardRank.Add("6", 6);
            cardRank.Add("8", 7);
            cardRank.Add("9", 8);
            cardRank.Add("10", 9);
            cardRank.Add("J", 10);
            cardRank.Add("Q", 11);
            cardRank.Add("K", 12);
            cardRank.Add("A", 13);
        }

    }
}
