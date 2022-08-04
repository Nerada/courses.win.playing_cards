using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Input;
using PlayingCards.Models;
using Prism.Commands;

namespace PlayingCards.ViewModels;

[ExcludeFromCodeCoverage]
public class GameViewModel : ViewModelBase
{
    private readonly Game _game;

    private string _error = string.Empty;

    public GameViewModel(Game game)
    {
        _game            = game;
        ShuffleCommand   = new DelegateCommand(Shuffle);
        AddPlayerCommand = new DelegateCommand<string>(AddPlayer);
    }

    public ICommand ShuffleCommand { get; }

    public ICommand AddPlayerCommand { get; }

    public IReadOnlyList<Card> GameCards => _game.GameCards;

    public IReadOnlyList<PlayerViewModel> Players => _game.Players.Select(p => new PlayerViewModel(p, DeletePlayer)).ToList();

    public string Winners
    {
        get
        {
            IReadOnlyList<(Player player, Outcome result)> gameResult = _game.Result();

            switch (gameResult.Count)
            {
                case 0:
                    return "No winner yet.";
                case 1:
                    return $"{gameResult[0].player.PlayerName} has won the game!";
                default:
                    return gameResult.Any(r => r.result.HighestHand == Outcome.Result.Wins)
                        ? $"{gameResult.Single(p => p.result.HighestHand == Outcome.Result.Wins).player.PlayerName} has won the game with the highest hand."
                        : $"The game ended in a draw between: {string.Join(" & ", gameResult.Select(w => w.player.PlayerName).ToList())}.";
            }
        }
    }

    public string Error
    {
        get => _error;
        private set => Set(ref _error, value);
    }

    public void ClearError() => Error = string.Empty;

    private void AddPlayer(string playerName)
    {
        if (string.IsNullOrWhiteSpace(playerName)) return;

        // Hard limit for now (it takes up max space)
        if (_game.Players.Count == 9)
        {
            Error = "Maximum players reached.";
            return;
        }

        if (_game.Players.Any(p => p.PlayerName == playerName))
        {
            Error = "Player already exists.";
            return;
        }

        _game.AddPlayer(new Player(playerName));

        AllPropertiesChanged();
    }

    private void DeletePlayer(Player player)
    {
        _game.RemovePlayer(player);
        AllPropertiesChanged();
    }

    private void Shuffle()
    {
        _game.ShuffleCards();
        AllPropertiesChanged();
    }

    private void AllPropertiesChanged()
    {
        RaisePropertyChanged(nameof(Players));
        RaisePropertyChanged(nameof(GameCards));
        RaisePropertyChanged(nameof(Winners));
    }
}