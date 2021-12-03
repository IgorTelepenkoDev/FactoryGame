using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.FactoryEvents
{
    public class FactoryEvent
    {
        public string Title { get; set; }
        public string DescriptionText { get; set; }

        public Func<bool> TriggerCondition { get; set; }

        public int BalanceChange { get; set; }
        public int ExpensesChange { get; set; }

        public List<FactoryEvent> NextEventsIfAccepted { get; set; }
        public List<FactoryEvent> NextEventsIfRejected { get; set; }
    }
}
