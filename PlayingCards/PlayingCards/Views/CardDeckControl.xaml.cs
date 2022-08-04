using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using PlayingCards.Models;

namespace PlayingCards.Views;

[ExcludeFromCodeCoverage]
public partial class CardDeckControl
{
    public static readonly DependencyProperty CardsProperty = DependencyProperty.Register(nameof(Cards), typeof(IReadOnlyList<Card>), typeof(CardDeckControl),
                                                                                          new PropertyMetadata(default(IReadOnlyList<Card>)));

    public CardDeckControl()
    {
        InitializeComponent();
    }

    public IReadOnlyList<Card> Cards
    {
        get => (IReadOnlyList<Card>)GetValue(CardsProperty);
        set => SetValue(CardsProperty, value);
    }
}