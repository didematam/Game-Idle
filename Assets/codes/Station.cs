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
    public PutCD putcd;
    public PutFood putFood;
    public Money money;
    public CustomerSpawner customerSpawner;
    public Customers currentCustomer;
    public Worker currentWorker;
    public CharacterCD character;
    public NewStationLoading newStationLoading;
    public Buttons Buttons;
    public WorkerSpawner WorkerSpawner;
    public CharacterMove charactermove;
    public int Stationmoney = 100;
    public GameObject level1;
    public GameObject level2;
    public int currLevel;
    public TextMeshProUGUI upgradePctext;
    public int remananingUpgradePc;
    public TextMeshProUGUI remananingUpgradePcText;

    public Station Stations;



    public void upgradestation()
    {
        if (WorkerSpawner.character.currentMoney >= Stationmoney)
        {
            WorkerSpawner.character.currentMoney -= Stationmoney;
            Stationmoney *= 2;
          
            currLevel =2;
            setLevel();
            remananingUpgradePc = 3-1;
            remananingUpgradePcText.text = remananingUpgradePc.ToString();

        }

        upgradePctext.text = Stationmoney.ToString();
    }

    void Start()
    {
       
        currLevel = 1;
      
       

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
        
    }
}
