using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private GameObject displayGameWinPanel;
    private CanvasGroup canvasGroupGameWin;

    private GameObject displayMenuPanel;
    private CanvasGroup canvasGroupMenu;

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

        displayGameWinPanel = GameObject.FindGameObjectWithTag("GameWinPanel");
        canvasGroupGameWin = displayGameWinPanel.GetComponent(typeof(CanvasGroup)) as CanvasGroup;

        displayMenuPanel = GameObject.FindGameObjectWithTag("MenuPanel");
        canvasGroupMenu = displayMenuPanel.GetComponent(typeof(CanvasGroup)) as CanvasGroup;
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
        if(canvasGroupMenu.alpha == 0)
        {
            DialogBoxShow(canvasGroupMenu);
        }
        else
        {
            DialogBoxHide(canvasGroupMenu);
        }
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

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void ActivateGameOverPanel()
    {
        DialogBoxShow(canvasGroupGameOver);
        resourceTimeManager.GetComponent<ResourcesTimeAssigner>().StopTime();
    }

    public void ActivateGameWinPanel()
    {
        DialogBoxShow(canvasGroupGameWin);
        resourceTimeManager.GetComponent<ResourcesTimeAssigner>().StopTime();
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
