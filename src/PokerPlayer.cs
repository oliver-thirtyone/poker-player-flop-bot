using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
    public static class PokerPlayer
    {
        public static readonly string VERSION = "TO THE MOON 🚀🚀🚀 - 13:02";
        private const int AllIn = 8000;

        public static int BetRequest(JObject jObject)
        {
            var gameState = GetParsedGameState(jObject);

            if (IsPlayerHoldingAPair(gameState))
            {
                return AllIn;
            }

            var players = gameState.players;
            var outPlayers = players.Select(player => player.status)
                .Count(status => status == Status.@out);

            return outPlayers < 5 ? 0 : AllIn;
        }

        public static void ShowDown(JObject gameState)
        {
            //TODO: Use this method to showdown
        }

        private static int GetScoreFromGameState(GameState gameState)
        {
            var playerId = gameState.in_action;
            var player = gameState.players.SingleOrDefault(p => p.id == playerId);

            if (player == null)
            {
                return 0;
            }

            var card1 = new PokerCard(player.hole_cards[0]);
            var card2 = new PokerCard(player.hole_cards[1]);

            if (card1 == null || card2 == null)
            {
                return 0;
            }


            var PokerPlayerHandScore = new PokerPlayerHandScore(card1, card2);

            return PokerPlayerHandScore.GetScore();
        }

        private static bool IsPlayerHoldingAPair(GameState gameState)
        {
            var playerId = gameState.in_action;
            var player = gameState.players.SingleOrDefault(p => p.id == playerId);

            if (player == null)
            {
                return false;
            }

            var card1 = player.hole_cards[0];
            var card2 = player.hole_cards[1];

            return card1.rank == card2.rank;
        }

        private static GameState GetParsedGameState(JObject gameState)
        {
            try
            {
                return gameState.ToObject<GameState>();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }

            return null;
        }
    }
}