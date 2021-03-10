using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
    public static class PokerPlayer
    {
        public static readonly string VERSION = "ALL IN !!!!!!111elf - 16:52";

        private const int AllIn = 8000;
        private const int CheckOrFold = 0;
        private const int TotalScoreThreshold = 1000;
        private const int NumberOfPlayers = 8;
        private const int FirstRoundMinCardScore = 21;
        private const int FirstRoundAllInScore = 25;

        public static int BetRequest(JObject jObject)
        {
            var gameState = GetParsedGameState(jObject);
            var activePlayers = GetActivePlayerCount(gameState);

            var handCardScore = GetHandCardScore(gameState);
            if (handCardScore >= FirstRoundAllInScore)
            {
                return AllIn;
            }

            if (activePlayers > 4)
            {
                return CheckOrFold;
            }

            var numberOfCommunityCards = gameState.community_cards.Count;
            var totalScore = GetTotalScore(gameState);

            if (numberOfCommunityCards > 0)
            {
                return totalScore < TotalScoreThreshold ? CheckOrFold : AllIn;
            }

            return CheckUntilWeSeeCommunityCards(gameState, totalScore);
        }

        private static int CheckUntilWeSeeCommunityCards(GameState gameState, int cardScore)
        {
            var bigBlind = gameState.small_blind * 2;
            var currentBuyIn = gameState.current_buy_in;

            if (currentBuyIn <= bigBlind)
            {
                return cardScore >= FirstRoundMinCardScore ? currentBuyIn : CheckOrFold;
            }

            return CheckOrFold;
        }

        private static int GetHandCardScore(GameState gameState)
        {
            var player = GetCurrentPlayer(gameState);
            var cards = GetHandCards(player);

            return HandScoreCalculator.GetScore(cards);
        }

        private static int GetTotalScore(GameState gameState)
        {
            var player = GetCurrentPlayer(gameState);
            var cards = GetHandCards(player);

            var boardCards = gameState.community_cards.Select(card => new PokerCard(card)).ToList();
            foreach (var boardCard in boardCards)
            {
                cards.Add(boardCard);
            }

            return HandScoreCalculator.GetScore(cards);
        }

        private static IList<PokerCard> GetHandCards(Player player)
        {
            var cards = new List<PokerCard>();

            if (player == null)
            {
                return cards;
            }

            if (player.hole_cards[0] == null || player.hole_cards[1] == null)
            {
                return cards;
            }

            cards.Add(new PokerCard(player.hole_cards[0]));
            cards.Add(new PokerCard(player.hole_cards[1]));

            return cards;
        }

        private static int GetActivePlayerCount(GameState gameState)
        {
            var players = gameState.players;
            var outPlayers = players.Select(player => player.status)
                .Count(status => status == Status.@out);
            return NumberOfPlayers - outPlayers;
        }

        private static Player GetCurrentPlayer(GameState gameState)
        {
            var playerId = gameState.in_action;
            var player = gameState.players.SingleOrDefault(p => p.id == playerId);
            return player;
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

        public static void ShowDown(JObject gameState)
        {
            //TODO: Use this method to showdown
        }
    }
}