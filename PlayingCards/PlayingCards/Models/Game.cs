using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using PlayingCards.Extensions;

namespace PlayingCards.Models;

public class Game
{
    private const int CardsPerPlayer = 2;
    private const int CardsPerGame = 5;
    private const int ReservedCardsForGame = 10;

    public enum Outcome
    {
        None,
        Draw,
        Wins,
        Lost
    }

    private readonly List<Card> _deckOfCards = new();

    private readonly List<Player> _players = new();
    private readonly List<Card> _gameCards = new();

    public Game() => RefreshDeck();

    public IReadOnlyList<Player> Players => new ReadOnlyCollection<Player>(_players);

    public IReadOnlyList<Card> GameCards => new ReadOnlyCollection<Card>(_gameCards);

    private void RefreshDeck()
    {
        _deckOfCards.Clear();
        _gameCards.Clear();
        Array availableSuits  = Enum.GetValues(typeof(Card.SuitType));
        Array availableValues = Enum.GetValues(typeof(Card.ValueType));

        foreach (Card.SuitType suit in availableSuits)
        {
            foreach (Card.ValueType value in availableValues) { _deckOfCards.Add(new Card(value, suit)); }
        }
    }

    public void AddPlayer(Player player)
    {
        if(ReservedCardsForGame + (CardsPerPlayer * (_players.Count + 1)) > _deckOfCards.Count) throw
            new InvalidOperationException("Not enough cards in a deck");

        if (player.Hand.Cards.Count == 0) player.GiveCards(GetRandomCards(CardsPerPlayer));
        _players.Add(player);
    }

    public void ShuffleCards()
    {
        RefreshDeck();

        _gameCards.AddRange(GetRandomCards(CardsPerGame));

        _players.ForEach(p =>
        {
            p.GiveCards(GetRandomCards(CardsPerPlayer));
            p.SetGameCards(_gameCards);
        });
    }

    private List<Card> GetRandomCards(int amount)
    {
        List<Card> newCards = new();

        while (newCards.Count != amount)
        {
            int cardIndex = RandomNumberGenerator.GetInt32(_deckOfCards.Count - 1);

            newCards.Add(_deckOfCards[cardIndex]);
            _deckOfCards.RemoveAt(cardIndex);
        }

        return newCards;
    }

    public (Outcome outcome, List<Player> players) Result()
    {
        List<Player> winningPlayers = PlayersWithTheHighestHand();

        return winningPlayers.Count == 0 ? (Outcome.None, winningPlayers) :
               winningPlayers.Count == 1 ? (Outcome.Wins, winningPlayers) : (Outcome.Draw, winningPlayers);
    }

    private List<Player> PlayersWithTheHighestHand()
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