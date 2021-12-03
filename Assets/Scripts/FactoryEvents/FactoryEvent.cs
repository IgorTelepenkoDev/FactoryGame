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
        public string Description { get; set; }

        public Func<bool> TriggerCondition { get; set; }

        public int BalanceChangeIfAccepted { get; set; }
        public int ExpensesChangeIfAccepted { get; set; }
        public int BalanceChangeIfRejected { get; set; }
        public int ExpensesChangeIfRejected { get; set; }

        public List<FactoryEvent> NextEventsIfAccepted { get; set; }
        public List<FactoryEvent> NextEventsIfRejected { get; set; }
    }
}
