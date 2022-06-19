using NiftyFramework.Core.Condition;

namespace NiftyFramework.NiftyCrafts.Blueprint
{
    /// <summary>
    /// 
    /// </summary>
    public class Blueprint<TCollectable> : IBlueprint
    {
        public StatefulConditionSet ConditionSet { get; }
        public ICraftySlotOutput<TCollectable>[] SlotOutputs { get; }

        protected Blueprint()
        {
            
        }
        
        protected Blueprint(StatefulConditionSet conditionSet)
        {
            ConditionSet = conditionSet;
        }

        public Blueprint(StatefulCondition condition, ICraftySlotOutput<TCollectable> output)
        {
            ConditionSet = new StatefulConditionSet(new[] { condition }, StatefulConditionSet.Mode.All);
            SlotOutputs = new ICraftySlotOutput<TCollectable>[] { output };
            ConditionSet.OnStateChanged += HandleStateChange;
        }

        private void HandleStateChange(bool canOutput, StatefulCondition condition)
        {
            if (canOutput)
            {
                foreach (ICraftySlotOutput<TCollectable> output in SlotOutputs)
                {
                    output.Produce();
                }
            }
        }
    }

    public class Blueprint<TCollectable, TCondition, TOutput> : IBlueprint where TCondition : StatefulCondition where TOutput: ICraftySlotOutput<TCollectable>
    {
    }
}