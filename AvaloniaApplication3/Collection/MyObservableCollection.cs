using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace AvaloniaApplication3.Collection;

public class MyObservableCollection<T> : IList<T>, INotifyCollectionChanged, INotifyPropertyChanged
{
    private List<T> _collection;

    public MyObservableCollection()
    {
        _collection = new List<T>();
    }

    public event NotifyCollectionChangedEventHandler CollectionChanged;

    public event PropertyChangedEventHandler PropertyChanged;

    public void Add(T item)
    {
        _collection.Add(item);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        throw new System.NotImplementedException();
    }

    public bool Remove(T item)
    {
        bool result = _collection.Remove(item);
        if (result)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
        }
        return result;
    }

    public void Clear()
    {
        _collection.Clear();
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    public bool Contains(T item)
    {
        return _collection.Contains(item);
    }

    public int Count => _collection.Count;
    
    public bool IsReadOnly { get; }

    public int IndexOf(T item)
    {
        return _collection.IndexOf(item);
    }

    public void Insert(int index, T item)
    {
        Add(item);
    }

    public void RemoveAt(int index)
    {
        throw new System.NotImplementedException();
    }

    public T this[int index]
    {
        get => _collection[index];
        set
        {
            T oldItem = _collection[index];
            _collection[index] = value;
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, oldItem, index));
        }
    }

    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        CollectionChanged?.Invoke(this, e);
        OnPropertyChanged(nameof(Count));
        OnPropertyChanged("Item[]");
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


    public IEnumerator<T> GetEnumerator()
    {
        return _collection.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_collection).GetEnumerator();
    }
}