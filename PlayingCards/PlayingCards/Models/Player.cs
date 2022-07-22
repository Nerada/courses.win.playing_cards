using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayingCards.Models;

public class Player
{
    private readonly List<Card> _cards = new();

    public Player(string playerName)
    {
        PlayerName = playerName;
    }

    public string PlayerName { get; }

    public void AddCards(string cardsString)
    {
        List<string> cardStrings = cardsString.Split().ToList();

        cardStrings.ForEach(s =>
        {
            char   suit  = s[^1];
            string value = s.Remove(s.Length - 1);

            _cards.Add(new Card(value, suit));
        });
    }

    public Card HighestCard() => _cards.MaxBy(c => c.Weight) ?? throw new InvalidOperationException();
}