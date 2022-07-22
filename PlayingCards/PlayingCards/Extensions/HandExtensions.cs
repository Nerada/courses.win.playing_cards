using System.Collections.Generic;
using System.Linq;
using PlayingCards.Models;

namespace PlayingCards.Extensions;

public static class HandExtensions
{
    public static bool IsRoyalFlush(this List<Card> cards)
    {
        if (!cards.IsFlush()) return false;

        if (cards.All(c => c.Value != Card.ValueType.Ace)) return false;
        if (cards.All(c => c.Value != Card.ValueType.King)) return false;
        if (cards.All(c => c.Value != Card.ValueType.Queen)) return false;
        if (cards.All(c => c.Value != Card.ValueType.Jack)) return false;
        if (cards.All(c => c.Value != Card.ValueType.Ten)) return false;

        return true;
    }

    public static bool IsStraightFlush(this List<Card> cards) => IsFlush(cards) && IsStraight(cards);

    public static bool IsFourOfAKind(this List<Card> cards) => cards.GroupBy(c => c.Value).Any(g => g.Count() == 4);


    public static bool IsStraight(this List<Card> cards)
    {
        int lowestCardWeight = cards.Min(c => Hand.CardWeight[c.Value]);

        for (int i = 0; i < 5; i++)
        {
            if (cards.All(c => Hand.CardWeight[c.Value] != lowestCardWeight + i)) return false;
        }

        return true;
    }

    public static bool IsFullHouse(this List<Card> cards) => IsThreeOfAKind(cards) && IsPair(cards);

    public static bool IsFlush(this List<Card> cards) => cards.All(c => c.Suit == cards[0].Suit);


    public static bool IsThreeOfAKind(this List<Card> cards) => cards.GroupBy(c => c.Value).Any(g => g.Count() == 3);

    public static bool IsTwoPairs(this List<Card> cards) => cards.GroupBy(c => c.Value).Count(g => g.Count() == 2) == 2;

    public static bool IsPair(this List<Card> cards) => cards.GroupBy(c => c.Value).Any(g => g.Count() == 2);
}