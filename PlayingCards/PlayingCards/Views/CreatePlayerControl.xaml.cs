using System.Windows;

namespace PlayingCards.Views;

/// <summary>
///     Interaction logic for CreatePlayerControl.xaml
/// </summary>
public partial class CreatePlayerControl
{
    public CreatePlayerControl()
    {
        InitializeComponent();
    }

    private void OnAddPlayer(object sender, RoutedEventArgs e)
    {
        PlayerName.Text = string.Empty;
    }
}