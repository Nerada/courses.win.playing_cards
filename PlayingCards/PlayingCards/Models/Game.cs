using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using PlayingCards.Extensions;

namespace PlayingCards.Models;

public class Game
{
    private const int CardsPerGame = 5;

    private const int CardsPerPlayer       = 2;
    private const int ReservedCardsForGame = 10;


    private readonly List<Card> _deckOfCards = new();
    private readonly List<Card> _gameCards   = new();

    private readonly List<Player> _players = new();

    public Game()
    {
        RefreshDeck();
    }

    public IReadOnlyList<Card> GameCards => new ReadOnlyCollection<Card>(_gameCards);

    public IReadOnlyList<Player> Players => new ReadOnlyCollection<Player>(_players);

    public IReadOnlyList<(Player player, Outcome result)> Result() => PlayersWithTheHighestHand();

    public void AddPlayer(Player player)
    {
        if (ReservedCardsForGame + CardsPerPlayer * (_players.Count + 1) > _deckOfCards.Count) throw new InvalidOperationException("Not enough cards in a deck");

        if (player.Hand.Cards.Count == 0) player.GiveCards(GetRandomCards(CardsPerPlayer));
        _players.Add(player);
    }

    public void RemovePlayer(Player player)
    {
        _deckOfCards.AddRange(player.Hand.PlayerCards);
        _players.Remove(player);
    }

    public void GiveCards(string cardsString)
    {
        _gameCards.Clear();
        _gameCards.AddRange(cardsString.ToCards());

        _players.ForEach(p => p.SetGameCards(_gameCards));
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
            int cardIndex = RandomNumberGenerator.GetInt32(_deckOfCards.Count);

            newCards.Add(_deckOfCards[cardIndex]);
            _deckOfCards.RemoveAt(cardIndex);
        }

        return newCards;
    }

    private List<(Player player, Outcome result)> PlayersWithTheHighestHand()
    {
        List<(Player player, Outcome result)> winningPlayers = new();

        foreach (Player player in _players)
        {
            if (winningPlayers.Count == 0)
            {
                winningPlayers.Add((player, new Outcome(Outcome.Result.Wins, Outcome.Result.Wins)));

                continue;
            }

            Outcome playOutcome = player.Hand.Play(winningPlayers[0].player.Hand);

            switch (playOutcome.Hand)
            {
                case Outcome.Result.Draw:
                    winningPlayers[0].result.UpdateHand(Outcome.Result.Draw);

                    if (playOutcome.HighestHand != Outcome.Result.Lost)
                    {
                        winningPlayers[0].result.UpdateHighestHand(playOutcome.HighestHand == Outcome.Result.Wins ? Outcome.Result.Lost : Outcome.Result.Draw);
                        winningPlayers.Insert(0, (player, playOutcome));
                    }
                    else
                    {
                        winningPlayers[0].result.UpdateHighestHand(Outcome.Result.Wins);
                        winningPlayers.Add((player, playOutcome));
                    }

                    continue;
                case Outcome.Result.Wins:
                    winningPlayers.Clear();
                    winningPlayers.Add((player, playOutcome));
                    break;
            }
        }

        return winningPlayers;
    }

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
}