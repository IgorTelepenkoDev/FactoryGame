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
    }

    // Update is called once per frame
    void Update()
    {
        if (!ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().isPaused && NextEvents != null)
            foreach (var availablEvent in NextEvents)
            {
                Func<bool> triggerCondition = EventCreator.CreateTriggerCondition(availablEvent.TriggerCondition);

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
            Find("EventTitle").
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

        NextEvents.Add(Events.Where(x => x.ID == currentEvent.Accepted.NextEventID).First());

        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().balance += currentEvent.Accepted.BalanceChange;
        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().expenses += currentEvent.Accepted.ExpensesChange;

        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().StartTime();
    }

    public void RejectEvent()
    {
        if (currentEvent == null)
            return;

        NextEvents.Add(Events.Where(x => x.ID == currentEvent.Rejected.NextEventID).First());

        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().balance += currentEvent.Rejected.BalanceChange;
        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().expenses += currentEvent.Rejected.ExpensesChange;

        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().StartTime();
    }
}
