using System.Windows;
using PlayingCards.Models;
using PlayingCards.ViewModels;
using PlayingCards.Views;

namespace PlayingCards;

/// <summary>
///     http://www.tddbuddy.com/katas/Poker%20Hands.pdf
/// </summary>
public partial class App
{
    private void OnStartup(object sender, StartupEventArgs e)
    {
        Player player1 = new("Player1");
        Player player2 = new("Player2");

        player1.GiveCards("10H 10S 9S 9H 10D");
        player2.GiveCards("2D 2S 2C 2H 3S");

        Game game = new();
        game.AddPlayer(player1);
        game.AddPlayer(player2);

        GameViewModel gameViewModel = new(game);

        MainWindow mainWindow = new(gameViewModel);
        mainWindow.Show();
    }
}