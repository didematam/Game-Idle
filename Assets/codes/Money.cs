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
    public int moneyCount;
    public Transform Location;
    public Transform Moneys;
    public Transform startpos;

    public int numberofrows;
    public int numberofcolumns;
    public float spacing;

    void Start()
    {
        List<Money> moneys = new List<Money>();
       
    }

    public void AddMoney(int moneyAmount)
    {
        
        moneyCount += moneyAmount;
        for (int row = 0; row < numberofrows; row++)
        {
            for (int col = 0; col < numberofcolumns; col++)
            {
                Vector3 start = new Vector3(startpos.position.x + col * spacing,  startpos.position.y, startpos.position.z - row * spacing);
                Transform x = Instantiate(Moneys, start, Quaternion.identity);
                x.SetParent(Location);
                money.Add(x.gameObject);

            }
        }
        //var x = Instantiate(moneys, moneyLocation.gameObject.transform);
       

    }
    public double RemoveMoney()
    {
       var outMoney= Math.Round((double)(moneyCount / money.Count), MidpointRounding.AwayFromZero);
        moneyCount -= ((int)outMoney);
        if(money.Count > 0)
        {

            var removedMoney = money.Last();
            money.Remove(removedMoney);
            Destroy(removedMoney);
           
        }

        return outMoney;

    }
    void spawnmone()
    {
       
    }
    
    void Update()
    {
       
    }
}
