using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Nancy.Simple
{
    public class CurrentHandServiceTest
    {
        private const int Jack = 11;
        private const int Queen = 12;
        private const int King = 13;
        private const int Ace = 14;

        [Test]
        public static void IsStraight_2To6_Yes()
        {
            var setup = new Setup(2, 3, 4, 5, 6);

            var actual = CurrentHandService.IsStraight(setup.GetOrderedCards());

            Assert.That(actual, Is.True);
        }

        [Test]
        public static void IsStraight_AceTo5_Yes()
        {
            var setup = new Setup(2, 3, 4, 5, Ace);

            var actual = CurrentHandService.IsStraight(setup.GetOrderedCards());

            Assert.That(actual, Is.True);
        }

        [Test]
        public static void IsStraight_2To5AndGap_No()
        {
            var setup = new Setup(2, 3, 4, 5, 8);

            var actual = CurrentHandService.IsStraight(setup.GetOrderedCards());

            Assert.That(actual, Is.False);
        }

        [Test]
        public static void IsStraight_NoCard_No()
        {
            var setup = new Setup();

            var actual = CurrentHandService.IsStraight(setup.GetOrderedCards());

            Assert.That(actual, Is.False);
        }

        [Test]
        public static void IsStraight_SingleCard_No()
        {
            var setup = new Setup(5);

            var actual = CurrentHandService.IsStraight(setup.GetOrderedCards());

            Assert.That(actual, Is.False);
        }

        [Test]
        public static void IsStraight_StraightInMiddle_Yes()
        {
            var setup = new Setup(3, 3, 5, 6, 7, 7, 8, 9, 11, 11);

            var actual = CurrentHandService.IsStraight(setup.GetOrderedCards());

            Assert.That(actual, Is.True);
        }

        private class Setup
        {
            private readonly List<PokerCard> cards = new List<PokerCard>();

            public List<PokerCard> GetOrderedCards()
            {
                return cards.OrderByDescending(c => c.Rank).ToList();
            }

            public Setup(Suit suit = Suit.clubs, params int[] ranks)
            {
                Array.ForEach(ranks, r => WithCard(r, suit));
            }

            public Setup(params int[] ranks)
            {
                Array.ForEach(ranks, r => WithCard(r, Suit.clubs));
            }

            public Setup WithCard(int rank, Suit suit)
            {
                cards.Add(new PokerCard(rank, suit));
                return null;
            }
        }
    }
}