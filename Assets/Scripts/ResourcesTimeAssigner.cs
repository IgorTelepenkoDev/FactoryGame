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
    public DateTime date = DateTime.Now;
    public string sDate;
    public int expenses;
    public bool isPaused = false;
    Coroutine timer;
    public float timeUnit = 2f;
    // Start is called before the first frame update
    void Start()
    {
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

       
        
        BalanceText.text = "123";
    }

    IEnumerator Timer(float countTime = 1f)
    {
        countTime = timeUnit;
        while(true)
        {
            yield return new WaitForSeconds(countTime);
            date = date.AddMonths(1);
            CurrentDate.text = date.ToString();
            Debug.Log("This one is from coroutine");
            Debug.Log(date.ToString());
        }
    }

    public int GetBalance()
    {
        return Convert.ToInt32(BalanceText.text);
    }

    public void SetBalance(int amount)
    {
        BalanceText.text = amount.ToString();
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
