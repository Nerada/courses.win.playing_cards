// -----------------------------------------------
//     Author: Ramon Bollen
//      File: PlayingCards.ViewModelBase.cs
// Created on: 20220723
// -----------------------------------------------

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace PlayingCards.ViewModels;

/// <summary>
///     View model base class.
/// </summary>
[ExcludeFromCodeCoverage]
public class ViewModelBase : INotifyPropertyChanged
{
    private static readonly ConcurrentDictionary<string, PropertyChangedEventArgs>
        CachedPropertyChangedEventArgs = new();

    public event PropertyChangedEventHandler? PropertyChanged;

    protected bool Set<T>(ref T field, T newValue, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, newValue)) return false;

        SetAndRaisePropertyChanged(out field, newValue, propertyName);
        return true;
    }

    protected void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
    {
        if (propertyName == null) return;

        PropertyChanged?.Invoke(this, GetCachedPropertyChangedEventArgs(propertyName));
    }

    private PropertyChangedEventArgs GetCachedPropertyChangedEventArgs(string propertyName)
    {
        if (!CachedPropertyChangedEventArgs.TryGetValue(propertyName, out PropertyChangedEventArgs? args))
        {
            args = new PropertyChangedEventArgs(propertyName);
            CachedPropertyChangedEventArgs.TryAdd(propertyName, args);
        }

        return args;
    }

    private void SetAndRaisePropertyChanged<T>(out T field, T newValue, string? propertyName)
    {
        field = newValue;
        RaisePropertyChanged(propertyName);
    }
}