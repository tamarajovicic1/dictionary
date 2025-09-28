using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Dictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
{
    //Lista u kojoj čuvamo sve parove ključeva i vrednosi.
    private List<KeyValuePair<TKey, TValue>> _items;

    //Pravimo prazan rečnik.
    public Dictionary()
    {
        _items = new List<KeyValuePair<TKey, TValue>>();
    }

    //Početni kapacitet, ako unapred znamo koliko će elemenata biti.
    public Dictionary(int capacity)
    {
        _items = new List<KeyValuePair<TKey, TValue>>(capacity);
    }

    //Ako ključ već postoji izbacuje izuztetak.
    public void Add(TKey key, TValue value)
    {
        if (ContainsKey(key))
            throw new ArgumentException("Ključ već postoji u rečniku.");
        _items.Add(new KeyValuePair<TKey, TValue>(key, value));
    }

    //Ako ključ nije pronadjen izbacuje izuzetak.
    public TValue Get(TKey key)
    {
        foreach (var kvp in _items)
        {
            if (EqualityComparer<TKey>.Default.Equals(kvp.Key, key))
                return kvp.Value;
        }
        throw new KeyNotFoundException("Ključ nije pronadjen.");
    }

    
    public TValue this[TKey key]
    {
        get => Get(key);
        set
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (EqualityComparer<TKey>.Default.Equals(_items[i].Key, key))
                {
                    // Ako ključ postoji, menja vrednost
                    _items[i] = new KeyValuePair<TKey, TValue>(key, value);
                    return;
                }
            }
            // Ako ključ ne postoji, dodaje novi par u rečnik.
            _items.Add(new KeyValuePair<TKey, TValue>(key, value));
        }
    }
    // Provera da li rečnik sadrži određeni ključ.
    public bool ContainsKey(TKey key)
    {
        return _items.Any(kvp => EqualityComparer<TKey>.Default.Equals(kvp.Key, key));
    }

    // Provera da li rečnik sadrži određenu vrednost.
    public bool ContainsValue(TValue value)
    {
        return _items.Any(kvp => EqualityComparer<TValue>.Default.Equals(kvp.Value, value));
    }

    // Uklanja element sa datim ključem.
    // Vraća true ako je uspešno obrisan, false ako ključ ne postoji.
    public bool Remove(TKey key)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (EqualityComparer<TKey>.Default.Equals(_items[i].Key, key))
            {
                _items.RemoveAt(i);
                return true;
            }

        }
        return false;
    }

    // Briše sve elemente iz rečnika.
    public void Clear()
    {
        _items.Clear();
    }

    // Vraća broj elemenata u rečniku.
    public int Count => _items.Count;
    // Vraća sve ključeve u rečniku.
    public IEnumerable<TKey> Keys
    {
        get
        {
            foreach (var kvp in _items)
                yield return kvp.Key;
        }
    }
    // Vraća sve vrednosti u rečniku.
        public IEnumerable<TValue> Values
    {
        get
        {
            foreach (var kvp in _items)
                yield return kvp.Value;
        }
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

}