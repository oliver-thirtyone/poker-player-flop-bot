using System.IO;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Nancy.Simple
{
    public class PokerPlayerTest
    {
        [Test]
        public void bet_0_if_there_are_more_than_3_active_players()
        {
            var gameState = CreateGameState("../test/six_active_players.json");

            var betRequest = PokerPlayer.BetRequest(gameState);
            Assert.That(betRequest, Is.EqualTo(0));
        }
        
        [Test]
        public void bet_8000_if_there_are_3_active_players_and_we_have_high_score()
        {
            var gameState = CreateGameState("../test/three_active_players_with_high_cards.json");

            var betRequest = PokerPlayer.BetRequest(gameState);
            Assert.That(betRequest, Is.EqualTo(8000));
        }

        [Test]
        public void always_fold_if_we_have_cards_with_low_score()
        {
            var gameState = CreateGameState("../test/three_active_players_with_low_cards.json");

            var betRequest = PokerPlayer.BetRequest(gameState);
            Assert.That(betRequest, Is.EqualTo(0));
        }

        [Test]
        public void call_if_we_have_high_cards_to_see_the_community_cards()
        {
            var gameState = CreateGameState("../test/three_active_players_without_community_cards.json");
            const int currentBuyIn = 70;

            var betRequest = PokerPlayer.BetRequest(gameState);
            Assert.That(betRequest, Is.EqualTo(currentBuyIn));
        }
        
        [Test]
        public void go_all_in_with_high_cards_without_seeing_the_community_cards()
        {
            var gameState = CreateGameState("../test/three_active_players_without_community_cards_and_high_cards.json");

            var betRequest = PokerPlayer.BetRequest(gameState);
            Assert.That(betRequest, Is.EqualTo(8000));
        }

        private JObject CreateGameState(string fileName = "../test/test_dummy.json")
        {
            var json = File.ReadAllText(fileName);
            return JObject.Parse(json);
        }
    }
}