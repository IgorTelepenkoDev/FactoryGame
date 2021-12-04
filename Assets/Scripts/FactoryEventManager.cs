using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.FactoryEvents;
using Assets.Scripts.Models;
using System.Linq;
using System;

public class FactoryEventManager : MonoBehaviour
{
    public List<GameEvent> Events;

    private List<GameEvent> NextEvents;
    private GameEvent currentEvent = null;

    private GameObject displayedEventPanel;

    private GameObject ResourcesTimeManager;

    // Start is called before the first frame update
    void Start()
    {
        // Assign default event to the list
        displayedEventPanel = GameObject.FindGameObjectWithTag("PanelFactoryEvent");
        ResourcesTimeManager = GameObject.FindGameObjectWithTag("ResourcesTimeManager");

        Events = EventCreator.LoadEvents();
        if (Events != null)
            NextEvents = new List<GameEvent> { Events[0] };
    }

    // Update is called once per frame
    void Update()
    {
        if (!ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().isPaused && NextEvents != null)
            foreach (var availablEvent in NextEvents)
            {
                var triggerCondition = EventCreator.CreateTriggerCondition(availablEvent.TriggerCondition);

                if (triggerCondition())
                    ActivateEvent(availablEvent);
            }
    }

    void ActivateEvent(GameEvent startedEvent)
    {
        if (startedEvent == null)
            return;

        currentEvent = startedEvent;
        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().StopTime();

        var eventTitleField = displayedEventPanel.transform.
            Find("EventTitle").transform.Find("TextTitle").
            GetComponent(typeof(Text)) as Text;

        eventTitleField.text = startedEvent.Title;

        var eventDescriptionField = displayedEventPanel.transform.
            Find("TextEventDescription").
            GetComponent(typeof(Text)) as Text;

        eventDescriptionField.text = startedEvent.Description;
    }

    public void AcceptEvent()
    {
        if (currentEvent == null)
            return;
        // change to return a list of next events, not just one
        NextEvents.Add(Events.Where(x => x.ID == currentEvent.Accepted.NextEventID).First());

        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().Balance += currentEvent.Accepted.BalanceChange;
        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().Expenses += currentEvent.Accepted.ExpensesChange;
        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().ChangeClimateBarValue(currentEvent.Accepted.ClimateChange);

        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().StartTime();
    }

    public void RejectEvent()
    {
        if (currentEvent == null)
            return;
        // change to return a list of next events, not just one
        NextEvents.Add(Events.Where(x => x.ID == currentEvent.Rejected.NextEventID).First());

        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().Balance += currentEvent.Rejected.BalanceChange;
        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().Expenses += currentEvent.Rejected.ExpensesChange;
        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().ChangeClimateBarValue(currentEvent.Rejected.ClimateChange);

        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().StartTime();
    }
}
