namespace WeebreeOpen.VisualStudioServerLib.Domain.V1.Common
{
    using System.Collections.Generic;
    using System.Collections.Specialized;

    public class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, INotifyCollectionChanged
    {
        private readonly Dictionary<TKey, TValue> internalDictionary = new Dictionary<TKey, TValue>();

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public int Count
        {
            get
            {
                return this.internalDictionary.Count;
            }
        }
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }
        public ICollection<TKey> Keys
        {
            get
            {
                return this.internalDictionary.Keys;
            }
        }
        public ICollection<TValue> Values
        {
            get
            {
                return this.internalDictionary.Values;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                return this.internalDictionary[key];
            }
            set
            {
                TValue item;
                bool itemExists = this.internalDictionary.TryGetValue(key, out item);

                this.internalDictionary[key] = value;

                if (itemExists)
                {
                    this.OnCollectionChanged(NotifyCollectionChangedAction.Replace, new KeyValuePair<TKey, TValue>(key, value), new KeyValuePair<TKey, TValue>(key, item));
                }
                else
                {
                    this.OnCollectionChanged(NotifyCollectionChangedAction.Add, new KeyValuePair<TKey, TValue>(key, value));
                }
            }
        }

        public void Add(TKey key, TValue value)
        {
            this.internalDictionary.Add(key, value);
            this.OnCollectionChanged(NotifyCollectionChangedAction.Add, new KeyValuePair<TKey, TValue>(key, value));
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)this.internalDictionary).Add(item);
            this.OnCollectionChanged(NotifyCollectionChangedAction.Add, item);
        }

        public void Clear()
        {
            this.internalDictionary.Clear();
            this.OnCollectionChanged(NotifyCollectionChangedAction.Reset);
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)this.internalDictionary).Contains(item);
        }

        public bool ContainsKey(TKey key)
        {
            return this.internalDictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)this.internalDictionary).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this.internalDictionary.GetEnumerator();
        }

        public bool Remove(TKey key)
        {
            TValue item;

            if (this.internalDictionary.TryGetValue(key, out item))
            {
                this.internalDictionary.Remove(key);
                this.OnCollectionChanged(NotifyCollectionChangedAction.Remove, new KeyValuePair<TKey, TValue>(key, item));
                return true;
            }

            return false;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return this.Remove(item.Key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return this.internalDictionary.TryGetValue(key, out value);
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action)
        {
            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, new NotifyCollectionChangedEventArgs(action));
            }
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, KeyValuePair<TKey, TValue> changedItem)
        {
            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, new NotifyCollectionChangedEventArgs(action, changedItem));
            }
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, KeyValuePair<TKey, TValue> newItem, KeyValuePair<TKey, TValue> oldItem)
        {
            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, new NotifyCollectionChangedEventArgs(action, newItem, oldItem));
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.internalDictionary.GetEnumerator();
        }
    }
}