// -----------------------------------------------
//     Author: Ramon Bollen
//      File: PlayingCards.Extensions.cs
// Created on: 20220722
// -----------------------------------------------

using System.Runtime.CompilerServices;
using PlayingCards.Models;

namespace PlayingCards;

public static class Extensions
{
    public static Suit ToSuit(this char suitChar) =>
        suitChar switch
        {
            'C' => Suit.Clubs,
            'D' => Suit.Diamonds,
            'H' => Suit.Hearts,
            'S' => Suit.Spades,
            _   => throw new SwitchExpressionException()
        };

    public static Game.Outcome CompareTo(this Card thisCard, Card otherCard) =>
        thisCard.Weight == otherCard.Weight ? Game.Outcome.Draw :
        thisCard.Weight > otherCard.Weight  ? Game.Outcome.Wins : Game.Outcome.Lost;
}