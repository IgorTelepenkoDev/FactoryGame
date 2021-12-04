using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiElementsController : MonoBehaviour
{
    bool isEventActive;
    private GameObject factoryEventManager;
    private GameObject displayedEventPanel;
    private CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        displayedEventPanel = GameObject.FindGameObjectWithTag("PanelFactoryEvent");
        canvasGroup = displayedEventPanel.GetComponent(typeof(CanvasGroup)) as CanvasGroup;
        factoryEventManager = GameObject.FindGameObjectWithTag("FactoryEventManager");
        

    }

    // Update is called once per frame
    void Update()
    {
        if(factoryEventManager.GetComponent<FactoryEventManager>().isEventActive)
       {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }
       else
       {
           canvasGroup.alpha = 0;
           canvasGroup.blocksRaycasts = false;
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

    public void DialogBoxShow()
    {
        canvasGroup.alpha = 1;
    }

    public void DialogBoxHide()
    {
        canvasGroup.alpha = 0;
    }
}
