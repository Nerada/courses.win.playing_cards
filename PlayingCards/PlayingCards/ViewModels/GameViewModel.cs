using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using PlayingCards.Models;
using Prism.Commands;

namespace PlayingCards.ViewModels;

public class GameViewModel : ViewModelBase
{
    private readonly Game _game;

    public GameViewModel(Game game)
    {
        _game            = game;
        ShuffleCommand   = new DelegateCommand(Shuffle);
        AddPlayerCommand = new DelegateCommand<string>(AddPlayer);
    }

    public ICommand ShuffleCommand { get; }

    public ICommand AddPlayerCommand { get; }

    public IReadOnlyList<Player> Players => _game.Players;

    public IReadOnlyList<Card> GameCards => _game.GameCards;

    public string Winners
    {
        get
        {
            (Game.Outcome outcome, List<Player> players) result = _game.Result();

            switch (result.outcome)
            {
                case Game.Outcome.None:
                    return "No winner yet.";
                case Game.Outcome.Wins:
                    return $"{result.players[0].PlayerName} has won the game!";
                case Game.Outcome.Draw:
                    return $"The game ended in a draw between:{Environment.NewLine}{string.Join(" & ", result.players.Select(w => w.PlayerName).ToList())}";
            }

            return string.Empty;
        }
    }

    private void AddPlayer(string playerName)
    {
        // Hard limit for now (it takes up max space)
        if (_game.Players.Count == 9) return;
        if (string.IsNullOrWhiteSpace(playerName)) return;
        if (_game.Players.Any(p => p.PlayerName == playerName)) return;

        _game.AddPlayer(new Player(playerName));

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