using PlayingCards.Models;

namespace PlayingCards.tests;

public class GameT
{
    [Fact]
    public void WinOnHighestHand()
    {
        Player player1 = new("Loki");
        Player player2 = new("Thor");

        player1.GiveCards("2C 3C");
        player2.GiveCards("2H 4H");

        Game game = new();
        game.AddPlayer(player1);
        game.AddPlayer(player2);

        game.GiveCards("3H 4C QS KC AD");

        game.Result().Count.Should().Be(2);

        //PlayerB has the highest pair
        game.Result().Count(r => r.result.Hand        == Outcome.Result.Draw).Should().Be(2);
        game.Result().Count(r => r.result.HighestHand == Outcome.Result.Wins).Should().Be(1);
    }

    [Fact]
    public void DrawOnHighestHand()
    {
        // ReSharper disable once StringLiteralTypo
        Player player1 = new("Skadi");
        Player player2 = new("Thor");
        Player player3 = new("Freya");

        player1.GiveCards("2C 3C");
        player2.GiveCards("2H 3H");
        player3.GiveCards("2D 3D");

        Game game = new();
        game.AddPlayer(player1);
        game.AddPlayer(player2);
        game.AddPlayer(player3);

        game.GiveCards("3S KD KC 6S 8H");

        game.Result().Count(r => r.result.Hand        == Outcome.Result.Draw).Should().Be(3);
        game.Result().Count(r => r.result.HighestHand == Outcome.Result.Draw).Should().Be(3);
    }
}