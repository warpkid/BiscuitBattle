# Charlie McCabe's Submission

## Questions

1. The game is currently two player only. How might you extend it to make it support more than one player? This would naturally mean changing the rules of Top Trumps a bit. Feel free to explain this in text. There is no need to write code.

ANS: 
- Create another AI (either full new class or generate new instance of BasicAI)
- Add them to the deck generation in Program.cs & PlayerStates in GameController.cs
- Compare scores across all three players each turn, highest score wins the card
- Re-write the code that determines who's go it is to incorporate a third player

2. What pattern is used to create the decks of cards for each player? Is this the best way?

ANS:
- Factory Pattern?
- A for loop runs in the Program.cs file calling GetCard() for both the Human & AI decks.
- GetCard(), defined in RandomBiscuitCardCreator.cs is selecting a random card from the whole selection each time, this will result in duplicates in the decks. 
- To avoid this, all cards could be pushed to an array, which is then shuffled and cards then distributed evenly amongst players. This will require more biscuits to ensure full decks for each player, however will ensure players don't have the same cards as each other.

3. A Service Locator is used to load dependencies at run time. How else might you do this?
Think about dependency injection (or Google it!)

ANS:
- Isn't this service locator overkill? 
- As we are only calling 3 classes (GameController, BasicAI, RandomBiscuitCardCreator), can we not just call them directly in Program.cs?
- var controller = new Controller();
- I am not 100% sure on this

4. How might you modify the gameplay to make it more interesting?

ANS:
- Once per game the round loser can request to keep the card they have just lost in place of another random one from their deck (eg. they just lost their best card, give another one up instead)
- Add more players
- Opt out of a turn once per game (one pass allowed if you know you are going to lose that round)
- Build a proper ui, making it more accessible to non-technical people.

## Tasks

Pick any of the following THREE tasks to complete. You do not need to do them all.

1. Add a new biscuit type of your choosing to the game.
a. We’d like you to create a new type of your choosing and all the required classes.

REF:
- created Custard Cream - BiscuitBattleStore/CustardCreme.cs
- added reference to Custard Cream in BiscuitBattleStore/RandomBiscuitCardCreator.cs for the switch/case statement
- updated min/max for random index generator to allow for the additional biscuit in BiscuitBattleStore/RandomBiscuitCardCreator.cs

2. Create a Biscuit Builder. Currently the name and attributes of biscuits are predetermined at the point of creation. For example a Digestive biscuit currently has the stats Texture: 60, Moistness: 20, Snap: 40, Sweetness: 60 and Texture: 60.
a. We’d like you to create a biscuit builder that will randomly generate stats from 0 to
100 for each attribute. Feel free to modify IBiscuitCard however you see fit.

REF:
- Created a new class to accoplish this in BiscuitBattleStore/RandomBiscuitBuilder.cs
- Created GenerateRandomAttribute() method to be called against each BiscuitAttribute in the constructor

b. We’d like you to create a simple name builder to generate the biscuit’s name, ideally
it will take the attributes of the biscuit into account when generating a name.

REF:
- Create GenerateBiscuitName() with a serious of if statements to evaluate the generated attributes to come up with a fitting name.
- This is called on the constructor after the BiscuitAttributes are generated.

3. Add a graphical element.
a. This question is purposefully open ended. Feel free to add a graphical element as you see fit! Think about the output of the game. How might a biscuit card store something that could be written to the console to ‘show itself’.

REF:
- Added several to make sure I got this right!
- Banner added in BiscuitBattle/Game/GameController.cs to welcome user to game
- Smiley added to BiscuitBattle/Players/Human.cs to display with each BiscuitCard
- Comment added to Both AI's whilst they think about their next move BiscuitBattle/Players/CrazyAI.cs & BiscuitBattle/Players/BasicAI.cs

4. IPlayer represents an object that can play the game. You will find two implementations in the project, one human that reads and writes to the console and one AI that picks an attribute to play. Create a new AI Player with a different personality. Maybe it picks an attribute at random (crazy AI Player). Maybe it picks the lower (generous AI Player)

REF:
- I completed a 4th task as I wasn't 100% about the generated biscuit, hope that is ok!
- Created CrazyAI at BiscuitBattle/Players/CrazyAI.cs
- Achieved random selection by counting the keys of the BiscuitAttributes (as they may change in the future)
- Then generated a random index using the key count as the max value.
- Then found key in Dictionary that matched the random index & returned result

## Thank you

I really appreciate the opportunity to try this challenge and being considered for the role. I would love to know the correct anser for question 3 if at all possible

Thank you

Charlie McCabe