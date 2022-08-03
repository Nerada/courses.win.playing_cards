// -----------------------------------------------
//     Author: Ramon Bollen
//      File: PlayingCards.Outcome.cs
// Created on: 20220803
// -----------------------------------------------

namespace PlayingCards.Models;

public class Outcome
{
    public enum Result
    {
        None,
        Draw,
        Wins,
        Lost
    }

    public Outcome(Result hand, Result highestHand = Result.None)
    {
        Hand        = hand;
        HighestHand = highestHand;
    }

    public Result Hand { get; private set; }

    public Result HighestHand { get; private set; }

    public void UpdateHand(Result newHandResult) => Hand = newHandResult;

    public void UpdateHighestHand(Result newHighestHandResult) => HighestHand = newHighestHandResult;
}