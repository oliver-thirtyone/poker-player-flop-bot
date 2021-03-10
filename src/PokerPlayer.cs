using System.Linq;
using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
    public static class PokerPlayer
    {
        public static readonly string VERSION = "Äxgüsi, häts scho agfange?";

        public static int BetRequest(JObject gameState)
        {
            var players = gameState["players"].Children();
            var activePlayers = players.Select(player => player["status"].ToObject<string>())
                .Count(status => status == "active");

            return activePlayers > 3 ? 0 : 8000;
        }

        public static void ShowDown(JObject gameState)
        {
            //TODO: Use this method to showdown
        }
    }
}