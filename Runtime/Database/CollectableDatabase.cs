using System;
using System.Collections.Generic;
using UnityEngine;

namespace NiftyFramework.NiftyCrafts
{ 
    public class CollectableDatabase
    {
        [SerializeField] protected CraftyCollectableData[] _collectableDataList;
        [SerializeField] protected bool _cacheOnLoad;

        protected List<CraftyCollectableData> _runtimeData;
        
        private Dictionary<string, ICraftyCollectable> _collectableCache;

        public CollectableDatabase(CraftyCollectableData[] collectableData, bool cache)
        {
            _collectableDataList = collectableData;
            if (cache)
            {
                Cache();
            }
        }
        
        public ICraftyCollectable GetCollectable(CraftyCollectableData craftyCollectableData)
        {
            if (!_collectableCache.ContainsKey(craftyCollectableData.Id))
            {
                _collectableCache[craftyCollectableData.Id] = craftyCollectableData.Create();
            }
            return _collectableCache[craftyCollectableData.Id];
        }

        public ICraftyCollectable GetCollectable(string id)
        {
            if (!_collectableCache.ContainsKey(id))
            {
                throw new Exception("CollectableDatabase cannot return Collectable id " + id + " as it hasn't been cached");
            }
            return _collectableCache[id];
        }

        public bool TryGetCollectable(string id, ref ICraftyCollectable collectable)
        {
            if (_collectableCache.ContainsKey(id))
            {
                collectable = _collectableCache[id];
                return true;
            }
            return false;
        }

        public void AddCollectable(CraftyCollectable collectable)
        {
            if (_collectableCache.ContainsKey(collectable.Id))
            {
                throw new Exception("CollectableDatabase cannot assign Collectable " + collectable +
                                    " as the id is already in the database as " + _collectableCache[collectable.Id]);
            }
            _collectableCache[collectable.Id] = collectable;
        }

        public void RemoveCollectable(CraftyCollectable collectable)
        {
            _collectableCache.Remove(collectable.Id);
        }
        
        public void RemoveCollectable(string id)
        {
            _collectableCache.Remove(id);
        }

        public void Cache()
        {
            if (_collectableCache == null)
            {
                _collectableCache = new Dictionary<string, ICraftyCollectable>();
            }
            if (_collectableDataList != null)
            {
                for (int i = 0; i < _collectableDataList.Length; i++)
                {
                    CraftyCollectableData collectableData = _collectableDataList[i];
                    if (collectableData != null && !string.IsNullOrEmpty(collectableData.Id))
                    {
                        if (!_collectableCache.ContainsKey(collectableData.Id))
                        {
                            _collectableCache.Add(collectableData.Id, collectableData.Create());
                        }
                    }
                }
            }
        }

        private void OnEnable()
        {
            if (_cacheOnLoad)
            {
               Cache();
            }
        }
    }
}