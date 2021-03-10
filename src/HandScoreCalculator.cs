using System.Collections.Generic;
using System.Linq;

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
        private const int OnePair = 1000;

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
                case Hand.OnePair: return OnePair;
                case Hand.HighCard: return SumCardRanks(firstCard, secondCard);
                default: return SumCardRanks(firstCard, secondCard);
            }
        }

        public static int GetScore(IList<PokerCard> cards)
        {
            var hand = CurrentHandService.GetHand(cards);
            
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
                case Hand.OnePair: return OnePair;
                case Hand.HighCard: return cards.Sum(c => c.Rank);
                default: return cards.Sum(c => c.Rank);
            }
        }

        private static int SumCardRanks(PokerCard firstCard, PokerCard secondCard)
        {
            return firstCard.Rank + secondCard.Rank;
        }
    }
}