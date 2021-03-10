using System.Collections.Generic;

namespace Nancy.Simple
{
    public static class HandScoreCalculator
    {
        private const int StraightFlush = 8000;
        private const int Foursome = 7000;
        private const int FullHouse = 6000;
        private const int Flush = 5000;
        private const int Straight = 4000;
        private const int Threesome = 3000;
        private const int TwoPair = 2000;
        private const int OnePair_High = 1000;
        private const int OnePair_Low = 500;

        public static int GetScore(PokerCard firstCard, PokerCard secondCard, List<PokerCard> boardCards)
        {
            if (IsStraightFlush(firstCard, secondCard, boardCards))
            {
                return StraightFlush;
            }

            if (IsFoursome(firstCard, secondCard, boardCards))
            {
                return Foursome;
            }

            if (IsFullHouse(firstCard, secondCard, boardCards))
            {
                return FullHouse;
            }

            if (IsFlush(firstCard, secondCard, boardCards))
            {
                return Flush;
            }

            if (IsStraight(firstCard, secondCard, boardCards))
            {
                return Straight;
            }

            if (IsThreesome(firstCard, secondCard, boardCards))
            {
                return Threesome;
            }

            if (IsTwoPair(firstCard, secondCard, boardCards))
            {
                return TwoPair;
            }

            if (IsPair(firstCard, secondCard, boardCards))
            {
                return firstCard.Rank > 7 ? OnePair_High : OnePair_Low;
            }

            return firstCard.Rank + secondCard.Rank;
        }

        private static bool IsStraightFlush(PokerCard firstCard, PokerCard secondCard, List<PokerCard> boardCards)
        {
            return false;
        }

        private static bool IsFoursome(PokerCard firstCard, PokerCard secondCard, List<PokerCard> boardCards)
        {
            return false;
        }

        private static bool IsFullHouse(PokerCard firstCard, PokerCard secondCard, List<PokerCard> boardCards)
        {
            return false;
        }

        private static bool IsFlush(PokerCard firstCard, PokerCard secondCard, List<PokerCard> boardCards)
        {
            return false;
        }

        private static bool IsStraight(PokerCard firstCard, PokerCard secondCard, List<PokerCard> boardCards)
        {
            return false;
        }

        private static bool IsThreesome(PokerCard firstCard, PokerCard secondCard, List<PokerCard> boardCards)
        {
            return false;
        }

        private static bool IsTwoPair(PokerCard firstCard, PokerCard secondCard, List<PokerCard> boardCards)
        {
            return false;
        }

        private static bool IsPair(PokerCard firstCard, PokerCard secondCard, List<PokerCard> boardCards)
        {
            return firstCard.Rank == secondCard.Rank;
        }
    }
}