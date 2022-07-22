using FluentAssertions;
using PlayingCards.Models;

namespace PlayingCards.tests;

public class PlayingCardsT
{
    [Fact]
    public void TestHighestCard()
    {
        Player player = new(string.Empty);

        player.AddCards("2H 3D 5S 9C KD 8C");

        player.HighestCard().Value.Should().Be("K");
        player.HighestCard().Suit.Should().Be(Suit.Diamonds);
    }

    [Theory]
    [InlineData("2H 3D 5S 9C QD", "PlayerA", "2C 3H 4S 8C KH", "PlayerB")]
    public void HighestCardForPlayer(string cardsPlayerA, string namePlayerA, string cardsPlayerB, string namePlayerB)
    {
        Player player1 = new(namePlayerA);
        Player player2 = new(namePlayerB);

        player1.AddCards(cardsPlayerA);
        player2.AddCards(cardsPlayerB);

        Game.Play(player1, player2).Should().Be(Game.Outcome.Wins);
    }
}