using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
    public static class PokerPlayer
    {
        public static readonly string VERSION = "Get smarter every hour - 14:43";
        private const int AllIn = 8000;
        private const int Fold = 0;
        private const int CardScoreThreshold = 1000;
        private const int NumberOfPlayers = 8;

        public static int BetRequest(JObject jObject)
        {
            var gameState = GetParsedGameState(jObject);
            var activePlayers = GetActivePlayerCount(gameState);

            if (activePlayers > 3)
            {
                return Fold;
            }

            var cardScore = GetCardScoreFromGameState(gameState);
            return cardScore < CardScoreThreshold ? Fold : AllIn;
        }

        private static int GetActivePlayerCount(GameState gameState)
        {
            var players = gameState.players;
            var outPlayers = players.Select(player => player.status)
                .Count(status => status == Status.@out);
            return NumberOfPlayers - outPlayers;
        }

        public static void ShowDown(JObject gameState)
        {
            //TODO: Use this method to showdown
        }

        private static int GetCardScoreFromGameState(GameState gameState)
        {
            var playerId = gameState.in_action;
            var player = gameState.players.SingleOrDefault(p => p.id == playerId);

            if (player == null)
            {
                return 0;
            }

            if (player.hole_cards[0] == null || player.hole_cards[1] == null)
            {
                return 0;
            }

            var card1 = new PokerCard(player.hole_cards[0]);
            var card2 = new PokerCard(player.hole_cards[1]);
            var boardCards = new List<PokerCard>();

            return HandScoreCalculator.GetScore(card1, card2, boardCards);
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