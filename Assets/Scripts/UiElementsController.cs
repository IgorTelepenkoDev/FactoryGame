using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiElementsController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
