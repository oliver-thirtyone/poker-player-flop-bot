using System.Collections.Generic;
using NUnit.Framework;

namespace Nancy.Simple
{
    public class CurrentHandServiceTest
    {
        private const int Ace = 14;

        [Test]
        public void GetHand_StraightFlush()
        {
            var setup = new Setup()
                .WithCards(Suit.clubs, 8, Ace)
                .WithCards(Suit.spades, 9)
                .WithCards(Suit.diamonds, 3, 4, 5, 6, 7);

            var actual = CurrentHandService.GetHand(setup.cards);

            Assert.That(actual, Is.EqualTo(Hand.StraightFlush));
        }

        [Test]
        public void GetHand_FourOfKind()
        {
            var setup = new Setup()
                .WithCards(Suit.clubs, 2, Ace)
                .WithCards(Suit.hearts, 2, 4)
                .WithCards(Suit.spades, 2)
                .WithCards(Suit.diamonds, 2, 4, 5, 6);

            var actual = CurrentHandService.GetHand(setup.cards);

            Assert.That(actual, Is.EqualTo(Hand.FourOfKind));
        }

        [Test]
        public void GetHand_FullHouse()
        {
            var setup = new Setup()
                .WithCards(Suit.clubs, 2, 3, Ace)
                .WithCards(Suit.hearts, 2, 3)
                .WithCards(Suit.diamonds, 2, 4, 5, 6);

            var actual = CurrentHandService.GetHand(setup.cards);

            Assert.That(actual, Is.EqualTo(Hand.FullHouse));
        }

        [Test]
        public void GetHand_Flush()
        {
            var setup = new Setup()
                .WithCards(Suit.clubs, 2, 3, Ace)
                .WithCards(Suit.diamonds, 2, 3, 4, 5, 9);

            var actual = CurrentHandService.GetHand(setup.cards);

            Assert.That(actual, Is.EqualTo(Hand.Flush));
        }

        [Test]
        public void GetHand_Straight()
        {
            var setup = new Setup()
                .WithCards(Suit.clubs, 2, 3, Ace)
                .WithCards(Suit.diamonds, 3, 4, 5, 6);

            var actual = CurrentHandService.GetHand(setup.cards);

            Assert.That(actual, Is.EqualTo(Hand.Straight));
        }

        [Test]
        public void GetHand_ThreeOfAKind()
        {
            var setup = new Setup()
                .WithCards(Suit.clubs, 2, 8)
                .WithCards(Suit.spades, 2)
                .WithCards(Suit.diamonds, 2, 3, 4, 5);

            var actual = CurrentHandService.GetHand(setup.cards);

            Assert.That(actual, Is.EqualTo(Hand.ThreeOfAKind));
        }

        [Test]
        public void GetHand_TwoPair()
        {
            var setup = new Setup()
                .WithCards(Suit.clubs, 2, 3)
                .WithCards(Suit.diamonds, 2, 3, 4, 5);

            var actual = CurrentHandService.GetHand(setup.cards);

            Assert.That(actual, Is.EqualTo(Hand.TwoPair));
        }

        [Test]
        public void GetHand_OnePair()
        {
            var setup = new Setup()
                .WithCards(Suit.clubs, 2)
                .WithCards(Suit.diamonds, 2, 3, 4, 5);

            var actual = CurrentHandService.GetHand(setup.cards);

            Assert.That(actual, Is.EqualTo(Hand.OnePair));
        }

        [Test]
        public void GetHand_HighCard()
        {
            var setup = new Setup()
                .WithCards(Suit.clubs, 8, 9)
                .WithCards(Suit.diamonds, 2, 3, 4, 5);

            var actual = CurrentHandService.GetHand(setup.cards);

            Assert.That(actual, Is.EqualTo(Hand.HighCard));
        }

        [Test]
        public void IsStraight_2To6_Yes()
        {
            var setup = new Setup(2, 3, 4, 5, 6);

            var actual = CurrentHandService.IsStraight(setup.cards);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void IsStraight_AceTo5_Yes()
        {
            var setup = new Setup(2, 3, 4, 5, Ace);

            var actual = CurrentHandService.IsStraight(setup.cards);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void IsStraight_2To5AndGap_No()
        {
            var setup = new Setup(2, 3, 4, 5, 8);

            var actual = CurrentHandService.IsStraight(setup.cards);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void IsStraight_NoCard_No()
        {
            var setup = new Setup();

            var actual = CurrentHandService.IsStraight(setup.cards);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void IsStraight_SingleCard_No()
        {
            var setup = new Setup(5);

            var actual = CurrentHandService.IsStraight(setup.cards);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void IsStraight_StraightInMiddle_Yes()
        {
            var setup = new Setup(3, 3, 5, 6, 7, 7, 8, 9, 11, 11);

            var actual = CurrentHandService.IsStraight(setup.cards);

            Assert.That(actual, Is.True);
        }

        private class Setup
        {
            public readonly List<PokerCard> cards = new List<PokerCard>();

            public Setup(params int[] ranks)
            {
                WithCards(Suit.clubs, ranks);
            }

            public Setup WithCards(Suit suit, params int[] ranks)
            {
                foreach (var rank in ranks)
                {
                    cards.Add(new PokerCard(rank, suit));
                }

                return this;
            }
        }
    }
}