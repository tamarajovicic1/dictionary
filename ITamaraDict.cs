using System;
using System.Collections.Generic;

public interface ITamaraDict<TKey, TValue>
{
  void Add(Tkey key, TValue value);

  TValue Get(Tkey key);
//get read-only, set menja vrednost
  TValue this[TKey key]{
  get; 
  set; }

  bool ContainsKey(TKey key);

  bool ContainsValue(TValue value);

  bool Remove(TKey key);

  void Clear();

  int Count {get; }

  IEnumerable <TKey> Keys {get; }

  IEnumerable <TValue> Values {get; }
}
