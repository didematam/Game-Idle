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
    public Animator animator;
    [SerializeField] public GameObject sObject;
    public AudioSource cashSound;
    public AudioSource CDSound;
    public AudioSource repairSound;
    public AudioSource colaSound;
    public AudioSource hamburgerSound;
    [SerializeField] public GameObject sLocation;
    public CharacterMove charactermove;
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
    public Station brokeStation;
    public Station currentBrokenStation;
    public Gladness gladness;
    public Worker worker;
    public int capasitymoney = 100;
    public int speedmoney = 100;
    public int WorkerSpeedMoney = 100;
    public int WorkerCapacityMoney = 100;
    public double currentMoney;
    public TextMeshProUGUI currentMoneyText;
    public TextMeshProUGUI currentGladnessText;
    public TextMeshProUGUI currentMoneyCapacity;
    public string ID;
    public string Name;
    bool isActive;
    
    void Start()
    {
        carryCD = new List<GameObject>();
        carryHamburger = new List<GameObject>();
        carryCola = new List<GameObject>();
        loadData();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "CDLocation" )
        {
            elapsed += Time.deltaTime;
            if (elapsed >= timemax)
            {
                var createdCD = other.GetComponent<CreatCD>();

                if (carryCD.Count != CDlimit && carryCola.Count==0 && carryHamburger.Count == 0)
                {
                    createdCD.RemoveCD();
                    AddCarryCD();
                    CDSound.Play();
                }
                elapsed = 0;
            }


        }
        if (other.gameObject.tag == "Trash")
        {
            elapsed += Time.deltaTime;
            if (elapsed >= timemax)
            {

                if (carryCD.Count > 0)
                {
                    RemoveCarryCD();
                    CDSound.Play();
                }
                if (carryCola.Count > 0)
                {
                    RemoveCarryCola();
                    CDSound.Play();
                }
                if (carryHamburger.Count > 0)
                {
                    RemoveCarryHamburger();
                    CDSound.Play();
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

                if (putedCD.putCD.Count != putedCD.putLimit && carryCD.Count > 0)
                {

                    putedCD.AddPutCD();
                    CDSound.Play();
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

                   
                    if(carryCola.Count>0&& putFood.station.currentCustomer.x==1)
                    {
                        colaSound.Play();
                        putFood.AddPutCola();
                       
                        RemoveCarryCola();
                    }
                    else if(carryHamburger.Count>0 && putFood.station.currentCustomer.x == 0)
                    {
                        hamburgerSound.Play();
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

                if (moneyList.money.Count > 0)
                {
                    var outMoney = moneyList.RemoveMoney();

                    currentMoney += outMoney;
                    currentMoneyText.text = currentMoney.ToString();
                    cashSound.Play();



                    elapsed = 0;
                }


            }
        }
        if (other.gameObject.tag == "NewStation" && !animator.GetBool("running"))
        {
            moneyElapsed += Time.deltaTime;
            if (moneyElapsed >= moneytimemax)
            {


                if (currentMoney >= 0)
            {

                other.GetComponent<NewStationLoading>().�ap�r(false);

                currentMoneyText.text = currentMoney.ToString();

                    moneyElapsed = 0;

                }

            }

        }
        if (other.gameObject.tag == "2.SceneStations" && !animator.GetBool("running"))
        {
            moneyElapsed += Time.deltaTime;
            if (moneyElapsed >= moneytimemax)
            {


                if (currentMoney >= 0)
                {

                    other.GetComponent<OtherNewStations>().�ap�r(false);

                    currentMoneyText.text = currentMoney.ToString();

                    moneyElapsed = 0;

                }

            }

        }
        if (other.gameObject.tag == "NewScene" && !animator.GetBool("running"))
        {
            moneyElapsed += Time.deltaTime;
            if (moneyElapsed >= moneytimemax)
            {


                if (gladness.happyamount >= 0)
                {

                    other.GetComponent<NewScene>().�ap�r(false);

                    currentGladnessText.text = gladness.happyamount.ToString();

                    moneyElapsed = 0;

                }

            }

        }

        if (other.gameObject.tag == "OpenCanteen" && !animator.GetBool("running"))
        {
                moneyElapsed += Time.deltaTime;
                if (moneyElapsed >= moneytimemax)
                {


                    if (currentMoney >= 0)
                {
                
                    other.GetComponent<canteen>().openCanteen(false);

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

                

                if (carryHamburger.Count != foodlimit && carryCola.Count == 0 && carryCD.Count == 0)
                {
                    CDSound.Play();
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

                

                if (carryCola.Count != foodlimit && carryCD.Count == 0 && carryHamburger.Count == 0 )
                {
                    CDSound.Play();
                    AddCarryCola();
                }

                elapsed = 0;


            }

        }


    }
    private void OnTriggerExit(Collider other)
    {
        
        isActive= false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Upgrade" && !animator.GetBool("running"))
        {
            if (!isActive)
            {
                isActive = true;
                Upgrade.SetActive(true);

            }



        }
        if (other.gameObject.tag == "BrokeStation")
        {

            var currentBrokenStation = other.gameObject.GetComponentInParent<Station>();
            if (!isActive)
            {
                isActive = true;
                brokeStation = currentBrokenStation;
                animator.SetTrigger("repair 0");
                repairSound.Play();
                charactermove.canMove = false;
                sLocation.SetActive(false);


            }



        }
        if (other.gameObject.tag == "BreakWorker")
        {

            var currentBrokenStation = other.gameObject.GetComponentInParent<Station>();
            if (!isActive && currentBrokenStation.breakWorker!=null)
            {
                isActive = true;
                this.currentBrokenStation = currentBrokenStation;
                animator.SetTrigger("angry");
                
                charactermove.canMove = false;

            }



        }

    }
    public void angry()
    {
        charactermove.canMove = true;
        if (currentBrokenStation.joystick != null)
        {
            currentBrokenStation.joystick.transform.position = currentBrokenStation.oldJoystick.transform.position;

        }
        currentBrokenStation.breakWorker.animator.SetTrigger("StandUp");
        worker.brokeImage.SetActive(false);
        currentBrokenStation.breakWorker.breakElapsed = 0;
        currentBrokenStation.breakWorker.selectedStationBreak = null;
        currentBrokenStation.breakWorker.brokeImage.SetActive(false);
        currentBrokenStation.breakWorker.agent.SetDestination(currentBrokenStation.breakWorker.WorkerSpawner.waiting.position);
        currentBrokenStation.breakWorker = null;      
        currentBrokenStation = null;


    }
 public void repair()
    {
        brokeStation.Repair();
        charactermove.canMove = true;
        brokeStation = null;
        sLocation.SetActive(true);
    }

    public void AddCarryCD()
    {

        var carrycd = Instantiate(sObject, sLocation.gameObject.transform);
        carrycd.transform.position += transform.up * spacing * carryCD.Count ;
        
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
        saveData();
        currentMoneyText.text = currentMoney.ToString();
        if(brokeStation != null)
        {
            transform.position = brokeStation.PCReapairPos.position;
            gameObject.transform.LookAt(brokeStation.PCPos.position);
        }
        if (currentBrokenStation != null)
        {
            currentBrokenStation.isBroke = false;
            currentBrokenStation.smokeBreak.SetActive(false);
            transform.position = currentBrokenStation.AngryWorker.position;
            gameObject.transform.LookAt(currentBrokenStation.AngryWorkerLook.position);
           
}

    }
    public void saveData()
    {
        PlayerPrefs.SetInt(ID + Name + "carryCD", carryCD.Count);
        PlayerPrefs.SetInt(ID + Name + "carryCola", carryCola.Count);
        PlayerPrefs.SetInt(ID + Name + "carryHamburger", carryHamburger.Count);// add ve remove ks��mlar�nda daha mantkl� olabilir.
        PlayerPrefs.SetFloat(ID + Name + "currentMoney",((float)currentMoney));
    }
    public void loadData()
    {
        var x = PlayerPrefs.GetInt(ID + Name + "carryCD", carryCD.Count);
      for(int i = 0; i < x; i++)
        {
            AddCarryCD();
        }
        var y= PlayerPrefs.GetInt(ID + Name + "carryCola", carryCola.Count);
        for (int i = 0; i < y; i++)
        {
            AddCarryCola();
        }
        var z = PlayerPrefs.GetInt(ID + Name + "carryHamburger", carryHamburger.Count);
        for (int i = 0; i < z; i++)
        {
            AddCarryHamburger();
        }
        currentMoney=PlayerPrefs.GetFloat(ID + Name + "currentMoney", ((float)currentMoney));
        currentMoneyText.text = currentMoney.ToString();
    }
}
