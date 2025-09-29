using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TamaraDict<TKey, TValue> : ITamaraDict<TKey, TValue>, IDictionary<TKey, TValue>
{
    private List<KeyValuePair<TKey, TValue>> _items;

    public TamaraDict()
    {
        _items = new List<KeyValuePair<TKey, TValue>>();
    }

    public TamaraDict(int capacity)
    {
        _items = new List<KeyValuePair<TKey, TValue>>(capacity);
    }

    public void Add(TKey key, TValue value)
    {
        if (ContainsKey(key))
            throw new ArgumentException("Ključ već postoji u rečniku.");
        _items.Add(new KeyValuePair<TKey, TValue>(key, value));
    }
//IDictionary Add i omogućava dodavanje KeyValuePair direktno u dict.
     public void Add(KeyValuePair<TKey, TValue> item)
    {
        Add(item.Key, item.Value);
    }
    
    public TValue Get(TKey key)
    {
        foreach (var kvp in _items)
        {
            if (EqualityComparer<TKey>.Default.Equals(kvp.Key, key))
                return kvp.Value;
        }
        throw new KeyNotFoundException("Ključ nije pronađen.");
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
                    _items[i] = new KeyValuePair<TKey, TValue>(key, value);
                    return;
                }
            }
            _items.Add(new KeyValuePair<TKey, TValue>(key, value));
        }
    }
    
 //ICollection<KeyValuePair> indekser, preusmerava indekser sa ITamaraDict na indekser koji već postoji.
    TValue IDictionary<TKey, TValue>.this[TKey key]
    {
        get => this[key];
        set => this[key] = value;
    }
    
    public bool ContainsKey(TKey key)
    {
        return _items.Any(kvp => EqualityComparer<TKey>.Default.Equals(kvp.Key, key));
    }

    public bool ContainsValue(TValue value)
    {
        return _items.Any(kvp => EqualityComparer<TValue>.Default.Equals(kvp.Value, value));
    }
//Da pronadje vrednost za dati ključ, ako ima true i postavlja value ako nema false i value je default odnosno TValue
     public bool TryGetValue(TKey key, out TValue value)
    {
        foreach (var kvp in _items)
        {
            if (EqualityComparer<TKey>.Default.Equals(kvp.Key, key))
            {
                value = kvp.Value;
                return true;
            }
        }
        value = default(TValue);
        return false;
    }

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
// Uklanja određeni KeyValuePair iz dict i vraća true ako je par uspešno uklonjen
    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (EqualityComparer<TKey>.Default.Equals(_items[i].Key, item.Key) &&
                EqualityComparer<TValue>.Default.Equals(_items[i].Value, item.Value))
            {
                _items.RemoveAt(i);
                return true;
            }
        }
        return false;
    } 

    public void Clear()
    {
        _items.Clear();
    }

    public int Count => _items.Count;
//Uvek vraća false jer dict podržava dodavanje i brisanje.
    public bool IsReadOnly => false;

    public IEnumerable<TKey> Keys => _items.Select(kvp => kvp.Key);

    public IEnumerable<TValue> Values => _items.Select(kvp => kvp.Value);
//Proverava da li dict sadrži dati KeyValuePair.
     public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        return _items.Any(kvp => EqualityComparer<TKey>.Default.Equals(kvp.Key, item.Key) &&
                                 EqualityComparer<TValue>.Default.Equals(kvp.Value, item.Value));
    }
//Kopira sve elemente dict u dati niz, počevši od određene pozicije u nizu koja je arrayIndex.
    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        if (array == null)
            throw new ArgumentNullException(nameof(array));
        if (arrayIndex < 0 || arrayIndex > array.Length)
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        if (array.Length - arrayIndex < _items.Count)
            throw new ArgumentException("Nema dovoljno prostora u nizu");

        for (int i = 0; i < _items.Count; i++)
        {
            array[arrayIndex + i] = _items[i];
        }
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

}
