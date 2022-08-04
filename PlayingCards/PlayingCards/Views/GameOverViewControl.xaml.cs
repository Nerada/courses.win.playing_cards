// -----------------------------------------------
//     Author: Ramon Bollen
//      File: PlayingCards.GameOverViewControl.xaml.cs
// Created on: 20220723
// -----------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using PlayingCards.ViewModels;

namespace PlayingCards.Views;

/// <summary>
///     Interaction logic for GameOverViewControl.xaml
/// </summary>
[ExcludeFromCodeCoverage]
public partial class GameOverViewControl
{
    private GameViewModel? _gameViewModel;

    public GameOverViewControl()
    {
        InitializeComponent();

        DataContextChanged += OnDataContextChanged;
    }

    private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (DataContext is not GameViewModel gameViewModel) throw new InvalidOperationException();

        _gameViewModel                 =  gameViewModel;
        _gameViewModel.PropertyChanged += OnGameViewModelPropertyChanged;
    }

    private void OnGameViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(GameViewModel.Error)) ClearErrors();
    }

    private async void ClearErrors()
    {
        await Task.Delay(5000);
        _gameViewModel?.ClearError();
    }
}