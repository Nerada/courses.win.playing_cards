using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using PlayingCards.Extensions;
using PlayingCards.Models;

namespace PlayingCards.Views;

[ExcludeFromCodeCoverage]
public partial class CardControl
{
    public static readonly DependencyProperty CardProperty = DependencyProperty.Register(nameof(Card), typeof(Card), typeof(CardControl), new PropertyMetadata(default(Card), OnCardChanged));
    private readonly       Image              _aceImage    = new() {Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/ace.png")), Opacity   = 0.4, UseLayoutRounding = true};
    private readonly       Image              _jackImage   = new() {Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/jack.png")), Opacity  = 0.4, UseLayoutRounding = true};
    private readonly       Image              _kingImage   = new() {Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/king.png")), Opacity  = 0.4, UseLayoutRounding = true};
    private readonly       Image              _queenImage  = new() {Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/queen.png")), Opacity = 0.4, UseLayoutRounding = true};

    public CardControl()
    {
        InitializeComponent();
    }

    public Card Card
    {
        get => (Card)GetValue(CardProperty);
        set => SetValue(CardProperty, value);
    }

    private static void OnCardChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        CardControl? cardControl = d as CardControl;
        cardControl?.UpdateLabels();
    }

    private void UpdateLabels()
    {
        UpdateValue(ValueTopLeft);
        UpdateValue(ValueTopRight);
        UpdateValue(ValueBottomRight);
        UpdateValue(ValueBottomLeft);
        UpdateSuit(SuitTopLeft);
        UpdateSuit(SuitTopRight);
        UpdateSuit(SuitBottomRight);
        UpdateSuit(SuitBottomLeft);
    }

    private void UpdateSuit(Label suitLabel)
    {
        suitLabel.Content    = Card.Suit.ToSuitChar().ToString();
        suitLabel.Foreground = Card.Suit.ToSuitColor();
    }

    private void UpdateValue(Label valueLabel)
    {
        valueLabel.Content    = Card.Value.ToValueString();
        valueLabel.Foreground = Card.Suit.ToSuitColor();
        SetImage();
    }

    private void SetImage()
    {
        ImageGrid.Children.Clear();
        ImageGrid.Margin = new Thickness(0, 0, 0, 0);

        switch (Card.Value)
        {
            case Card.ValueType.Ace:
                ImageGrid.Children.Add(_aceImage);
                ImageGrid.Margin = new Thickness(-20, -25, -25, -25);
                return;
            case Card.ValueType.King:
                ImageGrid.Children.Add(_kingImage);
                ImageGrid.Margin = new Thickness(-20, -20, -20, -20);
                return;
            case Card.ValueType.Queen:
                ImageGrid.Children.Add(_queenImage);
                ImageGrid.Margin = new Thickness(-30, -20, -45, -60);
                return;
            case Card.ValueType.Jack:
                ImageGrid.Children.Add(_jackImage);
                ImageGrid.Margin = new Thickness(-40, -30, -40, -60);
                return;
        }
    }
}