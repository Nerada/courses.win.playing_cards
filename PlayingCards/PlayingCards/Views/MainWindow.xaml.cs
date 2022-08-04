// -----------------------------------------------
//     Author: Ramon Bollen
//      File: PlayingCards.MainWindow.xaml.cs
// Created on: 20220723
// -----------------------------------------------

using System.Diagnostics.CodeAnalysis;
using PlayingCards.ViewModels;

namespace PlayingCards.Views;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
[ExcludeFromCodeCoverage]
public partial class MainWindow
{
    public MainWindow(GameViewModel gameViewModel)
    {
        InitializeComponent();

        DataContext = gameViewModel;
    }
}