using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class WorkerSpawner : MonoBehaviour
{

    public List<Worker> WorkerSpawns;
    public List<Station> Stations;
    public CharacterCD character;
    public Station Station;
    public canteen canteen;
    public Transform collectCDLocation;
    public GameObject WorkerSpawn;
    public GameObject WorkerSpawnLocation;
    public Transform waiting;
    public TextMeshProUGUI remainingWorker;
    public int Worker;
    public TextMeshProUGUI CapacityText;
    public float speed = 0.1f;




    void Start()
    {
        WorkerSpawns = new List<Worker>();
    }

    public void addWorker()
        
    {
        
        if (Station.character.currentMoney >= 1000)
        {
           
            if (WorkerSpawns.Count <= 5)
            {
                var x = Instantiate(WorkerSpawn, WorkerSpawnLocation.transform) ;
                var worker = x.GetComponent<Worker>();
                worker.collectCD = collectCDLocation;
                worker.WorkerSpawner = this;
                if(WorkerSpawns.Count > 0)
                {
                    WorkerSpawns.First();
                    worker.carrylimit = WorkerSpawns.First().carrylimit;
                }
                WorkerSpawns.Add(worker);
                Station.character.currentMoney -= 1000;
                Worker = 5 - WorkerSpawns.Count();
                remainingWorker.text = Worker.ToString();



            }
        }
    }
    void Update()
    {
        
    }
}
