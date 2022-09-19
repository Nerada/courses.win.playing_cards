// -----------------------------------------------
//     Author: Ramon Bollen
//      File: PlayingCards.PlayerViewModel.cs
// Created on: 20220804
// -----------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using Humanizer;
using PlayingCards.Models;
using Prism.Commands;

namespace PlayingCards.ViewModels;

[ExcludeFromCodeCoverage]
public class PlayerViewModel
{
    private readonly Player _player;

    public PlayerViewModel(Player player, Action<Player> deletePlayer)
    {
        _player             = player;
        DeletePlayerCommand = new DelegateCommand(() => deletePlayer(_player));
    }

    public ICommand DeletePlayerCommand { get; }

    public string PlayerName => _player.PlayerName;

    public string HandName => _player.Hand.Name.Humanize();

    public IReadOnlyList<Card> PlayerCards => _player.Hand.PlayerCards;
}