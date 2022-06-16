using NiftyFramework.Core.Condition;

namespace NiftyFramework.NiftyCrafts
{
    /// <summary>
    /// Condition
    /// </summary>
    public class TickCondition : StatefulCondition
    {
        private int _ticks;
        private readonly int _ticksRequired;
        
        public TickCondition(int ticksRequired = 1)
        {
            _ticksRequired = ticksRequired;
        }

        public void Tick(int tickCount)
        {
            _ticks += tickCount;
            StateChange(_ticks > _ticksRequired);
        }
    }
}