using System.Collections.Generic;
using System.Linq;

namespace PlayingCards.Models;

public class Player
{
    public Player(string playerName)
    {
        PlayerName = playerName;
    }

    public string PlayerName { get; }

    public Hand Hand { get; } = new();

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

    public void SetGameCards(List<Card> gameCards) => Hand.SetGameCards(gameCards);

    public void GiveCards(List<Card> newCards) => Hand.GiveCards(newCards);
}