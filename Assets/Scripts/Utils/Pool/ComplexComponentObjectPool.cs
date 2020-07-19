using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.Pool {
    public class ComplexComponentObjectPool<TKey, TComponent> where TComponent : Component {
        private readonly Func<object[], TComponent> _constructor;

        private readonly Dictionary<TKey, ComponentObjectPool<TComponent>> _pools =
            new Dictionary<TKey, ComponentObjectPool<TComponent>>();

        public ComplexComponentObjectPool(Func<object[], TComponent> constructor = null) {
            _constructor = constructor ?? Create;
        }

        private TComponent Create(params object[] parameters) {
            var go = new GameObject($"CPO<{typeof(TComponent)}, {typeof(TKey)}>");
            var comp = go.AddComponent<TComponent>();
            return comp;
        }

        public TComponent Get(TKey key, bool activate = true, params object[] parameters) {
            var found = _pools.TryGetValue(key, out var pool);
            if (!found) {
                pool = new ComponentObjectPool<TComponent>(_constructor);
                _pools[key] = pool;
            }

            return pool.Get(activate, parameters);
        }

        public void Release(TComponent obj, TKey key) {
            var found = _pools.TryGetValue(key, out var pool);
            if (!found) {
                pool = new ComponentObjectPool<TComponent>(_constructor);
                _pools[key] = pool;
            }

            pool.Release(obj);
        }

        public void ReleaseAll() {
            foreach (var pool in _pools.Values) {
                pool.ReleaseAll();
            }
        }
    }
}
