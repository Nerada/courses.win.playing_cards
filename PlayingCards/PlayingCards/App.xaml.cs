// -----------------------------------------------
//     Author: Ramon Bollen
//      File: PlayingCards.App.xaml.cs
// Created on: 20220722
// -----------------------------------------------

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using PlayingCards.Models;
using PlayingCards.ViewModels;
using PlayingCards.Views;

namespace PlayingCards;

/// <summary>
///     http://www.tddbuddy.com/katas/Poker%20Hands.pdf
/// </summary>
[ExcludeFromCodeCoverage]
public partial class App
{
    private void OnStartup(object sender, StartupEventArgs e)
    {
        AppDomain.CurrentDomain.FirstChanceException +=
            (exceptionSender, args) => Logger.Log.Fatal($@"{exceptionSender}: {args.Exception.Message}{Environment.NewLine}
                                                        Full stacktrace:{Environment.NewLine}{new StackTrace(true)}");

        Game game = new();
        Logger.Log.Information("Game created.");

        GameViewModel gameViewModel = new(game);
        Logger.Log.Information("GameViewModel created.");

        MainWindow mainWindow = new(gameViewModel);
        Logger.Log.Information("MainWindow created.");

        mainWindow.Show();
        Logger.Log.Information("MainWindow shown.");
    }
}