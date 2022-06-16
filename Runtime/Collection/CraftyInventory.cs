namespace NiftyFramework.NiftyCrafts
{
    public class CraftyInventory<TCollectable> where TCollectable : ICraftyCollectable
    {
        private ICraftySlotSet<TCollectable> _slotSet;
    }
}