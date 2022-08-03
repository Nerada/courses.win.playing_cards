using PlayingCards.Models;
using PlayingCards.ViewModels;

namespace PlayingCards.tests;

public class GameViewModelT
{
    [Fact]
    public void AddPlayer()
    {
        Game          game          = new();
        GameViewModel gameViewModel = new(game);

        gameViewModel.AddPlayerCommand.Execute("Valkyrie");

        gameViewModel.Players[0].PlayerName.Should().Be("Valkyrie");
    }
}