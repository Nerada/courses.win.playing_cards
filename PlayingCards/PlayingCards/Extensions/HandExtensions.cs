using System;
using System.Collections.Generic;
using System.Linq;
using PlayingCards.Models;

namespace PlayingCards.Extensions;

public static class HandExtensions
{
    public static bool IsRoyalFlush(this IReadOnlyList<Card> cards)
    {
        if (!cards.IsFlush()) return false;

        if (cards.All(c => c.Value != Card.ValueType.Ace)) return false;
        if (cards.All(c => c.Value != Card.ValueType.King)) return false;
        if (cards.All(c => c.Value != Card.ValueType.Queen)) return false;
        if (cards.All(c => c.Value != Card.ValueType.Jack)) return false;
        if (cards.All(c => c.Value != Card.ValueType.Ten)) return false;

        return true;
    }

    public static bool IsStraightFlush(this IReadOnlyList<Card> cards) => IsFlush(cards) && IsStraight(cards);

    public static bool IsFourOfAKind(this IReadOnlyList<Card> cards) => cards.GroupBy(c => c.Value).Any(g => g.Count() == 4);

    public static bool IsStraight(this IReadOnlyList<Card> cards)
    {
        int highestCardWeight = cards.Max(c => Hand.CardWeight[c.Value]);

        for (int i = 0; i < 5; i++)
        {
            if (cards.All(c => Hand.CardWeight[c.Value] != highestCardWeight - i)) return false;
        }

        return true;
    }

    public static bool IsFullHouse(this IReadOnlyList<Card> cards) => IsThreeOfAKind(cards) && IsPair(cards);

    public static bool IsFlush(this IReadOnlyList<Card> cards) => cards.Count >= 3 && cards.All(c => c.Suit == cards[0].Suit);

    public static bool IsThreeOfAKind(this IReadOnlyList<Card> cards) => cards.GroupBy(c => c.Value).Any(g => g.Count() == 3);

    public static bool IsTwoPairs(this IReadOnlyList<Card> cards) => cards.GroupBy(c => c.Value).Count(g => g.Count() == 2) == 2;

    public static bool IsPair(this IReadOnlyList<Card> cards) => cards.GroupBy(c => c.Value).Count(g => g.Count() == 2) == 1;

    public static IReadOnlyList<Card> GetRoyalFlushCards(this IReadOnlyList<Card> cards) => cards.IsRoyalFlush()
                                                                                                ? new List<Card>
                                                                                                {
                                                                                                    cards.First(c => c.Value == Card.ValueType.Ace),
                                                                                                    cards.First(c => c.Value == Card.ValueType.King),
                                                                                                    cards.First(c => c.Value == Card.ValueType.Queen),
                                                                                                    cards.First(c => c.Value == Card.ValueType.Jack),
                                                                                                    cards.First(c => c.Value == Card.ValueType.Ten)
                                                                                                }
                                                                                                : throw new InvalidOperationException();

    public static IReadOnlyList<Card> GetStraightFlushCards(this IReadOnlyList<Card> cards) =>
        cards.IsStraightFlush() ? cards.GetFlushCards().GetStraightCards() : throw new InvalidOperationException();

    public static IReadOnlyList<Card> GetFourOfAKindCards(this IReadOnlyList<Card> cards) =>
        cards.IsFourOfAKind() ? cards.GroupBy(c => c.Value).Where(g => g.Count() == 4).SelectMany(g => g).ToList() : throw new InvalidOperationException();

    public static IReadOnlyList<Card> GetStraightCards(this IReadOnlyList<Card> cards)
    {
        if (!cards.IsStraight()) throw new InvalidOperationException();

        int highestCardWeight = cards.Max(c => Hand.CardWeight[c.Value]);

        List<Card> straightCards = new();

        for (int i = 0; i < 5; i++) { straightCards.Add(cards.First(c => Hand.CardWeight[c.Value] == highestCardWeight - i)); }

        return straightCards;
    }

    public static IReadOnlyList<Card> GetFullHouseCards(this IReadOnlyList<Card> cards) =>
        cards.IsFullHouse() ? cards.GetThreeOfAKindCards().Concat(cards.GetPairCards()).ToList() : throw new InvalidOperationException();

    public static IReadOnlyList<Card> GetFlushCards(this IReadOnlyList<Card> cards) => cards.IsFlush() ? cards.Where(c => c.Suit == cards[0].Suit).ToList() : throw new InvalidOperationException();

    public static IReadOnlyList<Card> GetThreeOfAKindCards(this IReadOnlyList<Card> cards) =>
        cards.IsThreeOfAKind() ? cards.GroupBy(c => c.Value).Where(g => g.Count() == 3).SelectMany(g => g).ToList() : throw new InvalidOperationException();

    public static IReadOnlyList<Card> GetTwoPairsCards(this IReadOnlyList<Card> cards) =>
        cards.IsTwoPairs() ? cards.GroupBy(c => c.Value).Where(g => g.Count() == 2).SelectMany(g => g).ToList() : throw new InvalidCastException();

    public static IReadOnlyList<Card> GetPairCards(this IReadOnlyList<Card> cards) =>
        cards.IsPair() ? cards.GroupBy(c => c.Value).Where(g => g.Count() == 2).SelectMany(g => g).ToList() : throw new InvalidOperationException();
}