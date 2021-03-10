using System.Collections.Generic;

namespace Nancy.Simple
{
    public class GameState
    {
        public string tournament_id { get; set; } // Id of the current tournament

        public string game_id { get; set; } // Id of the current sit'n'go game. You can use this to link a
        // sequence of game states together for logging purposes, or to
        // make sure that the same strategy is played for an entire game

        public int round { get; set; } // Index of the current round within a sit'n'go

        public int bet_index { get; set; } // Index of the betting opportunity within a round

        public int small_blind { get; set; } // The small blind in the current round. The big blind is twice the
        //     small blind

        public int current_buy_in { get; set; } // The amount of the largest current bet from any one player

        public int pot { get; set; } // The size of the pot (sum of the player bets)

        public int minimum_raise { get; set; } // Minimum raise amount. To raise you have to return at least:
        //     current_buy_in - players[in_action][bet] + minimum_raise

        public int dealer { get; set; } // The index of the player on the dealer button in this round
        //     The first player is (dealer+1)%(players.length)

        public int orbits { get; set; } // Number of orbits completed. (The number of times the dealer
        //     button returned to the same player.)

        public int in_action { get; set; } // The index of your player, in the players array
        public List<Player> players { get; set; }
        public List<Card> community_cards { get; set; }
    }
}