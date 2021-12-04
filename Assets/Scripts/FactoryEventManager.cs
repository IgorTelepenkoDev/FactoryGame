using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.FactoryEvents;

public class FactoryEventManager : MonoBehaviour
{
    private List<FactoryEvent> nextFactoryEvents;
    private FactoryEvent currentEvent = null;

    private GameObject displayedEventPanel;

    private GameObject ResourcesTimeManager;

    // Start is called before the first frame update
    void Start()
    {
        // Assign default event to the list
        displayedEventPanel = GameObject.FindGameObjectWithTag("PanelFactoryEvent");
        ResourcesTimeManager = GameObject.FindGameObjectWithTag("ResourcesTimeManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (!ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().isPaused)
            if(nextFactoryEvents != null)
                foreach (var availablEvent in nextFactoryEvents)
                {
                    if (availablEvent.TriggerCondition())
                        ActivateEvent(availablEvent);
                }
    }

    void ActivateEvent(FactoryEvent startedEvent)
    {
        if (startedEvent == null)
            return;

        currentEvent = startedEvent;
        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().StopTime();

        var eventTitleField = displayedEventPanel.transform.Find("EventTitle").
            GetComponent(typeof(Text)) as Text;
        eventTitleField.text = startedEvent.Title;

        var eventDescriptionField = displayedEventPanel.transform.Find("TextEventDescription").
            GetComponent(typeof(Text)) as Text;
        eventDescriptionField.text = startedEvent.Description;
    }

    public void AcceptEvent()
    {
        if (currentEvent == null)
            return;

        nextFactoryEvents = currentEvent.NextEventsIfAccepted;
        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().balance += currentEvent.
            BalanceChangeIfAccepted;
        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().expenses += currentEvent.
            ExpensesChangeIfAccepted;
        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().ChangeClimateBarValue(currentEvent.BalanceChangeIfAccepted);

        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().StartTime();
    }

    public void RejectEvent()
    {
        if (currentEvent == null)
            return;

        nextFactoryEvents = currentEvent.NextEventsIfRejected;
        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().balance += currentEvent.
            BalanceChangeIfRejected;
        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().expenses += currentEvent.
            ExpensesChangeIfRejected;
        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().ChangeClimateBarValue(currentEvent.BalanceChangeIfRejected);

        ResourcesTimeManager.GetComponent<ResourcesTimeAssigner>().StartTime();
    }
}
