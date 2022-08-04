// -----------------------------------------------
//     Author: Ramon Bollen
//      File: PlayingCards.Player.cs
// Created on: 20220722
// -----------------------------------------------

using System.Collections.Generic;
using PlayingCards.Extensions;

namespace PlayingCards.Models;

public class Player
{
    public Player(string playerName)
    {
        PlayerName = playerName;
    }

    public Hand Hand { get; } = new();

    public string PlayerName { get; }

    public void GiveCards(string cardsString) => GiveCards(cardsString.ToCards());

    public void SetGameCards(List<Card> gameCards) => Hand.SetGameCards(gameCards);

    public void GiveCards(List<Card> newCards) => Hand.GiveCards(newCards);
}