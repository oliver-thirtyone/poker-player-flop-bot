using System.Collections.Generic;
using NUnit.Framework;

namespace Nancy.Simple
{
    public class HandScoreCalculatorTest
    {
        [Test]
        public void score_is_1000_if_we_have_a_pair()
        {
            var card1 = new PokerCard("K", Suit.clubs);
            var card2 = new PokerCard("K", Suit.hearts);
            var boardCards = new List<PokerCard>();

            var score = HandScoreCalculator.GetScore(card1, card2, boardCards);
            Assert.That(score, Is.EqualTo(1000));
        }

        [Test]
        public void score_is_500_if_we_have_a_low_pair()
        {
            var card1 = new PokerCard("6", Suit.clubs);
            var card2 = new PokerCard("6", Suit.hearts);
            var boardCards = new List<PokerCard>();

            var score = HandScoreCalculator.GetScore(card1, card2, boardCards);
            Assert.That(score, Is.EqualTo(500));
        }

        [Test]
        public void score_is_the_sum_of_ranks()
        {
            var card1 = new PokerCard("10", Suit.clubs);
            var card2 = new PokerCard("9", Suit.hearts);
            var boardCards = new List<PokerCard>();

            var score = HandScoreCalculator.GetScore(card1, card2, boardCards);
            Assert.That(score, Is.EqualTo(19));
        }
    }
}