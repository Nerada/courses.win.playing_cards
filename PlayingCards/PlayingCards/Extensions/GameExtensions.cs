// -----------------------------------------------
//     Author: Ramon Bollen
//      File: PlayingCards.Extensions.cs
// Created on: 20220722
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
                return HighestCards(new List<Cards> {new(thisHand.Cards.Except(thisHand.Cards.GetRoyalFlushCards()).ToList(), otherHand.Cards.Except(otherHand.Cards.GetRoyalFlushCards()).ToList())});
            case Hand.HandName.StraightFlush:
                return HighestCards(new List<Cards>
                {
                    new(thisHand.Cards.GetStraightFlushCards(), otherHand.Cards.GetStraightFlushCards()),
                    new(thisHand.Cards.Except(thisHand.Cards.GetStraightFlushCards()).ToList(), otherHand.Cards.Except(otherHand.Cards.GetStraightFlushCards()).ToList())
                });
            case Hand.HandName.FourOfAKind:
                return HighestCards(new List<Cards>
                {
                    new(thisHand.Cards.GetFourOfAKindCards(), otherHand.Cards.GetFourOfAKindCards()),
                    new(thisHand.Cards.Except(thisHand.Cards.GetFourOfAKindCards()).ToList(), otherHand.Cards.Except(otherHand.Cards.GetFourOfAKindCards()).ToList())
                });
            case Hand.HandName.Straight:
                return HighestCards(new List<Cards>
                {
                    new(thisHand.Cards.GetStraightCards(), otherHand.Cards.GetStraightCards()),
                    new(thisHand.Cards.Except(thisHand.Cards.GetStraightCards()).ToList(), otherHand.Cards.Except(otherHand.Cards.GetStraightCards()).ToList())
                });
            case Hand.HandName.FullHouse:
                return HighestCards(new List<Cards>
                {
                    new(thisHand.Cards.GetThreeOfAKindCards(), otherHand.Cards.GetThreeOfAKindCards()),
                    new(thisHand.Cards.GetPairCards(), otherHand.Cards.GetPairCards()),
                    new(thisHand.Cards.Except(thisHand.Cards.GetFullHouseCards()).ToList(), otherHand.Cards.Except(otherHand.Cards.GetFullHouseCards()).ToList())
                });
            case Hand.HandName.Flush:
                return HighestCards(new List<Cards>
                {
                    new(thisHand.Cards.GetFlushCards(), otherHand.Cards.GetFlushCards()),
                    new(thisHand.Cards.Except(thisHand.Cards.GetFlushCards()).ToList(), otherHand.Cards.Except(otherHand.Cards.GetFlushCards()).ToList())
                });
            case Hand.HandName.ThreeOfAKind:
                return HighestCards(new List<Cards>
                {
                    new(thisHand.Cards.GetThreeOfAKindCards(), otherHand.Cards.GetThreeOfAKindCards()),
                    new(thisHand.Cards.Except(thisHand.Cards.GetThreeOfAKindCards()).ToList(), otherHand.Cards.Except(otherHand.Cards.GetThreeOfAKindCards()).ToList())
                });
            case Hand.HandName.TwoPair:
                return HighestCards(new List<Cards>
                {
                    new(thisHand.Cards.GetTwoPairsCards(), otherHand.Cards.GetTwoPairsCards()),
                    new(thisHand.Cards.Except(thisHand.Cards.GetTwoPairsCards()).ToList(), otherHand.Cards.Except(otherHand.Cards.GetTwoPairsCards()).ToList())
                });
            case Hand.HandName.Pair:
                return HighestCards(new List<Cards>
                {
                    new(thisHand.Cards.GetPairCards(), otherHand.Cards.GetPairCards()),
                    new(thisHand.Cards.Except(thisHand.Cards.GetPairCards()).ToList(), otherHand.Cards.Except(otherHand.Cards.GetPairCards()).ToList())
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
        List<Card> theseSorted = cards.TheseCards.OrderByDescending(c => Hand.CardWeight[c.Value]).ToList();
        List<Card> otherSorted = cards.OtherCards.OrderByDescending(c => Hand.CardWeight[c.Value]).ToList();

        for (int i = 0; i < Math.Min(cards.TheseCards.Count, cards.OtherCards.Count); i++)
        {
            int thisHandCardWeight  = Hand.CardWeight[theseSorted[i].Value];
            int otherHandCardWeight = Hand.CardWeight[otherSorted[i].Value];

            if (thisHandCardWeight == otherHandCardWeight) continue;

            return thisHandCardWeight > otherHandCardWeight ? Outcome.Result.Wins : Outcome.Result.Lost;
        }

        return Outcome.Result.Draw;
    }

    private record Cards(IReadOnlyList<Card> TheseCards, IReadOnlyList<Card> OtherCards);
}