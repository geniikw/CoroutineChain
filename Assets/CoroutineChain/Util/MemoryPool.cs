using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace geniikw.CChain
{
    public class MemoryPool<T> where T : new()
    {
        Stack<T> m_pool = new Stack<T>();

        private readonly Action<T> _OnSpawn;
        private readonly Action<T> _OnDespawn;

        public MemoryPool(Action<T> OnSpawn=null, Action<T> OnDespawn=null)
        {
            _OnDespawn = OnDespawn;
            _OnSpawn = OnSpawn;
        }

        public T Spawn()
        {
            T item;
            if (m_pool.Count == 0)
            {
                item = new T();
            }
            else
            {
                item = m_pool.Pop();
            }
            if(_OnSpawn != null)
                _OnSpawn(item);

            return item;
        }

        public void Despawn(T item)
        {
            if(_OnDespawn != null)
                _OnDespawn(item);
            m_pool.Push(item);
        }
    }

}