using System.Collections.Generic;
using PlayingCards.Models;

namespace PlayingCards.ViewModels;

public class GameViewModel
{
    private readonly Game _game;

    public GameViewModel(Game game)
    {
        _game = game;
    }

    public IReadOnlyList<Player> Players => _game.Players;
}