using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nancy.Simple
{
    public class PokerPlayerHandScore
    {
        private PokerCard[] PlayerHand = new PokerCard[2];

        private PokerCard[] BoardCards = new PokerCard[3];

        public PokerPlayerHandScore(PokerCard[] cards)
        {
            PlayerHand = cards;
        }



    }
}
