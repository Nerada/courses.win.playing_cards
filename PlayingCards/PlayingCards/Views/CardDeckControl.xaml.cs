using System.Collections.Generic;
using System.Windows;
using PlayingCards.Models;

namespace PlayingCards.Views;

public partial class CardDeckControl
{
    public static readonly DependencyProperty CardsProperty = DependencyProperty.Register(
        "Cards", typeof(IReadOnlyList<Card>), typeof(CardDeckControl), new PropertyMetadata(default(IReadOnlyList<Card>)));

    public IReadOnlyList<Card> Cards
    {
        get => (IReadOnlyList<Card>)GetValue(CardsProperty);
        set => SetValue(CardsProperty, value);
    }

    public CardDeckControl()
    {
        InitializeComponent();
    }
}