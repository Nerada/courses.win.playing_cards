using System;
using System.Collections.Generic;
using System.Linq;
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

    public string Winners
    {
        get
        {
            List<Player> winningPlayers = _game.PlayersWithTheHighestHand();

            return winningPlayers.Count == 0 ? "No winner yet." :
                winningPlayers.Count    == 1 ? $"{winningPlayers[0].PlayerName} has won the game!" :
                                               $"The game ended in a draw between:{Environment.NewLine}{string.Join(" & ", winningPlayers.Select(w => w.PlayerName).ToList())}";
        }
    }

    private void Refresh()
    {
        _game.ShuffleCards();
        RaisePropertyChanged(nameof(Players));
        RaisePropertyChanged(nameof(Winners));
    }
}