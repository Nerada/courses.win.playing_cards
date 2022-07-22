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

    public Card(string value, char suit)
    {
        Value = value.ToValue();
        Suit  = suit.ToSuit();
    }

    public ValueType Value { get; }
    public SuitType  Suit  { get; }
}