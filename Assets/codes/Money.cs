using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

using System;

public class Money : MonoBehaviour
{
    Customers customer;
    public List<GameObject>money;
    public GameObject moneys;
    public GameObject moneyLocation;    
    public string ID;
    public string Name;



    public float spacing;

    void Start()
    {
     money = new List<GameObject>();
        loadData();
    }

    public void AddMoney(int moneyAmount)
    {
        
        for(int i = 0; i < moneyAmount; i+=5)
        {
            var x = Instantiate(moneys, moneyLocation.gameObject.transform);
            x.transform.position += transform.right * spacing*4 * (money.Count % 3);
            x.transform.position += transform.up * spacing * (money.Count / 3);
            money.Add(x);
        }
      

       

    }
    public double RemoveMoney()
    {
        if(money.Count > 0)
        {

            var removedMoney = money.Last();
            money.Remove(removedMoney);
            Destroy(removedMoney);
            return 5;

        }
        return 0;


    }

    public void saveData()
    {
        PlayerPrefs.SetInt(ID + Name + "money.Count", money.Count);
    }
    public void loadData()
    {
        var x = PlayerPrefs.GetInt(ID + Name + "money.Count", money.Count);
            for (int i = 0; i < x; i++)
        {
            AddMoney(5);
        }
    }
    void Update()
    {

        saveData();
    }
}
