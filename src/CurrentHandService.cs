using System.Collections.Generic;
using System.Linq;

namespace Nancy.Simple
{
    public static class CurrentHandService
    {
        public static Hand GetHand(PokerCard firstCard, PokerCard secondCard, IList<PokerCard> communityCards)
        {
            var allCards = new List<PokerCard>(communityCards) {firstCard, secondCard};
            return GetHand(allCards);
        }

        public static Hand GetHand(IList<PokerCard> cards)
        {
            var cardsByRank = cards.ToLookup(c => c.Rank);
            var cardsBySuit = cards.ToLookup(c => c.Suit);
            var countByRank = cardsByRank.ToDictionary(x => x.Key, x => x.Count());
            var countBySuit = cardsBySuit.ToDictionary(x => x.Key, x => x.Count());

            // Royal Flush doesn't happen anyway :D

            if (cardsBySuit.Any(g => IsStraight(g.ToList())))
            {
                return Hand.StraightFlush;
            }

            if (cardsByRank.Any(g => g.Count() >= 4))
            {
                return Hand.FourOfKind;
            }

            var hasThreeOfAKind = countByRank.Any(g => g.Value >= 3);
            var hasTwoPairs = countByRank.Count(g => g.Value >= 2) >= 2;
            if (hasThreeOfAKind && hasTwoPairs)
            {
                return Hand.FullHouse;
            }

            if (countBySuit.Any(g => g.Value >= 5))
            {
                return Hand.Flush;
            }

            if (IsStraight(cards))
            {
                return Hand.Straight;
            }

            if (hasThreeOfAKind)
            {
                return Hand.ThreeOfAKind;
            }

            if (hasTwoPairs)
            {
                return Hand.TwoPair;
            }

            if (countByRank.Any(g => g.Value >= 2))
            {
                return Hand.OnePair;
            }

            return Hand.HighCard;
        }

        public static bool IsStraight(IList<PokerCard> cards)
        {
            var distinctRanks = cards.Select(c => c.Rank).Distinct().OrderByDescending(x => x).ToList();
            if (distinctRanks.Contains(14)) // ace
            {
                distinctRanks.Add(1);
            }

            for (var startIndex = 0; startIndex <= distinctRanks.Count - 5; startIndex++)
            {
                for (var straightLength = 1;; straightLength++)
                {
                    var diff = distinctRanks[startIndex + straightLength - 1] -
                               distinctRanks[startIndex + straightLength];
                    if (diff != 1)
                    {
                        break;
                    }

                    if (straightLength + 1 == 5)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}