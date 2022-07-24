using FluentAssertions;
using PlayingCards.Models;

namespace PlayingCards.tests;

public class GameT
{
    [Fact]
    public void HighestPair()
    {
        Player player1 = new("PlayerA");
        Player player2 = new("PlayerB");

        player1.GiveCards("2H 3D 2S 9C KD");
        player2.GiveCards("7C 7H 4S 8C 6H");

        Game game = new();
        game.AddPlayer(player1);
        game.AddPlayer(player2);

        game.Result().players.Count.Should().Be(1);

        //PlayerB has the highest pair
        game.Result().players[0].PlayerName.Should().Be("PlayerB");
    }
}