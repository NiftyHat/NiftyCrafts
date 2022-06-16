using UnityEngine;

namespace NiftyFramework.NiftyCrafts
{
    public class CraftyCollectableData : ScriptableObject, ICraftyCollectableData
    {
        [SerializeField] 
        protected string _name;
        public string Id { get; private set; }
        public string Name => _name;
        public string DisplayName => _name;

        public CraftyCollectable Create()
        {
            Id = name;
            return new CraftyCollectable(Id);
        }

        public override string ToString()
        {
            return base.ToString() + Id;
        }
    }
}