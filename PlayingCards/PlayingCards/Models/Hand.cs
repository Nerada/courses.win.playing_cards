// -----------------------------------------------
//     Author: Ramon Bollen
//      File: PlayingCards.Hand.cs
// Created on: 20220722
// -----------------------------------------------

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

    private List<Card> _gameCards   = new();
    private List<Card> _playerCards = new();

    public Card HighestCard => _playerCards.MaxBy(c => c.Weight()) ?? throw new InvalidOperationException();

    public int Weight => Name == HandName.HighCard ? HighestCard.Weight() : Name.Weight();

    public IReadOnlyList<Card> PlayerCards => new ReadOnlyCollection<Card>(_playerCards);

    public IReadOnlyList<Card> Cards => new ReadOnlyCollection<Card>(_gameCards.Concat(_playerCards).ToList());

    public ProcessedCards HandSpecificCards { get; } = new();

    public HandName Name { get; private set; }

    public void GiveCards(List<Card> cards)
    {
        _playerCards = cards;
        UpdateHand();
    }

    public void SetGameCards(List<Card> cards)
    {
        _gameCards = cards;
        UpdateHand();
    }

    private void UpdateHand()
    {
        List<Card> cards = _gameCards.Concat(_playerCards).ToList();

        HandName GetHand()
        {
            if (cards.IsRoyalFlush(HandSpecificCards.Clear())) return HandName.RoyalFlush;
            if (cards.IsStraightFlush(HandSpecificCards.Clear())) return HandName.StraightFlush;
            if (cards.IsFourOfAKind(HandSpecificCards.Clear())) return HandName.FourOfAKind;
            if (cards.IsFullHouse(HandSpecificCards.Clear())) return HandName.FullHouse;
            if (cards.IsFlush(HandSpecificCards.Clear())) return HandName.Flush;
            if (cards.IsStraight(HandSpecificCards.Clear())) return HandName.Straight;
            if (cards.IsThreeOfAKind(HandSpecificCards.Clear())) return HandName.ThreeOfAKind;
            if (cards.IsTwoPairs(HandSpecificCards.Clear())) return HandName.TwoPair;
            if (cards.IsPair(HandSpecificCards.Clear())) return HandName.Pair;

            HandSpecificCards.Clear().AddHand(HandName.HighCard, cards);
            return HandName.HighCard;
        }

        Name = GetHand();
    }
}