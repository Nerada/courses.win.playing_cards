using System.Collections.Generic;
using System.Windows.Input;
using PlayingCards.Models;
using Prism.Commands;

namespace PlayingCards.ViewModels;

public class GameDeckViewModel : ViewModelBase
{
    private readonly Game _game;

    public GameDeckViewModel(Game game)
    {
        _game          = game;
        RefreshCommand = new DelegateCommand(Refresh);
    }

    public ICommand RefreshCommand { get; }

    public IReadOnlyList<Player> Players => _game.Players;

    private void Refresh()
    {
        _game.ShuffleCards();
        RaisePropertyChanged(nameof(Players));
    }
}