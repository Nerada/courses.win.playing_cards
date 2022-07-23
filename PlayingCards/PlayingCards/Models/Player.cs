using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayingCards.Models;

public class Player
{
    private Hand? _hand;

    public Player(string playerName)
    {
        PlayerName = playerName;
    }

    public string PlayerName { get; }

    public Hand Hand => _hand ?? throw new InvalidOperationException("No cards dealt yet");

    public void GiveCards(string cardsString)
    {
        List<string> cardStrings = cardsString.Split().ToList();
        List<Card>   cards       = new();

        cardStrings.ForEach(s =>
        {
            char   suit  = s[^1];
            string value = s.Remove(s.Length - 1);

            cards.Add(new Card(value, suit));
        });

        GiveCards(cards);
    }

    public void GiveCards(List<Card> newCards) => _hand = new Hand(newCards);
}