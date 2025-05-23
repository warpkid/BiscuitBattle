using BiscuitBattle.Players;
using BiscuitBattle.Game;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using BiscuitBattle.Store.Decks; // Using official IBiscuitCard and BiscuitAttribute

namespace BiscuitBattle.Tests
{
    [TestFixture]
    public class GameLogicTests
    {
        public class MockBiscuitCard : IBiscuitCard
        {
            public string Name { get; set; }
            public string Description { get; set; }
            // Matches official IBiscuitCard: Dictionary, not IDictionary
            public Dictionary<BiscuitAttribute, int> Stats { get; set; } = new Dictionary<BiscuitAttribute, int>();
            // Added to match official IBiscuitCard
            public int Quality { get; set; } // Implemented as get; set; for test flexibility
        }

        public class MockPlayer : IPlayer
        {
            private readonly BiscuitAttribute _attributeToPlay;

            public MockPlayer(BiscuitAttribute attributeToPlay)
            {
                _attributeToPlay = attributeToPlay;
            }

            // Updated tuple element names to match IPlayer interface (Attr, Score)
            public (BiscuitAttribute Attr, int Score) DecideAttributeToPlay(IBiscuitCard card)
            {
                if (card.Stats.TryGetValue(_attributeToPlay, out int scoreValue))
                {
                    return (_attributeToPlay, scoreValue);
                }
                return (_attributeToPlay, 0); 
            }
        }

        private GameController _gameController;

        [SetUp]
        public void Setup()
        {
            _gameController = new GameController();
        }

        [Test]
        public void TestCardMovementOnWin()
        {
            // Using actual BiscuitAttribute enum members
            var playerOne = new MockPlayer(BiscuitAttribute.Snap); 
            var playerTwo = new MockPlayer(BiscuitAttribute.Snap); 

            var cardP1 = new MockBiscuitCard { Name = "P1Card", Quality = 100, Stats = new Dictionary<BiscuitAttribute, int> { { BiscuitAttribute.Snap, 10 } } };
            var cardP2 = new MockBiscuitCard { Name = "P2Card", Quality = 90, Stats = new Dictionary<BiscuitAttribute, int> { { BiscuitAttribute.Snap, 5 } } };

            _gameController.PlayerOne = playerOne;
            _gameController.PlayerTwo = playerTwo;
            _gameController.PlayerOneDeck = new List<IBiscuitCard> { cardP1 };
            _gameController.PlayerTwoDeck = new List<IBiscuitCard> { cardP2 };

            _gameController.PlaySingleRound();

            Assert.AreEqual(2, _gameController.PlayerOneDeck.Count, "Player One should have 2 cards.");
            Assert.AreEqual(0, _gameController.PlayerTwoDeck.Count, "Player Two should have 0 cards.");
            
            Assert.AreSame(cardP1, _gameController.PlayerOneDeck[0], "Player One's original card (cardP1) should be at the bottom (index 0).");
            Assert.AreSame(cardP2, _gameController.PlayerOneDeck[1], "Player Two's card (cardP2) should be at the top (index 1) of Player One's deck.");
        }

        [Test]
        public void TestCardMovementOnLoss()
        {
            var playerOne = new MockPlayer(BiscuitAttribute.Texture); 
            var playerTwo = new MockPlayer(BiscuitAttribute.Texture); 

            var cardP1 = new MockBiscuitCard { Name = "P1Card", Quality = 80, Stats = new Dictionary<BiscuitAttribute, int> { { BiscuitAttribute.Texture, 3 } } };
            var cardP2 = new MockBiscuitCard { Name = "P2Card", Quality = 70, Stats = new Dictionary<BiscuitAttribute, int> { { BiscuitAttribute.Texture, 8 } } };

            _gameController.PlayerOne = playerOne;
            _gameController.PlayerTwo = playerTwo;
            _gameController.PlayerOneDeck = new List<IBiscuitCard> { cardP1 };
            _gameController.PlayerTwoDeck = new List<IBiscuitCard> { cardP2 };

            _gameController.PlaySingleRound();

            Assert.AreEqual(0, _gameController.PlayerOneDeck.Count, "Player One should have 0 cards.");
            Assert.AreEqual(2, _gameController.PlayerTwoDeck.Count, "Player Two should have 2 cards.");

            Assert.AreSame(cardP2, _gameController.PlayerTwoDeck[0], "Player Two's original card (cardP2) should be at the bottom (index 0).");
            Assert.AreSame(cardP1, _gameController.PlayerTwoDeck[1], "Player One's card (cardP1) should be at the top (index 1) of Player Two's deck.");
        }

        [Test]
        public void TestCardMovementOnDraw()
        {
            var playerOne = new MockPlayer(BiscuitAttribute.DunkIntegrity);
            var playerTwo = new MockPlayer(BiscuitAttribute.DunkIntegrity);

            var cardP1 = new MockBiscuitCard { Name = "P1Card", Quality = 60, Stats = new Dictionary<BiscuitAttribute, int> { { BiscuitAttribute.DunkIntegrity, 7 } } };
            var cardP2 = new MockBiscuitCard { Name = "P2Card", Quality = 65, Stats = new Dictionary<BiscuitAttribute, int> { { BiscuitAttribute.DunkIntegrity, 7 } } };

            _gameController.PlayerOne = playerOne;
            _gameController.PlayerTwo = playerTwo;
            _gameController.PlayerOneDeck = new List<IBiscuitCard> { cardP1 };
            _gameController.PlayerTwoDeck = new List<IBiscuitCard> { cardP2 };

            _gameController.PlaySingleRound();
            
            Assert.AreEqual(1, _gameController.PlayerOneDeck.Count, "Player One should still have 1 card after a draw.");
            Assert.AreEqual(1, _gameController.PlayerTwoDeck.Count, "Player Two should still have 1 card after a draw.");
            Assert.AreSame(cardP1, _gameController.PlayerOneDeck[0], "Player One's card should be at the bottom (and only card).");
            Assert.AreSame(cardP2, _gameController.PlayerTwoDeck[0], "Player Two's card should be at the bottom (and only card).");
        }

        [Test]
        public void TestGameEndCondition()
        {
            var playerOne = new MockPlayer(BiscuitAttribute.Sweetness);
            var playerTwo = new MockPlayer(BiscuitAttribute.Sweetness);

            var cardP1 = new MockBiscuitCard { Name = "P1CardWin", Quality = 95, Stats = new Dictionary<BiscuitAttribute, int> { { BiscuitAttribute.Sweetness, 10 } } };
            var cardP2 = new MockBiscuitCard { Name = "P2CardLose", Quality = 85, Stats = new Dictionary<BiscuitAttribute, int> { { BiscuitAttribute.Sweetness, 5 } } };

            _gameController.PlayerOne = playerOne;
            _gameController.PlayerTwo = playerTwo;
            _gameController.PlayerOneDeck = new List<IBiscuitCard> { cardP1 };
            _gameController.PlayerTwoDeck = new List<IBiscuitCard> { cardP2 };

            _gameController.Begin(); 

            Assert.AreEqual(2, _gameController.PlayerOneDeck.Count, "Player One should have 2 cards (winning the game).");
            Assert.AreEqual(0, _gameController.PlayerTwoDeck.Count, "Player Two should have 0 cards (losing the game).");
        }
    }
}
