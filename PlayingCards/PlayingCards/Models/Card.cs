using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PlayingCards.Models;

public enum Suit
{
    Spades,
    Clubs,
    Hearts,
    Diamonds
}

public class Card
{
    private readonly ReadOnlyDictionary<string, int> _cardWeight = new(new Dictionary<string, int>
    {
        {"A", 13},
        {"K", 12},
        {"Q", 11},
        {"J", 10},
        {"10", 9},
        {"9", 8},
        {"8", 7},
        {"7", 6},
        {"6", 5},
        {"5", 4},
        {"4", 3},
        {"3", 2},
        {"2", 1}
    });

    public Card(string value, char suit)
    {
        Value  = value;
        Suit   = suit.ToSuit();
        Weight = _cardWeight[Value];
    }

    public string Value  { get; }
    public Suit   Suit   { get; }
    public int    Weight { get; }
}