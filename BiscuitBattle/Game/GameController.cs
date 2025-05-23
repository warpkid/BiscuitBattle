using BiscuitBattle.Extensions;
using BiscuitBattle.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BiscuitBattle.Store.Decks;

namespace BiscuitBattle.Game
{
    public class GameController : IGameController
    {
        public IList<IBiscuitCard> PlayerOneDeck { get; set; }
        public IList<IBiscuitCard> PlayerTwoDeck { get; set; }

        public IPlayer PlayerOne { get; set; }
        public IPlayer PlayerTwo { get; set; }

        // Made PlayerStates and turnCounter class members for PlaySingleRound to access
        private IList<PlayerState> PlayerStates { get; set; }
        private int turnCounter; 

        // Public method for testing individual rounds
        public void PlaySingleRound()
        {
            if (PlayerStates == null || !PlayerStates.Any())
            {
                // Initialize PlayerStates if Begin() hasn't been called (e.g. for single round testing)
                // This setup is crucial for direct calls to PlaySingleRound in tests.
                PlayerStates = new List<PlayerState>
                {
                    new PlayerState() { Player = PlayerOne, Deck = PlayerOneDeck, Label = "Player One" },
                    new PlayerState() { Player = PlayerTwo, Deck = PlayerTwoDeck, Label = "Player Two" }
                };
                // turnCounter should also be initialized if not already.
                // For single round tests, turnCounter = 1 is a typical start.
                if (turnCounter == 0) turnCounter = 1;
            }
            
            // Ensure decks have cards before playing a round
            if (!PlayerStates.All(state => state.Deck.Any())) {
                Console.WriteLine("Cannot play a round, one or both decks are empty.");
                return;
            }

            //take card from top of each deck
            PlayerStates[0].CurrentCard = PlayTopCard(PlayerStates[0].Deck);
            PlayerStates[1].CurrentCard = PlayTopCard(PlayerStates[1].Deck);

            //determine whose go it is
            var activePlayer = PlayerStates[turnCounter % 2 > 0 ? 0 : 1];
            var defendingPlayer = PlayerStates[turnCounter % 2 > 0 ? 1 : 0];

            //compare
            var (attr, score) = activePlayer.Player.DecideAttributeToPlay(activePlayer.CurrentCard);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"{activePlayer.Label} played {activePlayer.CurrentCard.Name}");
            Console.WriteLine($"{defendingPlayer.Label} played {defendingPlayer.CurrentCard.Name}");
            Console.WriteLine($"{activePlayer.Label} chose to play {attr} with a score of {score}");
            var comparerScore = defendingPlayer.CurrentCard.Stats[attr];

            if (comparerScore < score)
            {
                Console.WriteLine($"{ activePlayer.Label} WINS ROUND with a attribute score of {score} compared to {comparerScore}");
                activePlayer.Deck.Insert(0, defendingPlayer.CurrentCard);
                defendingPlayer.Deck.Remove(defendingPlayer.CurrentCard);
                activePlayer.Deck.Insert(0, activePlayer.CurrentCard); // Add winner's played card to bottom
            }
            else if (comparerScore > score)
            {
                Console.WriteLine($"{ defendingPlayer.Label} WINS ROUND with a score of {comparerScore} compared to {score}" );
                defendingPlayer.Deck.Insert(0, activePlayer.CurrentCard);
                activePlayer.Deck.Remove(activePlayer.CurrentCard);
                defendingPlayer.Deck.Insert(0, defendingPlayer.CurrentCard); // Add winner's played card to bottom
            }
            else
            {
                Console.WriteLine("Draw");
                activePlayer.Deck.Insert(0, activePlayer.CurrentCard); // Add player one's card to bottom
                defendingPlayer.Deck.Insert(0, defendingPlayer.CurrentCard); // Add player two's card to bottom
            }
            Console.WriteLine(Environment.NewLine);
            // turnCounter is incremented in Begin() loop
        }

        public void Begin()
        {
            PlayerStates = new List<PlayerState>
            {
                new PlayerState() { Player = PlayerOne, Deck = PlayerOneDeck, Label = "Player One" },
                new PlayerState() { Player = PlayerTwo, Deck = PlayerTwoDeck, Label = "Player Two" }
            };

            turnCounter = 1; // Initialize turn counter

            while (PlayerStates.All(state => state.Deck.Count() != 0))
            {
                PlaySingleRound(); // Call the extracted round logic

                turnCounter++;
                Console.WriteLine("Current Deck Status: " + String.Join(",", PlayerStates.Select(p => p.Deck.Count())));
                Console.WriteLine(Environment.NewLine);
            }

            Console.WriteLine($"The Winner is: {PlayerStates.First(player => player.Deck.Any()).Label}");
            // Console.ReadKey(); // Removed for testability
        }

        private IBiscuitCard PlayTopCard(IList<IBiscuitCard> deck)
        {
            var card = deck[deck.Count() - 1];
            deck.RemoveAt(deck.Count() - 1);
            return card;
        }

        private void MoveCardToBottom(IList<IBiscuitCard> deck)
        {
            var top = deck.Count() - 1;
            var c = deck[top];
            deck.RemoveAt(top);
            deck.Insert(0, c);
        }



    }
}
