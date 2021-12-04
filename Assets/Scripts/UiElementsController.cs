using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiElementsController : MonoBehaviour
{
    private GameObject factoryEventManager;
    private GameObject displayedEventPanel;
    private CanvasGroup canvasGroupEventPanel;

    private GameObject displayedPausePanel;
    private CanvasGroup canvasGroupPause;
    private bool isPausedByButton = false;

    private GameObject displayedGameOverPanel;
    private CanvasGroup canvasGroupGameOver;

    private GameObject resourceTimeManager;
    // Start is called before the first frame update
    void Start()
    {
        displayedEventPanel = GameObject.FindGameObjectWithTag("PanelFactoryEvent");
        canvasGroupEventPanel = displayedEventPanel.GetComponent(typeof(CanvasGroup)) as CanvasGroup;
        factoryEventManager = GameObject.FindGameObjectWithTag("FactoryEventManager");

        resourceTimeManager = GameObject.FindGameObjectWithTag("ResourcesTimeManager");

        displayedPausePanel = GameObject.FindGameObjectWithTag("PausePanel");
        canvasGroupPause = displayedPausePanel.GetComponent(typeof(CanvasGroup)) as CanvasGroup;

        displayedGameOverPanel = GameObject.FindGameObjectWithTag("GameOverPanel");
        canvasGroupGameOver = displayedGameOverPanel.GetComponent(typeof(CanvasGroup)) as CanvasGroup;
    }

    // Update is called once per frame
    void Update()
    {
        if(factoryEventManager.GetComponent<FactoryEventManager>().isEventActive)
       {
            DialogBoxShow(canvasGroupEventPanel);
        }
       else
       {
           DialogBoxHide(canvasGroupEventPanel);
        }

    }

    public void ActivateMenu()
    {
        var menuPanel = GameObject.FindGameObjectWithTag("MenuPanel");

        //change to animation
        if (menuPanel.activeSelf == false)
            menuPanel.SetActive(true);
        else
            menuPanel.SetActive(false);
    }

    public void Pause()
    {
        if(isPausedByButton == true)
        {
            isPausedByButton = false;
            resourceTimeManager.GetComponent<ResourcesTimeAssigner>().StartTime();
            DialogBoxHide(canvasGroupPause);
        }
        else
        {
            isPausedByButton = true;
            resourceTimeManager.GetComponent<ResourcesTimeAssigner>().StopTime();
            DialogBoxShow(canvasGroupPause);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ActivateGameOverPanel()
    {
        DialogBoxShow(canvasGroupGameOver);
    }

    public void DialogBoxShow(CanvasGroup canvasGroupElem)
    {
        canvasGroupElem.alpha = 1;
        canvasGroupElem.blocksRaycasts = true;
    }

    public void DialogBoxHide(CanvasGroup canvasGroupElem)
    {
        canvasGroupElem.alpha = 0;
        canvasGroupElem.blocksRaycasts = false;
    }
}
