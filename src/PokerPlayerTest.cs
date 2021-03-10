using System.IO;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Nancy.Simple
{
    public class PokerPlayerTest
    {
        [Test]
        public void bet_0_in_round_0()
        {
            var gameState = CreateGameState();
            var betRequest = PokerPlayer.BetRequest(gameState);
            Assert.That(betRequest, Is.EqualTo(0));
        }

        [Test]
        public void bet_5000_in_round_21()
        {
            var gameState = CreateGameState();
            gameState["round"] = 21;

            var betRequest = PokerPlayer.BetRequest(gameState);
            Assert.That(betRequest, Is.EqualTo(5000));
        }
        
        [Test]
        public void bet_0_if_there_are_more_than_2_active_players()
        {
            var gameState = CreateGameState("../test/six_active_players.json");

            var betRequest = PokerPlayer.BetRequest(gameState);
            Assert.That(betRequest, Is.EqualTo(0));
        }

        private JObject CreateGameState(string fileName = "../test/test_dummy.json")
        {
            var json = File.ReadAllText(fileName);
            return JObject.Parse(json);
        }
    }
}