using System.Collections.Generic;

namespace Nancy.Simple
{
    public static class HandScoreCalculator
    {
        private const int RoyalFlush = 9000;
        private const int StraightFlush = 8000;
        private const int Foursome = 7000;
        private const int FullHouse = 6000;
        private const int Flush = 5000;
        private const int Straight = 4000;
        private const int Threesome = 3000;
        private const int TwoPair = 2000;
        private const int OnePair_High = 1000;
        private const int OnePair_Low = 500;

        private const int LowPairThreshold = 7;

        public static int GetScore(PokerCard firstCard, PokerCard secondCard, IList<PokerCard> boardCards)
        {
            var hand = CurrentHandService.GetHand(firstCard, secondCard, boardCards);

            switch (hand)
            {
                case Hand.RoyalFlush: return RoyalFlush;
                case Hand.StraightFlush: return StraightFlush;
                case Hand.FourOfKind: return Foursome;
                case Hand.FullHouse: return FullHouse;
                case Hand.Flush: return Flush;
                case Hand.Straight: return Straight;
                case Hand.ThreeOfAKind: return Threesome;
                case Hand.TwoPair: return TwoPair;
                case Hand.OnePair: return CalculateLowOrHighPair(firstCard);
                case Hand.HighCard: return SumCardRanks(firstCard, secondCard);
                default: return SumCardRanks(firstCard, secondCard);
            }
        }

        private static int CalculateLowOrHighPair(PokerCard firstCard)
        {
            return firstCard.Rank > LowPairThreshold ? OnePair_High : OnePair_Low;
        }

        private static int SumCardRanks(PokerCard firstCard, PokerCard secondCard)
        {
            return firstCard.Rank + secondCard.Rank;
        }
    }
}