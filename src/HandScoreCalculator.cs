using System.Collections.Generic;

namespace Nancy.Simple
{
    public static class HandScoreCalculator
    {
        private const int PairScore = 1000;

        public static int GetScore(PokerCard firstCard, PokerCard secondCard, List<PokerCard> boardCards)
        {
            if (IsPair(firstCard, secondCard))
            {
                return PairScore;
            }

            return firstCard.Rank + firstCard.Rank;
        }

        private static bool IsPair(PokerCard firstCard, PokerCard secondCard)
        {
            return firstCard.Rank == secondCard.Rank;
        }
    }
}