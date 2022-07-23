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
        Player player1 = new("Nemesis");
        Player player2 = new("Kronos");
        Player player3 = new("Skadi");

        Game game = new();
        game.AddPlayer(player1);
        game.AddPlayer(player2);
        game.AddPlayer(player3);

        GameDeckViewModel gameDeckViewModel = new(game);

        MainWindow mainWindow = new(gameDeckViewModel);
        mainWindow.Show();
    }
}