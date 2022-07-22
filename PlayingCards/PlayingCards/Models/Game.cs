using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    private readonly List<Player> _players = new();

    public IReadOnlyList<Player> Players => new ReadOnlyCollection<Player>(_players);

    public void AddPlayer(Player player) => _players.Add(player);

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