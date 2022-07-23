// -----------------------------------------------
//     Author: Ramon Bollen
//      File: PlayingCards.Extensions.cs
// Created on: 20220722
// -----------------------------------------------

using System.Runtime.CompilerServices;
using System.Windows.Media;
using PlayingCards.Models;

namespace PlayingCards.Extensions;

public static class Extensions
{
    public static Card.SuitType ToSuit(this char suitChar) =>
        suitChar switch
        {
            'C' => Card.SuitType.Clubs,
            'D' => Card.SuitType.Diamonds,
            'H' => Card.SuitType.Hearts,
            'S' => Card.SuitType.Spades,
            _   => throw new SwitchExpressionException()
        };

    public static char ToSuitChar(this Card.SuitType suitType) =>
        suitType switch
        {
            Card.SuitType.Clubs    => '♣',
            Card.SuitType.Diamonds => '♦',
            Card.SuitType.Hearts   => '♥',
            Card.SuitType.Spades   => '♠',
            _                      => throw new SwitchExpressionException()
        };

    public static Card.ValueType ToValue(this string valueString) =>
        valueString switch
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

    public static string ToValueString(this Card.ValueType value) =>
        value switch
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

    public static Brush ToSuitColor(this Card.SuitType suit) =>
        suit is Card.SuitType.Hearts or Card.SuitType.Diamonds ? new SolidColorBrush(Colors.OrangeRed) : new SolidColorBrush(Colors.DimGray);

    public static Game.Outcome Play(this Card thisCard, Card otherCard) =>
        Hand.CardWeight[thisCard.Value] == Hand.CardWeight[otherCard.Value] ? Game.Outcome.Draw :
        Hand.CardWeight[thisCard.Value] > Hand.CardWeight[otherCard.Value]  ? Game.Outcome.Wins : Game.Outcome.Lost;

    public static Game.Outcome Play(this Hand thisHand, Hand otherHand) =>
        thisHand.Weight == otherHand.Weight ? Game.Outcome.Draw :
        thisHand.Weight > otherHand.Weight  ? Game.Outcome.Wins : Game.Outcome.Lost;
}