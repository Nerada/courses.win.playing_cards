using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Media;
using PlayingCards.ViewModels;

namespace PlayingCards.Views;

/// <summary>
///     Interaction logic for CreatePlayerControl.xaml
/// </summary>
[ExcludeFromCodeCoverage]
public partial class CreatePlayerControl
{
    // ReSharper disable once StringLiteralTypo
    private readonly IReadOnlyList<string> _suggestedNames = new ReadOnlyCollection<string>(new List<string>
    {
        "Freya", "Skadi", "Thor", "Odin", "Loki", "Valkyrie"
    });

    private GameViewModel? _gameViewModel;

    public CreatePlayerControl()
    {
        InitializeComponent();

        DataContextChanged += OnDataContextChanged;
    }

    private string SuggestedPlayerName
    {
        get
        {
            List<string> suggestedNames = _gameViewModel != null ? _suggestedNames.Except(_gameViewModel.Players.Select(p => p.PlayerName).ToList()).ToList() : new List<string>();

            return suggestedNames.Count == 0 ? string.Empty : suggestedNames[RandomNumberGenerator.GetInt32(suggestedNames.Count)];
        }
    }

    private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (DataContext is not GameViewModel gameViewModel) throw new InvalidOperationException();

        _gameViewModel                 =  gameViewModel;
        _gameViewModel.PropertyChanged += OnGameViewModelPropertyChanged;

        ResetPlayerName();
    }

    private void OnGameViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(GameViewModel.Players)) ResetPlayerName();
    }

    private void OnPlayerNameFocus(object _, RoutedEventArgs __) => PreparePlayerNameForInput();

    private void OnPlayerNameLostFocus(object _, RoutedEventArgs __)
    {
        if (!string.IsNullOrWhiteSpace(PlayerName.Text)) return;

        ResetPlayerName();
    }

    private void PreparePlayerNameForInput()
    {
        PlayerName.Text       = string.Empty;
        PlayerName.Foreground = new SolidColorBrush(Colors.DarkSlateGray);
    }

    private void ResetPlayerName()
    {
        PlayerName.Text       = SuggestedPlayerName;
        PlayerName.Foreground = new SolidColorBrush(Colors.LightGray);
    }
}