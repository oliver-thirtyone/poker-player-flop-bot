using System.Collections.Generic;
using System.Linq;

namespace Nancy.Simple
{
    public static class CurrentHandService
    {
        public static Hand GetHand(PokerCard firstCard, PokerCard secondCard, List<PokerCard> communityCards)
        {
            var allCards = new List<PokerCard>(communityCards);
            allCards.Add(firstCard);
            allCards.Add(secondCard);
            return GetHand(allCards);
        }

        public static Hand GetHand(List<PokerCard> cards)
        {
            var orderedCards = cards
                .OrderByDescending(c => c.Rank)
                .ToList();

            var cardsByRank = orderedCards.ToLookup(c => c.Rank);
            var cardsBySuit = orderedCards.ToLookup(c => c.Suit);
            var countByRank = cardsByRank.ToLookup(x => x.Key, x => cardsByRank.Count());
            var countBySuit = cardsBySuit.ToLookup(x => x.Key, x => cardsBySuit.Count());

            if (IsStraight(orderedCards))
            {
                return Hand.Straight;
            }

            if (countByRank.Any(g => g.Count() >= 3))
            {
                return Hand.ThreeOfAKind;
            }

            if (countByRank.Count(g => g.Count() >= 2) >= 2)
            {
                return Hand.TwoPair;
            }

            if (cardsByRank.Any(g => g.Count() >= 2))
            {
                return Hand.OnePair;
            }

            return Hand.HighCard;
        }

        public static bool IsStraight(List<PokerCard> orderedCards)
        {
            var distinctRanks = orderedCards.Select(c => c.Rank).Distinct().ToList();
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