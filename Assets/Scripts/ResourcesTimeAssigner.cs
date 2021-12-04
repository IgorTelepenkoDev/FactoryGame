using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ResourcesTimeAssigner : MonoBehaviour
{
    public Text BalanceText;
    public Text CurrentDate;
    public Text ExpensesText;
    public Text ClimateText;
    public DateTime Date = DateTime.Now;

    public int Expenses { get; set; }
    public int Balance { get; set; }
    public int Climate { get; set; }

    public bool isPaused = false;
    public float timeUnit = 2f;

    private Coroutine timer;
    private GameObject climateChangeBar;

    private GameObject UIElemCOntroller;

    // Start is called before the first frame update
    void Start()
    {
        Balance = 10000;
        Expenses = 200;

        BalanceText = GameObject.Find("FieldBalance").GetComponent(typeof(Text)) as Text;
        CurrentDate = GameObject.Find("FieldDate").GetComponent(typeof(Text)) as Text;
        ExpensesText = GameObject.Find("FieldMonthExpenses").GetComponent(typeof(Text)) as Text;
        climateChangeBar = GameObject.Find("ClimateBarInner");
        UIElemCOntroller = GameObject.Find("UiController");
        CurrentDate.text = Date.ToString();

        timer = StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            UIElemCOntroller.GetComponent<UiElementsController>().Pause();
        }

        BalanceText.text = Balance.ToString();
        ExpensesText.text = Expenses.ToString();
    }

    IEnumerator Timer(float countTime = 1f)
    {
        countTime = timeUnit;
        while(true)
        {
            yield return new WaitForSeconds(countTime);

            Date = Date.AddMonths(1);
            CurrentDate.text = Date.ToString();
            Balance -= Expenses;
        }
    }

    public void StopTime()
    {
        isPaused = true;   

        StopCoroutine(timer);
    }

    public void StartTime()
    {
        isPaused = false;

        timer = StartCoroutine(Timer());
    }

    public void ChangeClimateBarValue(int climateChangeRate)
    {
        var climateChangeBarValueOffset = 
            climateChangeBar.GetComponent<RectTransform>().offsetMax.x + climateChangeRate;
        
        if (climateChangeBarValueOffset > 0)
            climateChangeBarValueOffset = 0;
        else if (climateChangeBarValueOffset < -100)
            climateChangeBarValueOffset = -97;

        climateChangeBar.GetComponent<RectTransform>().offsetMax = new 
            Vector2(climateChangeBarValueOffset, climateChangeBar.GetComponent<RectTransform>().offsetMax.y);
    }

    public int GetClimateBarValue()
    {
        var climateChangeBarValueOffset =
            climateChangeBar.GetComponent<RectTransform>().offsetMax.x;
        return Convert.ToInt32(climateChangeBarValueOffset + 100);
    }
}
