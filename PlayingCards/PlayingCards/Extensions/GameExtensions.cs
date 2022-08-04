// -----------------------------------------------
//     Author: Ramon Bollen
//      File: PlayingCards.GameExtensions.cs
// Created on: 20220801
// -----------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using PlayingCards.Models;

namespace PlayingCards.Extensions;

public static class GameExtensions
{
    public static Outcome Play(this Hand thisHand, Hand otherHand) => thisHand.Weight == otherHand.Weight ? new Outcome(Outcome.Result.Draw, thisHand.HasHighestHandWhenSame(otherHand)) :
        thisHand.Weight > otherHand.Weight ? new Outcome(Outcome.Result.Wins) : new Outcome(Outcome.Result.Lost);

    /// <summary>
    ///     Get cards relevant to hand and check which one is highest
    /// </summary>
    private static Outcome.Result HasHighestHandWhenSame(this Hand thisHand, Hand otherHand)
    {
        if (thisHand.Name != otherHand.Name) throw new InvalidOperationException("Highest hand is only important if hands are equal");

        switch (thisHand.Name)
        {
            case Hand.HandName.RoyalFlush:
                return HighestCards(new List<Cards>
                {
                    new(thisHand.HandSpecificCards.RemainingCards(thisHand.Cards), otherHand.HandSpecificCards.RemainingCards(otherHand.Cards))
                });
            case Hand.HandName.FullHouse:
                return HighestCards(new List<Cards>
                {
                    new(thisHand.HandSpecificCards.GetHand(Hand.HandName.ThreeOfAKind), otherHand.HandSpecificCards.GetHand(Hand.HandName.ThreeOfAKind)),
                    new(thisHand.HandSpecificCards.GetHand(Hand.HandName.Pair), otherHand.HandSpecificCards.GetHand(Hand.HandName.Pair)),
                    new(thisHand.HandSpecificCards.RemainingCards(thisHand.Cards), otherHand.HandSpecificCards.RemainingCards(otherHand.Cards))
                });
            case Hand.HandName.StraightFlush:
            case Hand.HandName.FourOfAKind:
            case Hand.HandName.Straight:
            case Hand.HandName.Flush:
            case Hand.HandName.ThreeOfAKind:
            case Hand.HandName.TwoPair:
            case Hand.HandName.Pair:
                return HighestCards(new List<Cards>
                {
                    new(thisHand.HandSpecificCards.AllCards(), otherHand.HandSpecificCards.AllCards()),
                    new(thisHand.HandSpecificCards.RemainingCards(thisHand.Cards), otherHand.HandSpecificCards.RemainingCards(otherHand.Cards))
                });
            case Hand.HandName.HighCard:
                return HighestCards(new List<Cards> {new(thisHand.Cards, otherHand.Cards)});
        }

        return Outcome.Result.Draw;
    }

    private static Outcome.Result HighestCards(IReadOnlyList<Cards> lists)
    {
        foreach (Outcome.Result currentOutcome in lists.Select(HighestCards).Where(currentOutcome => currentOutcome != Outcome.Result.Draw)) { return currentOutcome; }

        return Outcome.Result.Draw;
    }

    private static Outcome.Result HighestCards(Cards cards)
    {
        List<Card> theseSorted = cards.TheseCards.OrderByDescending(c => c.Weight()).ToList();
        List<Card> otherSorted = cards.OtherCards.OrderByDescending(c => c.Weight()).ToList();

        for (int i = 0; i < Math.Min(cards.TheseCards.Count, cards.OtherCards.Count); i++)
        {
            int thisHandCardWeight  = theseSorted[i].Weight();
            int otherHandCardWeight = otherSorted[i].Weight();

            if (thisHandCardWeight == otherHandCardWeight) continue;

            return thisHandCardWeight > otherHandCardWeight ? Outcome.Result.Wins : Outcome.Result.Lost;
        }

        return Outcome.Result.Draw;
    }

    private record Cards(IReadOnlyList<Card> TheseCards, IReadOnlyList<Card> OtherCards);
}