using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    public class Check
    {
        public string Field { get; set; }
        public string Operation { get; set; }
        public string Value { get; set; }
    }

    public class TriggerCondition
    {
        public string Date { get; set; }
        public List<Check> Checks { get; set; }
    }

    public class Accepted
    {
        public int BalanceChange { get; set; }
        public int ExpensesChange { get; set; }
        public string NextEventID { get; set; }
    }

    public class Rejected
    {
        public int BalanceChange { get; set; }
        public int ExpensesChange { get; set; }
        public string NextEventID { get; set; }
    }

    public class GameEvent
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TriggerCondition TriggerCondition { get; set; }
        public Accepted Accepted { get; set; }
        public Rejected Rejected { get; set; }
    }

    public class Root
    {
        public List<GameEvent> Events { get; set; }
    }
}
