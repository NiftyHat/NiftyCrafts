using NiftyFramework.NiftyCrafts;
using NiftyFramework.NiftyCrafts.Slot;

namespace Crafty.Test
{
    public class InventoryTests
    {
        private class TestData
        {
           public readonly CraftyCollectable coin = new CraftyCollectable("coin");
           public readonly CraftyCollectable helm = new CraftyCollectable("helm");
           public readonly CraftyCollectable potion = new CraftyCollectable("potion");
           public readonly CraftyCollectable keyItem = new CraftyCollectable("keyItem");
        }
        
        public void TestSlotSet()
        {
            var slotSet = new CraftySlotSet<CraftyCollectable>(10);
            var testData = new TestData();
            slotSet.Add(new CraftySlot<CraftyCollectable>(testData.keyItem));
            slotSet.Add(new CraftySlotStacking<CraftyCollectable>(testData.coin, 100));
            slotSet.Add(new CraftySlotStacking<CraftyCollectable>(testData.helm));
            slotSet.Add(new CraftySlotStacking<CraftyCollectable>(testData.potion, 5));
            slotSet.Add(new CraftySlotStacking<CraftyCollectable>(testData.coin, 100));
            
        }
    }
}