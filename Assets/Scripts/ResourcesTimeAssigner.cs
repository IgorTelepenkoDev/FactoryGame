using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ResourcesTimeAssigner : MonoBehaviour
{
    public Text BalanceText;
    public Text CurrentDate;
    public DateTime date = DateTime.Now;
    public string sDate;
    // Start is called before the first frame update
    void Start()
    {
        //  var balanceText = GameObject.Find("FieldBalance");
        //   balanceText.text = "123";
        StartCoroutine(DoTimer());
    }

    // Update is called once per frame
    void Update()
    {
        BalanceText.text = "123";
    }

    IEnumerator DoTimer(float countTime = 1f)
    {
        int count = 0;
        while(true)
        {
            yield return new WaitForSeconds(countTime);
            date = date.AddMonths(1);
            CurrentDate.text = date.ToString();
            Debug.Log("This one is from coroutine");
            Debug.Log(date.ToString());
        }
    }
}
