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
    public DateTime date = DateTime.Now;
    public string sDate;
    public int expenses { get; set; }
    public int balance { get; set; }
    public int climate { get; set; }
    public bool isPaused = false;
    Coroutine timer;
    public float timeUnit = 2f;
    // Start is called before the first frame update
    void Start()
    {
        balance = 10000;
        expenses = 200;
        BalanceText = GameObject.Find("FieldBalance").GetComponent(typeof(Text)) as Text;
        CurrentDate = GameObject.Find("FieldDate").GetComponent(typeof(Text)) as Text;
        ExpensesText = GameObject.Find("FieldMonthExpenses").GetComponent(typeof(Text)) as Text;
        
        //  var balanceText = GameObject.Find("FieldBalance");
        //   balanceText.text = "123";
        timer = StartCoroutine(Timer());
        
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKeyDown(KeyCode.Space))
        {
            if (isPaused)
            {
                timer = ResumeTime();
                isPaused = false;
            }
            else
            {
                StopTime(timer);
                isPaused = true;
            }
            
        }

       
        
        BalanceText.text = balance.ToString();
        ExpensesText.text = expenses.ToString();
    }

    IEnumerator Timer(float countTime = 1f)
    {
        countTime = timeUnit;
        while(true)
        {
            yield return new WaitForSeconds(countTime);
            date = date.AddMonths(1);
            CurrentDate.text = date.ToString();
            balance -= expenses;
            Debug.Log("This one is from coroutine");
            Debug.Log(date.ToString());
        }
    }





    public void StopTime(Coroutine t)
    {
        StopCoroutine(t);
    }

    public Coroutine ResumeTime()
    {
       return StartCoroutine(Timer());
    }
}
