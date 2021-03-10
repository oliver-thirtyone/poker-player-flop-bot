using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
	public static class PokerPlayer
	{
		public static readonly string VERSION = "Äxgüsi, häts scho agfange?";

		public static int BetRequest(JObject gameState)
		{
			var round = gameState["round"].ToObject<int>();
			return round > 20 ? 5000 : 0;
		}

		public static void ShowDown(JObject gameState)
		{
			//TODO: Use this method to showdown
		}
	}
}

