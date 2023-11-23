using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AvaloniaApplication3.Collection;
using AvaloniaApplication3.Commands;

namespace AvaloniaApplication3.ViewModels;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private string _greeting = "Welcome to Avalonia!";

    public string Greeting
    {
        get => _greeting;
        set => SetField(ref _greeting, value);
    }


    private RelayCommand? _defaultCommand;

    public RelayCommand DefaultCommand
    {
        get
        {
            return _defaultCommand ??
                   (_defaultCommand = new RelayCommand(o => { Greeting = "Hello, Avalonia!"; }));
        }
    }

    public IList<int> NewCollection { get; } = new[] { 1, 2, 3 };

    public IList<int> Collection { get; } = new MyObservableCollection<int>();

    public MainWindowViewModel()
    {

        var c = new ObservableCollection<int>();
        
        c.Add(1);
        Collection.Add(1);
        Collection.Add(2);
        Collection.Add(3);
    }

    private RelayCommand? _addCommand;

    public RelayCommand AddCommand
    {
        get
        {
            return _addCommand ??= new RelayCommand(o =>
            {
                var i
                    = new Random().Next(0, 100);
                Collection.Add(i);
            });
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}