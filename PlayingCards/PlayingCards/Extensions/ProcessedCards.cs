// -----------------------------------------------
//     Author: Ramon Bollen
//      File: PlayingCards.ProcessedCards.cs
// Created on: 20220804
// -----------------------------------------------

using System.Collections.Generic;
using System.Linq;
using PlayingCards.Models;

namespace PlayingCards.Extensions;

public class ProcessedCards
{
    private readonly List<(Hand.HandName hand, List<Card> cards)> _hands = new();

    public IReadOnlyList<Card> RemainingCards(IReadOnlyList<Card> cards) => cards.Except(_hands.SelectMany(h => h.cards).ToList()).ToList();

    public IReadOnlyList<Card> GetHand(Hand.HandName name) => _hands.First(h => h.hand == name).cards;

    public IReadOnlyList<Card> AllCards() => _hands.SelectMany(h => h.cards).Distinct().ToList();

    public ProcessedCards Clear()
    {
        _hands.Clear();
        return this;
    }

    public void AddHand(Hand.HandName name, List<Card> cards) => _hands.Add((name, cards));
}