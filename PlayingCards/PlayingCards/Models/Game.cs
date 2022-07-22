namespace PlayingCards.Models;

public static class Game
{
    public enum Outcome
    {
        Draw,
        Wins,
        Lost
    }

    public static Outcome Play(Player playerA, Player playerB) => playerA.HighestCard().CompareTo(playerB.HighestCard());
}