using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
    public static class PokerPlayer
    {
        public static readonly string VERSION = "We need to win more - 16:19";

        private const int AllIn = 8000;
        private const int CheckOrFold = 0;
        private const int CardScoreThreshold = 1000;
        private const int NumberOfPlayers = 8;
        private const int FirstRoundMinCardScore = 21;
        private const int FirstRoundAllInScore = 25;

        public static int BetRequest(JObject jObject)
        {
            var gameState = GetParsedGameState(jObject);
            var activePlayers = GetActivePlayerCount(gameState);

            if (activePlayers > 4)
            {
                return CheckOrFold;
            }

            var numberOfCommunityCards = gameState.community_cards.Count;
            var cardScore = GetCardScoreFromGameState(gameState);

            if (numberOfCommunityCards > 0)
            {
                return cardScore < CardScoreThreshold ? CheckOrFold : AllIn;
            }

            return CheckUntilWeSeeCommunityCards(gameState, cardScore);
        }

        private static int CheckUntilWeSeeCommunityCards(GameState gameState, int cardScore)
        {
            if (cardScore >= FirstRoundAllInScore)
            {
                return AllIn;
            }

            var bigBlind = gameState.small_blind * 2;
            var currentBuyIn = gameState.current_buy_in;

            if (currentBuyIn <= bigBlind)
            {
                return cardScore >= FirstRoundMinCardScore ? currentBuyIn : CheckOrFold;
            }

            return CheckOrFold;
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
            var boardCards = gameState.community_cards.Select(card => new PokerCard(card)).ToList();

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