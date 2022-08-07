using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Buttons : MonoBehaviour
{
    public GameObject Upgrade;
    public AudioSource buttonAudio;
    public Station stations;
    public AllStations allstation;
    public GameObject Workerspawners;
    public Button capacity;
    public Button speed;
    public Button workerspeed;
    public Button workerCapacity;
    public Button pc;

    public string ID;
    public string Name;

    public int hireWorkerCount=0;
    public int upgradeSpeedCount = 0;
    public int upgradeCapacityCount=0; 
    public int upgradeWorkerCapacityCount = 0;
    public int upgradeWorkerSpeedCount = 0;
    public int upgradepcCount = 0;
    public GameObject upgradePCLock;

    void Start()
    {
        Upgrade.SetActive(false);
        loadData();

    }
    public void exit()
    {

        Upgrade.SetActive(false);
        buttonAudio.Play();
    }
    public void hireWorker(bool isload)
    {
        buttonAudio.Play();
        var Workerspawner = Workerspawners.GetComponent<WorkerSpawner>();
        if (Workerspawner.WorkerSpawns.Count < 5)
        {
            if(Workerspawner.addWorker(isload))
                hireWorkerCount++;
        }
    }
    public void upgradeCpacity()
    {
        buttonAudio.Play();
        upgradeCapacityCount++;
        stations.character.upgradeCDLimit();
    }
    public void upgradeSpeed()
    {
        buttonAudio.Play();
        upgradeSpeedCount++;
        stations.charactermove.upgradeSpeed();
    }
    public void upgradeWorkerSpeed()
    {
        buttonAudio.Play();
        var Workerspawner = Workerspawners.GetComponent<WorkerSpawner>();
        foreach (var worker in Workerspawner.WorkerSpawns)
        {
            worker.upgradeSpeed();
        }
        stations.character.currentMoney -= stations.character.WorkerSpeedMoney;
        stations.character.WorkerSpeedMoney *= 2;
        upgradeWorkerSpeedCount++;
    }
    public void upgradeWorkerCapacity()
    {
        buttonAudio.Play();
        var Workerspawner = Workerspawners.GetComponent<WorkerSpawner>();
       foreach (var worker in Workerspawner.WorkerSpawns)
        {
            worker.upgradeCapacity();
        }
        stations.character.currentMoney -= stations.character.WorkerCapacityMoney;
        stations.character.WorkerCapacityMoney *= 2;
        upgradeWorkerCapacityCount++;

    }
    public void upgradepc()
    {
        buttonAudio.Play();
        if (stations.currLevel != 2)
        {
            upgradepcCount++;
            foreach(var station in allstation.Stations)
            {
                station.upgradestation();
            }
            upgradePCLock.SetActive(true);
        }
    }
   
    void Update()
    {
        saveData();

        if (stations.character.currentMoney >= stations.character.capasitymoney)
        {

            capacity.interactable = true;


        }
        if (stations.character.currentMoney < stations.character.capasitymoney)
        {
            capacity.interactable = false;

        }
        if (stations.character.currentMoney >= stations.character.speedmoney)
        {

            speed.interactable = true;


        }
        if (stations.character.currentMoney < stations.character.speedmoney)
        {
            speed.interactable = false;

        }
        if (stations.WorkerSpawner.WorkerSpawns.Count != 0 )
        {

            if(stations.character.currentMoney < stations.character.WorkerSpeedMoney)
                workerspeed.interactable = false;
            else
                workerspeed.interactable = true; 
            
            if (stations.character.currentMoney < stations.character.WorkerCapacityMoney)
                workerCapacity.interactable = false;
            else
                workerCapacity.interactable = true;
            
        }
        else
        {
            workerCapacity.interactable = false;
            workerspeed.interactable = false;
        }

        if (stations.level2.active==true)
        {
            pc.interactable=false;
        }
        if (stations.level2.active == false)
        {
            pc.interactable = true;
        }
    }
    
    public void saveData()
    {
        PlayerPrefs.SetInt(ID + Name + "hireWorkerCount ", hireWorkerCount);
        PlayerPrefs.SetInt(ID + Name + "upgradeSpeedCount ", upgradeSpeedCount);
        PlayerPrefs.SetInt(ID + Name + "upgradeCapacityCount ", upgradeCapacityCount);
        PlayerPrefs.SetInt(ID + Name + "upgradeWorkerCapacityCount ", upgradeWorkerCapacityCount);
        PlayerPrefs.SetInt(ID + Name + "upgradepcCount ", upgradepcCount);

    }
    public void loadData()
    {
        hireWorkerCount =  PlayerPrefs.GetInt(ID + Name + "hireWorkerCount ", hireWorkerCount);
        upgradeSpeedCount =  PlayerPrefs.GetInt(ID + Name + "upgradeSpeedCount ", upgradeSpeedCount);
        upgradeCapacityCount =  PlayerPrefs.GetInt(ID + Name + "upgradeCapacityCount ", upgradeCapacityCount);
        upgradeWorkerCapacityCount =  PlayerPrefs.GetInt(ID + Name + "upgradeWorkerCapacityCount ", upgradeWorkerCapacityCount);
        upgradepcCount =  PlayerPrefs.GetInt(ID + Name + "upgradepcCount ", upgradepcCount);

       
        for (int i = 0; i < upgradeSpeedCount; i++)
        {
            upgradeSpeed();
            upgradeSpeedCount--;
        }
        for (int i = 0; i < upgradeCapacityCount; i++)
        {
            upgradeCpacity();
            upgradeCapacityCount--;
        }
        for (int i = 0; i < upgradeWorkerCapacityCount; i++)
        {
            upgradeWorkerCapacity();
            upgradeWorkerCapacityCount--;
        }
        for (int i = 0; i < hireWorkerCount; i++)
        {
            hireWorker(true);
            hireWorkerCount--;
        }
        for (int i = 0; i < upgradepcCount; i++)
        {
            upgradepc();
            upgradepcCount--;
        }
    }




}
