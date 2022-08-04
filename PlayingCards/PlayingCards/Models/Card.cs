// -----------------------------------------------
//     Author: Ramon Bollen
//      File: PlayingCards.Card.cs
// Created on: 20220723
// -----------------------------------------------

using PlayingCards.Extensions;

namespace PlayingCards.Models;

public class Card
{
    public enum SuitType
    {
        Spades,
        Clubs,
        Hearts,
        Diamonds
    }

    public enum ValueType
    {
        Ace,
        King,
        Queen,
        Jack,
        Ten,
        Nine,
        Eight,
        Seven,
        Six,
        Five,
        Four,
        Three,
        Two
    }

    public Card(string value, char suit) : this(value.ToValue(), suit.ToSuit())
    {
    }

    public Card(ValueType value, SuitType suit)
    {
        Value = value;
        Suit  = suit;
    }

    public SuitType Suit { get; }

    public ValueType Value { get; }
}