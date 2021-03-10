using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
	public static class PokerPlayer
	{
		public static readonly string VERSION = "Eigentlich hani scho Hunger, will es isch 11:20";

        public static int BetRequest(JObject gameState)
        {
            var parsedGameState = GetParsedGameState(gameState);

            var players = parsedGameState.players;
            var activePlayers = players.Select(player => player.status)
                .Count(status => status == Status.active);

            return activePlayers > 3 ? 0 : 8000;
        }

        public static void ShowDown(JObject gameState)
        {
            //TODO: Use this method to showdown
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