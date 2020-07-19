using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils.Pool {
    public class ComponentObjectPool<T> where T : Component {
        private readonly Func<object[], T> _constructor;
        private readonly Queue<T> _free = new Queue<T>();
        private readonly HashSet<T> _used = new HashSet<T>();

        public ComponentObjectPool(Func<object[], T> constructor = null) {
            _constructor = constructor ?? Create;
        }

        public T Get(bool activate = true, params object[] parameters) {
            var result = _free.Count > 0 ? _free.Dequeue() : _constructor.Invoke(parameters);
            if (result is null) {
                return null;
            }

            _used.Add(result);

            if (activate) {
                result.gameObject.SetActive(true);
            }

            return result;
        }

        public T[] GetAllActive() {
            return _used.ToArray();
        }

        public void Release(T obj, float time = 0.0f) {
            if (time > 0.0f) {
                var handler = obj.GetComponent<PoolHandler>() ??
                              obj.gameObject.AddComponent<PoolHandler>();

                handler.AutoRelease(this, time, obj);
            } else {
                _used.Remove(obj);
                _free.Enqueue(obj);

                obj.gameObject.SetActive(false);
            }
        }

        public void ReleaseAll() {
            foreach (var obj in _used) {
                _free.Enqueue(obj);

                obj.gameObject.SetActive(false);
            }

            _used.Clear();
        }

        private T Create(params object[] parameters) {
            var go = new GameObject($"PO<{typeof(T)}>");
            var comp = go.AddComponent<T>();
            return comp;
        }
    }
}
