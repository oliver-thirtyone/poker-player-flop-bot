﻿using System.IO;
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
        public void bet_8000_if_there_are_3_active_players()
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