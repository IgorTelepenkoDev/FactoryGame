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
    private GameObject UiElemController;

    public bool isEventActive = false;

    // Start is called before the first frame update
    void Start()
    {
        // Assign default event to the list
        displayedEventPanel = GameObject.FindGameObjectWithTag("PanelFactoryEvent");
        ResourcesTimeManager = GameObject.FindGameObjectWithTag("ResourcesTimeManager");
        UiElemController = GameObject.Find("UiController");

        Events = EventCreator.LoadEvents();
        if (Events != null)
            NextEvents = new List<GameEvent> { Events[0] };
    }

    // Update is called once per frame
    void Update()
    {
        if (!ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().isPaused && NextEvents != null)
            foreach (var availableEvent in NextEvents)
            {
                if (availableEvent == Events.LastOrDefault())
                {
                    UiElemController.GetComponent<UiElementsController>().ActivateGameWinPanel();
                    return;
                }

                var triggerCondition = EventCreator.CreateTriggerCondition(availableEvent.TriggerCondition);

                if (triggerCondition())
                {
                    ActivateEvent(availableEvent);
                }
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
        Debug.Log(currentEvent);
        isEventActive = true;
    }

    public void AcceptEvent()
    {
        if (currentEvent == null)
        {
            isEventActive = false;
            return;
        }
        
        if(currentEvent.Accepted.NextEventIDs.Count != 0)
            NextEvents.AddRange(Events.Where(x => currentEvent.Accepted.NextEventIDs.Contains(x.ID)));

        NextEvents.Remove(NextEvents.Where(x => x.ID == currentEvent.ID).First());

        NextEvents.Select(x => x.ID).ToList().ForEach(x => Debug.Log(x));

        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().Balance += currentEvent.Accepted.BalanceChange;
        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().Expenses += currentEvent.Accepted.ExpensesChange;
        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().ChangeClimateBarValue(currentEvent.Accepted.ClimateChange);

        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().StartTime();
        isEventActive = false;
    }

    public void RejectEvent()
    {
        if (currentEvent == null)
        {
            isEventActive = false;
            return;
        }
        if (currentEvent.Accepted.NextEventIDs.Count != 0)
            NextEvents.AddRange(Events.Where(x => currentEvent.Rejected.NextEventIDs.Contains(x.ID)));

        NextEvents.Remove(NextEvents.Where(x => x.ID == currentEvent.ID).First());

        NextEvents.Select(x => x.ID).ToList().ForEach(x => Debug.Log(x));

        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().Balance += currentEvent.Rejected.BalanceChange;
        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().Expenses += currentEvent.Rejected.ExpensesChange;
        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().ChangeClimateBarValue(currentEvent.Rejected.ClimateChange);

        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().StartTime();
        isEventActive = false;
    }
}
