# Playing Cards [![Build Status](https://dev.azure.com/nerada/GitHub_Public/_apis/build/status/Nerada.courses.win.playing_cards?branchName=master)](https://dev.azure.com/nerada/GitHub_Public/_build/latest?definitionId=21&branchName=master)
Practicing test driven development together with the team.

## Details
http://www.tddbuddy.com/katas/Poker%20Hands.pdf

#### Playing Cards TDD Kata<br />
-----
1. Write a function that takes a string representing 5 playing cards and returns the highest scoring card.
   - Input 5 space delimited strings
   - Each string is comprised of two parameters:
     - value 2,3,4,5,6,7,8,9,10,J,Q,K,A
	 - suit	C,D,H,S
   - e.g. <br />
     2C = Two of clubs<br />
	 QS = Queen of spades<br />
   - "2H 3D 5S 9C KD" should result in the KD card<br />
-----
2. Create a function that takes two players and reports the winner and their winning card. The winner is the player with highest card.<br />
   - e.g. "2H 3D 5S 9C QD", "2C 3H 4S 8C KH"	-> "2C 3H 4S 8C KH", "KH"<br />
-----
3. Expand the solution to calculate and evaluate winning hands based on poker hands<br />
High card, Pair, Two Pair, Three of a kind, Straight, Flush, Full house, Four of a kind, Straight flush, Royal flush<br />
e.g.<br />

| Player 1 | Player 2 | Result |
| --- | --- | --- |
| 2H 3D 5S 9C KD | 2C 3H 4S 8C AH | Player 1 wins - with high card: Ace |
| 2H 4S 4C 2D 4H | 2S 8S AS QS 3S | Player 2 wins - with full house: 4 over 2 |
| 2H 3D 5S 9C QD | 2C 3H 4S 8C KH | Player 1 wins - with high card: king |
| 2H 3D 5S 9C KD | 2D 3H 5C 9S KH | Tie |

-----
4. Expand the game to support 2-8 players<br />
-----
### ChangeLog
0.1.0 First working version<br />
