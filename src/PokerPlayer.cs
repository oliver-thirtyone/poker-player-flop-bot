using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
	public static class PokerPlayer
	{
		public static readonly string VERSION = "Äxgüsi, häts scho agfange? 2";

        public static int BetRequest(JObject gameState)
        {
            var parsedGameState = GetParsedGameState(gameState);

            var players = gameState["players"].Children();
            var activePlayers = players.Select(player => player["status"].ToObject<string>())
                .Count(status => status == "active");

            return activePlayers > 3 ? 0 : 8000;
        }

        public static void ShowDown(JObject gameState)
        {
            //TODO: Use this method to showdown
        }

        private static object GetParsedGameState(JObject gameState)
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