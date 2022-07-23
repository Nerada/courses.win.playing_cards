using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using PlayingCards.Extensions;

namespace PlayingCards.Models;

public class Game
{
    public enum Outcome
    {
        Draw,
        Wins,
        Lost
    }

    private readonly List<Card> _deckOfCards = new();

    private readonly List<Player> _players = new();

    public Game()
    {
        RefreshDeck(_deckOfCards);
    }

    public IReadOnlyList<Player> Players => new ReadOnlyCollection<Player>(_players);

    private void RefreshDeck(List<Card> cards)
    {
        cards.Clear();
        Array availableSuits  = Enum.GetValues(typeof(Card.SuitType));
        Array availableValues = Enum.GetValues(typeof(Card.ValueType));

        foreach (Card.SuitType suit in availableSuits)
        {
            foreach (Card.ValueType value in availableValues) { cards.Add(new Card(value, suit)); }
        }
    }

    public void AddPlayer(Player player)
    {
        player.GiveCards(RandomCards());
        _players.Add(player);
    }

    public void ShuffleCards()
    {
        RefreshDeck(_deckOfCards);

        _players.ForEach(p => p.GiveCards(RandomCards()));
    }

    private List<Card> RandomCards()
    {
        List<Card> newCards = new();

        while (newCards.Count != 5)
        {
            int cardIndex = RandomNumberGenerator.GetInt32(_deckOfCards.Count - 1);

            newCards.Add(_deckOfCards[cardIndex]);
            _deckOfCards.RemoveAt(cardIndex);
        }

        return newCards;
    }

    public List<Player> PlayersWithTheHighestHand()
    {
        List<Player> winningPlayers = new();

        foreach (Player player in _players)
        {
            if (winningPlayers.Count == 0)
            {
                winningPlayers.Add(player);

                continue;
            }

            if (player.Hand.Play(winningPlayers[0].Hand) == Outcome.Draw)
            {
                winningPlayers.Add(player);

                continue;
            }

            if (player.Hand.Play(winningPlayers[0].Hand) == Outcome.Wins)
            {
                winningPlayers.Clear();
                winningPlayers.Add(player);
            }
        }

        return winningPlayers;
    }

    public List<Player> PlayersWithHighestCard()
    {
        List<Player> winningPlayers = new();

        foreach (Player player in _players)
        {
            Card highestPlayerCard = player.Hand.HighestCard;

            if (winningPlayers.Count == 0)
            {
                winningPlayers.Add(player);

                continue;
            }

            if (highestPlayerCard.Play(winningPlayers[0].Hand.HighestCard) == Outcome.Draw)
            {
                winningPlayers.Add(player);

                continue;
            }

            if (highestPlayerCard.Play(winningPlayers[0].Hand.HighestCard) == Outcome.Wins)
            {
                winningPlayers.Clear();
                winningPlayers.Add(player);
            }
        }

        return winningPlayers;
    }
}