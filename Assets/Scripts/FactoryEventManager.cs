using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.FactoryEvents;

public class EventManager : MonoBehaviour
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
            foreach (var availablEvent in nextFactoryEvents)
            {
                if (availablEvent.TriggerCondition())
                    ActivateEvent(availablEvent);


            }
    }

    void ActivateEvent(FactoryEvent startedEvent)
    {
        currentEvent = startedEvent;
        var eventTitleField = displayedEventPanel.transform.Find("EventTitle").GetComponent(typeof(Text)) as Text;
    }

    public void AcceptEvent()
    {

    }

    public void RejectEvent()
    {

    }
}
