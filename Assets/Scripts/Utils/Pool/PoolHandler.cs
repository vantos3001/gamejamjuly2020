using System.Collections;
using UnityEngine;

namespace Utils.Pool {
    public class PoolHandler : MonoBehaviour {
        public void AutoRelease<T>(ComponentObjectPool<T> pool, float time, T obj) where T : Component {
            StartCoroutine(ReleaseAfterTime(pool, time, obj));
        }

        private IEnumerator ReleaseAfterTime<T>(ComponentObjectPool<T> pool, float time, T obj) where T : Component {
            yield return new WaitForSeconds(time);
            pool.Release(obj);
        }
    }
}
