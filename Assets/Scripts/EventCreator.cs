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
        var resourceTimeAssigner = resourcesTimeManager.GetComponent<ResourcesTimeAssigner>();

        var expressions = new List<Expression<Func<bool>>>();

        var curTime = resourceTimeAssigner.Date;
        var jsonTime = Convert.ToDateTime(condition.Date);

        if(curTime < jsonTime)
            return () => false;
        else if (condition.Checks.Count == 0)
            return () => true;

        foreach (var check in condition.Checks)
        {
            ConstantExpression x = null;
            ConstantExpression y = null;
            BinaryExpression exp = null;

            if (check.Field == "Balance") 
                x = Expression.Constant(resourceTimeAssigner.Balance);
            else if (check.Field == "Expenses")
                x = Expression.Constant(resourceTimeAssigner.Expenses);
            else if (check.Field == "Climate")
                x = Expression.Constant(resourceTimeAssigner.GetClimateBarValue());

            y = Expression.Constant(check.Value);

            if (check.Operator == ">")
                exp = Expression.GreaterThan(x, y);
            else if (check.Operator == "<")
                exp = Expression.LessThan(x, y);
            else if (check.Operator == "=")
                exp = Expression.Equal(x, y);

            expressions.Add(Expression.Lambda<Func<bool>>(exp));
        }

        var firstExpression = expressions[0];
        expressions.RemoveAt(0);

        var finalExpression = expressions.Aggregate(firstExpression, (acc, i) => Expression.Lambda<Func<bool>>(Expression.And(acc, i))).Compile();

        return finalExpression;
    }
}
