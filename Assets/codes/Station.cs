using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class Station : MonoBehaviour
{
   public Gladness Gladness;
    public Transform movePositionTransform;
    public Transform exit;
   
    public Transform Monitor;
    public Transform PCReapairPos;
    public Transform PCPos;
    public Transform AngryWorker;
    public Transform AngryWorkerLook;
    public Transform newJoystick;
    public Transform oldJoystick;
    public GameObject joystick;
   
    public GameObject OpenScreen;
    public PutCD putcd;
    public PutFood putFood;
    public Money money;
    public CustomerSpawner customerSpawner;
    public Customers currentCustomer;
    public Worker currentWorker;
    public Worker breakWorker;
    public CharacterCD character;
    public NewStationLoading newStationLoading;
    public Buttons Buttons;
    public WorkerSpawner WorkerSpawner;
    public CharacterMove charactermove;
    public int Stationmoney = 100;
    public GameObject level1;
    public GameObject level2;
    public GameObject smokeBreak;
    public GameObject openPCScreen;
    public int currLevel;
    public TextMeshProUGUI upgradePctext;
    public int remananingUpgradePc;
    public TextMeshProUGUI remananingUpgradePcText;
    public float maxTime;
    public float currTime;

    public string CustomerAnimasion;
    public bool CanBroke =false;
   public bool isBroke;
    public bool gameMashine = false;
    public bool ps = false;


    public void BrokeTime()
    {
        if (currTime < maxTime && !isBroke)
        {
            currTime += Time.deltaTime;
        }
        else
        {
            smokeBreak.SetActive(true);
            isBroke = true;
            currTime = 0;

        }
       

    }
    public void Repair()
    {
        isBroke=false;
        smokeBreak.SetActive(false);
    }
   
    public void upgradestation()
    {
        currLevel = 2;
        setLevel();
    }

    void Start()
    {
       
        currLevel = 1;
        maxTime = Random.Range(100, 200);
        if(!gameMashine)
        {

       
        smokeBreak.SetActive(false);
        }
    }

    public void setLevel()
    {
        switch (currLevel)
        {
            case 1:
                level1.SetActive(true);
                level2.SetActive(false);
                break;
            case 2:
                level2.SetActive(true);
                level1.SetActive(false);
                break;

        }
    }
    
    
    void Update()
    {
        if (CanBroke)
        {
            BrokeTime();
        }
    }
}
