using PlayingCards.ViewModels;

namespace PlayingCards.Views;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public MainWindow(GameViewModel gameViewModel)
    {
        InitializeComponent();

        DataContext = gameViewModel;
    }
}