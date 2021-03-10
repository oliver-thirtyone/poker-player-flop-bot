using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nancy.Simple
{
    public class PokerCard
    {
        public int rank;

        public string suit;

        public PokerCard(int rank, string suit)
        {
            this.rank = rank;
            this.suit = suit;
        }

    }
}
