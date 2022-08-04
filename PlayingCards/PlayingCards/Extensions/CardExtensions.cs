// -----------------------------------------------
//     Author: Ramon Bollen
//      File: PlayingCards.CardExtensions.cs
// Created on: 20220803
// -----------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using PlayingCards.Models;

namespace PlayingCards.Extensions;

public static class CardExtensions
{
    private static readonly ReadOnlyDictionary<Card.ValueType, int> CardWeight = new(new Dictionary<Card.ValueType, int>
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

    public static string ToValueString(this Card.ValueType value) => value switch
    {
        Card.ValueType.Ace   => "A",
        Card.ValueType.King  => "K",
        Card.ValueType.Queen => "Q",
        Card.ValueType.Jack  => "J",
        Card.ValueType.Ten   => "10",
        Card.ValueType.Nine  => "9",
        Card.ValueType.Eight => "8",
        Card.ValueType.Seven => "7",
        Card.ValueType.Six   => "6",
        Card.ValueType.Five  => "5",
        Card.ValueType.Four  => "4",
        Card.ValueType.Three => "3",
        Card.ValueType.Two   => "2",
        _                    => throw new SwitchExpressionException()
    };

    public static Card.SuitType ToSuit(this char suitChar) => suitChar switch
    {
        'C' => Card.SuitType.Clubs,
        'D' => Card.SuitType.Diamonds,
        'H' => Card.SuitType.Hearts,
        'S' => Card.SuitType.Spades,
        _   => throw new SwitchExpressionException()
    };

    public static Card.ValueType ToValue(this string valueString) => valueString switch
    {
        "A"  => Card.ValueType.Ace,
        "K"  => Card.ValueType.King,
        "Q"  => Card.ValueType.Queen,
        "J"  => Card.ValueType.Jack,
        "10" => Card.ValueType.Ten,
        "9"  => Card.ValueType.Nine,
        "8"  => Card.ValueType.Eight,
        "7"  => Card.ValueType.Seven,
        "6"  => Card.ValueType.Six,
        "5"  => Card.ValueType.Five,
        "4"  => Card.ValueType.Four,
        "3"  => Card.ValueType.Three,
        "2"  => Card.ValueType.Two,
        _    => throw new SwitchExpressionException()
    };

    public static List<Card> ToCards(this string cardsString)
    {
        List<string> cardStrings = cardsString.Split().ToList();
        List<Card>   cards       = new();

        cardStrings.ForEach(s =>
        {
            char   suit  = s[^1];
            string value = s.Remove(s.Length - 1);

            cards.Add(new Card(value, suit));
        });

        return cards;
    }

    public static Brush ToSuitColor(this Card.SuitType suit) => suit is Card.SuitType.Hearts or Card.SuitType.Diamonds ? new SolidColorBrush(Colors.OrangeRed) : new SolidColorBrush(Colors.DimGray);

    public static char ToSuitChar(this Card.SuitType suitType) => suitType switch
    {
        Card.SuitType.Clubs    => '♣',
        Card.SuitType.Diamonds => '♦',
        Card.SuitType.Hearts   => '♥',
        Card.SuitType.Spades   => '♠',
        _                      => throw new SwitchExpressionException()
    };

    public static int Weight(this Card card) => CardWeight[card.Value];
}