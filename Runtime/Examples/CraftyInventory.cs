namespace NiftyFramework.NiftyCrafts.Examples
{
    public class CraftyInventory<TCollectable>
    {
        public CraftySlotSet<TCollectable> _slotSet;

        public CraftyInventory(CraftySlotSet<TCollectable> slotSet)
        {
            _slotSet = slotSet;
        }

        public ICraftySlot<TCollectable> GetFirstEmptySlot()
        {
            return _slotSet.GetFirstSlot<ICraftySlot<TCollectable>>(slot => slot.IsEmpty());
        }
    }
}