using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using System;

public class CharacterCD : MonoBehaviour
{
    public Animator animator;
    public List<GameObject> carryCD;
    [SerializeField] public GameObject NewStationLoading;
    [SerializeField] public GameObject sObject;
    [SerializeField] public GameObject sLocation;
    [SerializeField] public GameObject spawnObject;
    [SerializeField] public GameObject spawnLocation;
    [SerializeField] public GameObject Upgrade;
    [SerializeField] public float timemax=0.1f;
    [SerializeField] public float elapsed = 0f;
    public double currentMoney;
    public TextMeshProUGUI currentMoneyText;
    bool isActive;
    void Start()
    {
        carryCD = new List<GameObject>();
        animator = GetComponent<Animator>();
    }
   
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "CDLocation")
        {
            elapsed += Time.deltaTime;
            if (elapsed >= timemax)
            {
                var createdCD = other.GetComponent<CreatCD>();

                if (carryCD.Count != 5)
                {
                    createdCD.RemoveCD();
                    AddCarryCD();
                }
                elapsed = 0;
            }


        }



        if (other.gameObject.tag=="PutCDLocation")
        {
            elapsed += Time.deltaTime;
            if (elapsed >= timemax)
            {

                var putedCD = other.GetComponent<PutCD>();

                if (putedCD.putCD.Count != 5 && carryCD.Count>0)
                {

                    putedCD.AddPutCD();
                    RemoveCarryCD();
                }

                elapsed = 0;
            }
            
        }
        if(other.gameObject.tag=="Money")
        {
            elapsed += Time.deltaTime;
            if (elapsed >= timemax)
            {
               
                var moneyList = other.GetComponent<Money>();
           
                if (moneyList.moneyCount>0)
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
            if (currentMoney >= 0)
            {
                
                other.GetComponent<NewStationLoading>().çapýr();
                
                currentMoneyText.text = currentMoney.ToString();



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
        carryCD.Add(carrycd);
    }
    public void RemoveCarryCD()
    {
        if(carryCD.Count>0)
        {
            var removedCD = carryCD.Last();
            carryCD.Remove(removedCD);
            Destroy(removedCD);
        }
       
    }
    void Update()
    {
        
    }
}
