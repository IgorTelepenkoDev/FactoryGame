using Assets.Scripts.FactoryEvents;
using Assets.Scripts.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;

public static class EventCreator
{
    public static List<GameEvent> LoadEvents()
    {
        List<GameEvent> events;

        using (StreamReader file = File.OpenText(@"Assets/Data/Events.json"))
        using (JsonTextReader reader = new JsonTextReader(file))
        {
            var obj = JToken.ReadFrom(reader) as JObject;
            events = JsonConvert.DeserializeObject<Root>(obj.ToString()).Events;
        }

        return events;
    }

    public static Func<bool> CreateTriggerCondition(TriggerCondition condition)
    {
        var resourcesTimeManager = GameObject.FindGameObjectWithTag("ResourcesTimeManager");

        foreach (var check in condition.Checks)
        {
            ConstantExpression x = null;
            ConstantExpression y = null;


            if (check.Field == "Balance") 
                x = Expression.Constant(resourcesTimeManager.GetComponent<ResourcesTimeAssigner>().balance);
            else if (check.Field == "Expenses")
                x = Expression.Constant(resourcesTimeManager.GetComponent<ResourcesTimeAssigner>().expenses);
            else if (check.Field == "Climate") ;
            //x = Expression.Constant(climate)

            y = Expression.Constant(check.Value);

            if (check.Operation == ">") ;
            else if (check.Operation == "<") ;
            else if (check.Operation == "=") ;

        }
    }
}
