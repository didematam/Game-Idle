using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Buttons : MonoBehaviour
{
    public GameObject Upgrade;

    public Station stations;
    public GameObject Workerspawners;
    public Button capacity;
    public Button speed;
    public Button workerspeed;
    public Button workerCapacity;


    void Start()
    {
        Upgrade.SetActive(false);

    }
    public void exit()
    {

        Upgrade.SetActive(false);
    }
    public void hireWorker()
    {

        var Workerspawner = Workerspawners.GetComponent<WorkerSpawner>();
        if (Workerspawner.WorkerSpawns.Count <= 5)
        {
            Workerspawner.addWorker();
        }



      


    }
    public void upgradeCpacity()
    {
     

        stations.character.upgradeCDLimit();


    }
    public void upgradeSpeed()
    {
        stations.charactermove.upgradeSpeed();
    }
    public void upgradeWorkerCapacity()
    {
        var Workerspawner = Workerspawners.GetComponent<WorkerSpawner>();
       foreach (var worker in Workerspawner.WorkerSpawns)
        {
            worker.upgradeCapacity();
        }
       
    }
    //public void OnEnable()
    //{
    //    if (stations.character.currentMoney >= stations.character.capasitymoney)
    //    {

    //        capacity.interactable = true;


    //    }
    //    if (stations.character.currentMoney < stations.character.capasitymoney)
    //    {
    //        capacity.interactable = false;

    //    }
    //    if (stations.character.currentMoney >= stations.character.speedmoney)
    //    {

    //        speed.interactable = true;


    //    }
    //    if (stations.character.currentMoney < stations.character.speedmoney)
    //    {
    //        speed.interactable = false;

    //    }
    //    if (stations.WorkerSpawner.WorkerSpawns.Count == 0)
    //    {
    //        workerspeed.interactable = false;
    //        workerCapacity.interactable = false;

    //    }
    //    if (stations.WorkerSpawner.WorkerSpawns.Count != 0)
    //    {
    //        workerspeed.interactable = true;
    //        workerCapacity.interactable = true;
    //    }
    //}
    void Update()
    {
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
        if (stations.WorkerSpawner.WorkerSpawns.Count == 0)
        {
            workerspeed.interactable = false;
            workerCapacity.interactable = false;

        }
        if (stations.WorkerSpawner.WorkerSpawns.Count != 0)
        {
            workerspeed.interactable = true;
            workerCapacity.interactable = true;
        }
    }
}
