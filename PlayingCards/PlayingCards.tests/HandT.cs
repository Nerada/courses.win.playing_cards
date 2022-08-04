// -----------------------------------------------
//     Author: Ramon Bollen
//      File: PlayingCards.tests.HandT.cs
// Created on: 20220723
// -----------------------------------------------

using PlayingCards.Models;

namespace PlayingCards.tests;

public class HandT
{
    [Theory]
    [InlineData("QH KH 10H JH AH",   Hand.HandName.RoyalFlush)]
    [InlineData("6S 8S 4S 7S 5S",    Hand.HandName.StraightFlush)]
    [InlineData("5C 5S 5H 5D 10S",   Hand.HandName.FourOfAKind)]
    [InlineData("10H 10S 9S 9H 10D", Hand.HandName.FullHouse)]
    [InlineData("4H 6H 10H JH QH",   Hand.HandName.Flush)]
    [InlineData("9C 6H 7S 5D 8D",    Hand.HandName.Straight)]
    [InlineData("3C 5H 9D 5D 5S",    Hand.HandName.ThreeOfAKind)]
    [InlineData("AH AD 8C 5C 5D",    Hand.HandName.TwoPair)]
    [InlineData("3C 5H 9D 5D 2S",    Hand.HandName.Pair)]
    public void CheckHands(string cards, Hand.HandName expectedHand)
    {
        Player testPlayer = new(string.Empty);
        testPlayer.GiveCards(cards);
        testPlayer.Hand.Name.Should().Be(expectedHand);
    }
}