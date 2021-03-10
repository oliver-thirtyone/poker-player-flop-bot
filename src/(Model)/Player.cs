using System.Collections.Generic;

namespace Nancy.Simple
{
    public class Player
    {
        public int id { get; set; } // Id of the player (same as the index)

        public string name { get; set; } // Name specified in the tournament config

        public Status status { get; set; } // Status of the player:
        //   - active: the player can make bets, and win the current pot
        //   - folded: the player folded, and gave up interest in
        //       the current pot. They can return in the next round.
        //   - out: the player lost all chips, and is out of this sit'n'go

        public string version { get; set; } // Version identifier returned by the player

        public int stack { get; set; } // Amount of chips still available for the player. (Not including
        //     the chips the player bet in this round.)

        public int bet { get; set; } // The amount of chips the player put into the pot

        public List<Card> hole_cards { get; set; } // The cards of the player. This is only visible for your own player
        //     except after showdown, when cards revealed are also included.
    }
}