using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PlayingCards.Extensions;

namespace PlayingCards.Models;

public class Hand
{
    public enum HandName
    {
        RoyalFlush,
        StraightFlush,
        FourOfAKind,
        FullHouse,
        Flush,
        Straight,
        ThreeOfAKind,
        TwoPair,
        Pair,
        HighCard
    }

    public static readonly ReadOnlyDictionary<Card.ValueType, int> CardWeight = new(new Dictionary<Card.ValueType, int>
    {
        {Card.ValueType.Ace, 13},
        {Card.ValueType.King, 12},
        {Card.ValueType.Queen, 11},
        {Card.ValueType.Jack, 10},
        {Card.ValueType.Ten, 9},
        {Card.ValueType.Nine, 8},
        {Card.ValueType.Eight, 7},
        {Card.ValueType.Seven, 6},
        {Card.ValueType.Six, 5},
        {Card.ValueType.Five, 4},
        {Card.ValueType.Four, 3},
        {Card.ValueType.Three, 2},
        {Card.ValueType.Two, 1}
    });

    private readonly ReadOnlyDictionary<HandName, int> _handWeight = new(new Dictionary<HandName, int>
    {
        {HandName.RoyalFlush, 28},
        {HandName.StraightFlush, 27},
        {HandName.FourOfAKind, 26},
        {HandName.FullHouse, 25},
        {HandName.Flush, 24},
        {HandName.Straight, 23},
        {HandName.ThreeOfAKind, 22},
        {HandName.TwoPair, 21},
        {HandName.Pair, 20}
    });

    private List<Card> _cards = new();

    public IReadOnlyList<Card> Cards => new ReadOnlyCollection<Card>(_cards);

    public HandName Name => GetHandName(_cards);

    public Card HighestCard => GetHighestCard(_cards);

    public int Weight => Name == HandName.HighCard ? CardWeight[HighestCard.Value] : _handWeight[Name];

    public void GiveCards(List<Card> cards) => _cards = cards;

    private Card GetHighestCard(List<Card> cards) => cards.MaxBy(c => CardWeight[c.Value]) ?? throw new InvalidOperationException();

    private HandName GetHandName(List<Card> cards)
    {
        if (cards.IsRoyalFlush()) return HandName.RoyalFlush;
        if (cards.IsStraightFlush()) return HandName.StraightFlush;
        if (cards.IsFourOfAKind()) return HandName.FourOfAKind;
        if (cards.IsFullHouse()) return HandName.FullHouse;
        if (cards.IsFlush()) return HandName.Flush;
        if (cards.IsStraight()) return HandName.Straight;
        if (cards.IsThreeOfAKind()) return HandName.ThreeOfAKind;
        if (cards.IsTwoPairs()) return HandName.TwoPair;
        if (cards.IsPair()) return HandName.Pair;

        return HandName.HighCard;
    }
}