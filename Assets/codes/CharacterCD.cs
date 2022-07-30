using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using System;


public class CharacterCD : MonoBehaviour
{

    public List<GameObject> carryCD;
    public List<GameObject> carryHamburger;
    public List<GameObject> carryCola;
    [SerializeField] public GameObject sObject;
    [SerializeField] public GameObject sLocation;
    
    [SerializeField] public GameObject hamburgerObject;
    [SerializeField] public GameObject hamburgerLocation;
    [SerializeField] public GameObject colaObject;
    [SerializeField] public GameObject colaLocation;
    [SerializeField] public GameObject Upgrade;
    [SerializeField] public float timemax = 0.1f;
    [SerializeField] public float elapsed = 0f;
    [SerializeField] public float moneyElapsed = 0f;
    [SerializeField] public float moneytimemax = 1f;
    [SerializeField] public int CDlimit = 5;
    [SerializeField] public int foodlimit = 1;
    [SerializeField] public float spacing = 0.5f;

    public int capasitymoney = 100;
    public int speedmoney = 100;
    public int WorkerSpeedMoney = 100;
    public int WorkerCapacityMoney = 100;
    public double currentMoney;
    public TextMeshProUGUI currentMoneyText;
    public TextMeshProUGUI currentMoneyCapacity;
    bool isActive;
    void Start()
    {
        carryCD = new List<GameObject>();
        carryHamburger = new List<GameObject>();
        carryCola = new List<GameObject>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "CDLocation")
        {
            elapsed += Time.deltaTime;
            if (elapsed >= timemax)
            {
                var createdCD = other.GetComponent<CreatCD>();

                if (carryCD.Count != CDlimit)
                {
                    createdCD.RemoveCD();
                    AddCarryCD();
                }
                elapsed = 0;
            }


        }



        if (other.gameObject.tag == "PutCDLocation")
        {
            elapsed += Time.deltaTime;
            if (elapsed >= timemax)
            {

                var putedCD = other.GetComponent<PutCD>();

                if (putedCD.putCD.Count != CDlimit && carryCD.Count > 0)
                {

                    putedCD.AddPutCD();
                    RemoveCarryCD();
                }

                elapsed = 0;
            }

        }
        if (other.gameObject.tag == "PutFoodLocation")
        {
            elapsed += Time.deltaTime;
            if (elapsed >= timemax)
            {

                var putFood = other.GetComponent<PutFood>();

                if (putFood.putCola.Count != foodlimit && carryCola.Count > 0 ||  putFood.putHamburger.Count != foodlimit && carryHamburger.Count>0) 
                {

                   
                    if(carryCola.Count>0)
                    {
                        putFood.AddPutCola();
                        RemoveCarryCola();
                    }
                    else if(carryHamburger.Count>0)
                    {
                        putFood.AddPutHamburger();
                        RemoveCarryHamburger();
                    }
                   
                }

                elapsed = 0;
            }

        }
        if (other.gameObject.tag == "Money")
        {
            elapsed += Time.deltaTime;
            if (elapsed >= timemax)
            {

                var moneyList = other.GetComponent<Money>();

                if (moneyList.moneyCount > 0)
                {
                    var outMoney = moneyList.RemoveMoney();

                    currentMoney += outMoney;
                    currentMoneyText.text = currentMoney.ToString();



                    elapsed = 0;
                }


            }
        }
        if (other.gameObject.tag == "NewStation")
        {
            moneyElapsed += Time.deltaTime;
            if (moneyElapsed >= moneytimemax)
            {


                if (currentMoney >= 0)
            {

                other.GetComponent<NewStationLoading>().çapýr();

                currentMoneyText.text = currentMoney.ToString();

                    moneyElapsed = 0;

                }

            }

        }

        if (other.gameObject.tag == "OpenCanteen")
        {
                moneyElapsed += Time.deltaTime;
                if (moneyElapsed >= moneytimemax)
                {


                    if (currentMoney >= 0)
                {
                
                    other.GetComponent<canteen>().openCanteen();

                    currentMoneyText.text = currentMoney.ToString();

                        moneyElapsed = 0;

                    }

                }

        }

        if (other.gameObject.tag == "hamburger")
        {
            elapsed += Time.deltaTime;
            if (elapsed >= timemax)
            {

                

                if (carryHamburger.Count != foodlimit)
                {

                     AddCarryHamburger();
                }

                elapsed = 0;               
                    

                }
            
        }
        if (other.gameObject.tag == "cola")
        {
            elapsed += Time.deltaTime;
            if (elapsed >= timemax)
            {

                

                if (carryCola.Count != foodlimit)
                {

                    AddCarryCola();
                }

                elapsed = 0;


            }

        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Upgrade")
        {
            if (!isActive)
            {
                Upgrade.SetActive(true);

            }



        }
    }


    public void AddCarryCD()
    {

        var carrycd = Instantiate(sObject, sLocation.gameObject.transform);
        carrycd.transform.position += transform.up * spacing * carryCD.Count ;
        //carrycd.transform.position += transform.right * spacing * (carryCD.Count % 3);
        //  carrycd.transform.position += transform.up * spacing * (carryCD.Count /3);
        carryCD.Add(carrycd);
    }
    public void RemoveCarryCD()
    {
        if (carryCD.Count > 0)
        {
            var removedCD = carryCD.Last();
            carryCD.Remove(removedCD);
            Destroy(removedCD);
        }

    }
    public void AddCarryHamburger()
    {
        var carryHamburgers = Instantiate(hamburgerObject, hamburgerLocation.gameObject.transform);
        carryHamburger.Add(carryHamburgers);
    }
    public void RemoveCarryHamburger()
    {
        if (carryHamburger.Count > 0)
        {
            var removedHamburger = carryHamburger.Last();
            carryHamburger.Remove(removedHamburger);
            Destroy(removedHamburger);
        }

    }
    public void AddCarryCola()
    {
        var carryColas = Instantiate(colaObject,colaLocation.gameObject.transform);
        carryCola.Add(carryColas);
    }
    public void RemoveCarryCola()
    {
        if (carryCola.Count > 0)
        {
            var removedCola = carryCola.Last();
            carryCola.Remove(removedCola);
            Destroy(removedCola);
        }

    }
    public void upgradeCDLimit()
    {
        if (currentMoney >= capasitymoney)
        {
            currentMoney -= capasitymoney;
            capasitymoney *= 2;
            var x = CDlimit + 3;
            CDlimit = x;
           
        }
        currentMoneyCapacity.text = capasitymoney.ToString();

    }
    void Update()
    {

    }
}
