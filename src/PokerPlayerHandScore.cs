using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace Nancy.Simple
{
    public class PokerPlayerHandScore
    {
        private PokerCard FirstCard;

        private PokerCard SecondCard;

        private PokerCard[] BoardCards;

        private const int twinScore = 500;

        public PokerPlayerHandScore(PokerCard FirstCard, PokerCard SecondCard)
        {
            this.FirstCard = FirstCard;
            this.SecondCard = SecondCard;
        }

        public int GetScore()
        {
            int calculatedScore = 0;

            calculatedScore += GetHighCardScore();

            calculatedScore += GetTwinCardsScore();


            if (BoardCards == null)
            {
                return calculatedScore;
            }

            calculatedScore += GetBoardMatchScore();


            return calculatedScore;
        }

        public void SetBoardCards(PokerCard[] BoardCards)
        {
            this.BoardCards = BoardCards;
        }

        private int GetTwinCardsScore()
        {
            if (FirstCard.Equals(SecondCard))
            {
                return twinScore;
            }
            else return 0;
        }

        private int GetBoardMatchScore()
        {
            foreach(PokerCard boardCard in BoardCards)
            {
                if (boardCard.Equals(FirstCard) || boardCard.Equals(SecondCard))
                {
                    return 2000;
                }
            }
            return 0;
        }

        private int GetHighCardScore()
        {
            var highCardScore = 0;

            if (FirstCard.rank > 10)
            {
                highCardScore += 500;
            }

            if (SecondCard.rank > 10)
            {
                highCardScore += 500;
            }

            return highCardScore;
        }





    }
}
