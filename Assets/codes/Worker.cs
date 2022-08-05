using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using TMPro;
using System;

public class Worker : MonoBehaviour
{
    public WorkerSpawner WorkerSpawner;
    public Animator animator;

    public Transform collectCD;
    public GameObject brokeImage;


    public NavMeshAgent agent;
    public List<GameObject> carryCD;
 
    public canteen canteen;
    public Station selectedStation;
    public Station selectedStationBreak;

    [SerializeField] public GameObject sObject;
    [SerializeField] public GameObject sLocation;
   

    [SerializeField] public float timemax = 0.1f;
    [SerializeField] public float elapsed = 0f;
    [SerializeField] public float breakTimeMax = 0.1f;
    [SerializeField] public float breakElapsed = 0f;
    [SerializeField] public int carrylimit = 5;
    [SerializeField] public float speed = 0.1f;
    bool onWay;




    void Start()
    {
       
        carryCD = new List<GameObject>();
        breakTimeMax = UnityEngine.Random.Range(10, 20);
        brokeImage.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "CDLocation")
        {
            elapsed += Time.deltaTime;
            if (elapsed >= timemax)
            {
                var createdCD = other.GetComponent<CreatCD>();

                if (carryCD.Count != carrylimit)
                {
                    createdCD.RemoveCD();
                    AddCarryCD();
                }
                elapsed = 0;
            }


        }



        if (other.gameObject.tag == "PutCDLocation" && selectedStation!=null&& selectedStation.putcd == other.gameObject.GetComponent<PutCD>())
        {
            elapsed += Time.deltaTime;
            if (elapsed >= timemax)
            {

                var putedCD = other.GetComponent<PutCD>();

                if (putedCD.putCD.Count != carrylimit && carryCD.Count > 0)
                {

                    putedCD.AddPutCD();
                    RemoveCarryCD();
                }

                elapsed = 0;
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
        if (carryCD.Count > 0)
        {
            var removedCD = carryCD.Last();
            carryCD.Remove(removedCD);
            Destroy(removedCD);
        }

    }
   


    void Update()
    {

        if (Vector3.Distance(agent.destination, transform.position) < 0.1f)

        {
            animator.SetBool("running", false);


            agent.isStopped = true;
        }
        else
        {
            animator.SetBool("running", true);

            agent.isStopped = false;
        }
        if (carryCD.Count != 0)
        {


            animator.SetBool("carry", true);
        }
        else
        {
            animator.SetBool("carry", false);
        }

        desicion();
        brokeImage.transform.LookAt(Camera.main.transform.position);
    }

 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Target" && Vector3.Distance(agent.destination, transform.position) < 2f)
        {
            if(selectedStationBreak != null)
            {
                transform.position = selectedStationBreak.movePositionTransform.position;
                transform.LookAt(selectedStationBreak.Monitor);

                selectedStationBreak.isBroke = false;
                selectedStationBreak.smokeBreak.SetActive(false);
                brokeImage.SetActive(true);
                animator.SetBool("sit", true);
                selectedStationBreak.openPCScreen.SetActive(true);
                selectedStationBreak.currentWorker = this;
            }
        }
    }

    public void desicion()
    {
        if(breakTimeMax <= breakElapsed)
        {
            if(selectedStation == null && selectedStationBreak==null  )
            {
              selectedStationBreak = WorkerSpawner.Stations.Where(x => x.gameObject.activeInHierarchy && x.currentCustomer==null && x.breakWorker == null).FirstOrDefault();
              
            }
            if (selectedStationBreak != null)
            {
                agent.destination = selectedStationBreak.movePositionTransform.position;
                selectedStationBreak.breakWorker = this;
                return;
            }


        }
        else
        {
            breakElapsed += Time.deltaTime;
        }

        if (!brokeImage.activeInHierarchy)
        {

            if (selectedStation == null)
            {
                selectedStation = WorkerSpawner.Stations.Where(x => x.gameObject.activeInHierarchy && x.putcd.putCD.Count == 0 && x.currentWorker == null).OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                return;
            }
            else if (selectedStation != null)
            {
                if (carryCD.Count < carrylimit && !onWay)
                {
                    agent.destination = collectCD.position;
                    return;
                }

                else if (carryCD.Count == 0 || selectedStation.putcd.putCD.Count == selectedStation.putcd.putLimit)
                {
                    agent.destination = WorkerSpawner.waiting.position;
                    onWay = false;
                    selectedStation.currentWorker = null;
                    selectedStation = null;
                    for (int i = 0; i < carryCD.Count; i++)
                    {
                        RemoveCarryCD();
                    }

                }
                else if (carryCD.Count == carrylimit || selectedStation.putcd.putCD.Count != selectedStation.putcd.putLimit)
                {
                    onWay = true;
                    agent.destination = selectedStation.putcd.WorkerPutCD.transform.position;
                    selectedStation.currentWorker = this;
                    return;
                }


            }

        }
        else
        {
            agent.SetDestination(transform.position);
            selectedStation = null;
            onWay = false;

        }

    }


    
    public void upgradeCapacity()
    {
        if (WorkerSpawner.character.currentMoney >= WorkerSpawner.character.WorkerCapacityMoney)
        {
          
            var x = carrylimit + 3;
            carrylimit = (int)x;
           
        }
       
        WorkerSpawner.CapacityText.text = WorkerSpawner.character.WorkerCapacityMoney.ToString();

    }
    public void upgradeSpeed()
    {
        if (WorkerSpawner.character.currentMoney >= WorkerSpawner.character.WorkerSpeedMoney)
        {
          
             agent.speed += 1;

        }

        WorkerSpawner.SpeedText.text = WorkerSpawner.character.WorkerSpeedMoney.ToString();
    }
}
