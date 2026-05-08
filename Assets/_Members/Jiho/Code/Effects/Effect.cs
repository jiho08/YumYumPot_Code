using GondrLib.ObjectPool.RunTime;
using UnityEngine;

namespace Code.Effects
{
    public class Effect : MonoBehaviour, IPoolable
    {
        [SerializeField] private float effectDuration;
        
        [field: SerializeField] public PoolItemSO PoolItem { get; private set; }
        public GameObject GameObject => gameObject;

        private Pool _myPool;

        public void Play(Vector3 pos)
        {
            transform.localPosition = pos;
            gameObject.SetActive(true);
            
            Invoke(nameof(Push), effectDuration);
        }
        
        public void Push()
        {
            _myPool.Push(this);
        }
        
        public void SetUpPool(Pool pool)
        {
            _myPool = pool;
        }

        public void ResetItem()
        {
        }
    }
}