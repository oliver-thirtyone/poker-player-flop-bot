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

        private const int twinScore = 1000;
        private const int boardMatchScore = 1000;
        private const int highCardScore = 200;

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
                    return boardMatchScore;
                }
            }
            return 0;
        }

        private int GetHighCardScore()
        {
            var score = 0;

            if (FirstCard.rank > 10)
            {
                score += highCardScore;
            }

            if (SecondCard.rank > 10)
            {
                score += highCardScore;
            }

            return score;
        }





    }
}
