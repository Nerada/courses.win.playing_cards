// -----------------------------------------------
//     Author: Ramon Bollen
//      File: PlayingCards.HandExtensions.cs
// Created on: 20220801
// -----------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PlayingCards.Models;

namespace PlayingCards.Extensions;

public static class HandExtensions
{
    private static readonly ReadOnlyDictionary<Hand.HandName, int> HandWeight = new(new Dictionary<Hand.HandName, int>
    {
        {Hand.HandName.RoyalFlush, 28},
        {Hand.HandName.StraightFlush, 27},
        {Hand.HandName.FourOfAKind, 26},
        {Hand.HandName.FullHouse, 25},
        {Hand.HandName.Flush, 24},
        {Hand.HandName.Straight, 23},
        {Hand.HandName.ThreeOfAKind, 22},
        {Hand.HandName.TwoPair, 21},
        {Hand.HandName.Pair, 20}
    });

    public static bool IsRoyalFlush(this IReadOnlyList<Card> cards, ProcessedCards royalFlushCards)
    {
        if (!cards.IsStraightFlush(royalFlushCards)) return false;

        List<Card> straightFlushCards = royalFlushCards.AllCards().OrderBy(c => c.Value).ToList().Take(5).ToList();

        return straightFlushCards.Count == 5 && straightFlushCards.Count(c => c.Value == Card.ValueType.Ace) == 1;
    }

    public static bool IsStraightFlush(this IReadOnlyList<Card> cards, ProcessedCards straightFlushCards)
    {
        if (!straightFlushCards.RemainingCards(cards).IsFlush(straightFlushCards) || !straightFlushCards.GetHand(Hand.HandName.Flush).IsStraight(straightFlushCards)) return false;

        IReadOnlyList<Card> straightFlush = straightFlushCards.GetHand(Hand.HandName.Straight);
        straightFlushCards.Clear().AddHand(Hand.HandName.StraightFlush, straightFlush.ToList());
        return true;
    }

    public static bool IsFourOfAKind(this IReadOnlyList<Card> cards, ProcessedCards fourOfAKindCards) => fourOfAKindCards.RemainingCards(cards).IsKind(4, fourOfAKindCards);

    public static bool IsStraight(this IReadOnlyList<Card> cards, ProcessedCards straightCards)
    {
        List<Card> orderedCardList = cards.DistinctBy(d => d.Value).OrderBy(c => c.Value).ToList();

        if (orderedCardList.Count < 5) return false;

        List<Card> consecutiveCards = new();

        for (int cardIndex = 0; cardIndex < orderedCardList.Count - 4; cardIndex++)
        {
            consecutiveCards.Add(orderedCardList[cardIndex]);
            for (int currentIndex = cardIndex; currentIndex < cardIndex + 5; currentIndex++)
            {
                if (orderedCardList[currentIndex].Weight() - 1 == orderedCardList[currentIndex + 1].Weight())
                {
                    consecutiveCards.Add(orderedCardList[currentIndex + 1]);

                    if (consecutiveCards.Count == 5)
                    {
                        straightCards.AddHand(Hand.HandName.Straight, consecutiveCards);
                        return true;
                    }
                }
                else
                {
                    consecutiveCards.Clear();
                    break;
                }
            }
        }

        return false;
    }

    public static bool IsFullHouse(this IReadOnlyList<Card> cards, ProcessedCards fullHouseCards) =>
        fullHouseCards.RemainingCards(cards).IsThreeOfAKind(fullHouseCards) && fullHouseCards.RemainingCards(cards).IsPair(fullHouseCards);

    public static bool IsFlush(this IReadOnlyList<Card> cards, ProcessedCards flushCards)
    {
        List<Card> flush = flushCards.RemainingCards(cards).GroupBy(c => c.Suit).OrderByDescending(g => g.Count()).First().ToList();

        if (flush.Count < 5) return false;

        flushCards.AddHand(Hand.HandName.Flush, flush);
        return true;
    }

    public static bool IsThreeOfAKind(this IReadOnlyList<Card> cards, ProcessedCards threeOfAKindCards) => threeOfAKindCards.RemainingCards(cards).IsKind(3, threeOfAKindCards);

    public static bool IsTwoPairs(this IReadOnlyList<Card> cards, ProcessedCards twoPairCards) =>
        twoPairCards.RemainingCards(cards).IsPair(twoPairCards) && twoPairCards.RemainingCards(cards).IsPair(twoPairCards);

    public static bool IsPair(this IReadOnlyList<Card> cards, ProcessedCards pairCards) => pairCards.RemainingCards(cards).IsKind(2, pairCards);

    public static int Weight(this Hand.HandName handName) => HandWeight[handName];

    private static bool IsKind(this IReadOnlyList<Card> cards, int amount, ProcessedCards kindCards)
    {
        Hand.HandName GetHand() => amount switch
        {
            2 => Hand.HandName.Pair,
            3 => Hand.HandName.ThreeOfAKind,
            4 => Hand.HandName.FourOfAKind,
            _ => throw new InvalidOperationException()
        };

        List<Card>? kindList = cards.OrderBy(c => c.Value).GroupBy(c => c.Value).FirstOrDefault(g => g.Count() == amount)?.Select(g => g).ToList();

        if (kindList == null || kindList.Count != amount) return false;

        kindCards.AddHand(GetHand(), kindList);

        return true;
    }
}