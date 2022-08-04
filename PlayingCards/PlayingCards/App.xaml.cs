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
    /// <summary>
    ///     Composition
    /// </summary>
    private void OnStartup(object sender, StartupEventArgs e)
    {
        Game game = new();

        GameViewModel gameViewModel = new(game);

        MainWindow mainWindow = new(gameViewModel);
        mainWindow.Show();
    }
}