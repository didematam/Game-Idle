using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using TMPro;

public class Worker : MonoBehaviour
{
    public WorkerSpawner WorkerSpawner;
    public Animator animator;
    public Station currentStation;

    public Transform collectCD;


    public NavMeshAgent agent;
    public List<GameObject> carryCD;
    public List<GameObject> carryHamburger;
    public List<GameObject> carryCola;
    public canteen canteen;

    [SerializeField] public GameObject sObject;
    [SerializeField] public GameObject sLocation;
    [SerializeField] public GameObject hamburgerObject;
    [SerializeField] public GameObject hamburgerLocation;
    [SerializeField] public GameObject colaObject;
    [SerializeField] public GameObject colaLocation;
    [SerializeField] public int foodlimit = 1;
    [SerializeField] public float timemax = 0.1f;
    [SerializeField] public float elapsed = 0f;
    [SerializeField] public int carrylimit = 5;
    [SerializeField] public float speed = 0.1f;
    




    void Start()
    {
        carryHamburger = new List<GameObject>();
        carryCola = new List<GameObject>();
        carryCD = new List<GameObject>();
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



        if (other.gameObject.tag == "PutCDLocation")
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
        if (other.gameObject.tag == "PutFoodLocation")
        {
            elapsed += Time.deltaTime;
            if (elapsed >= timemax)
            {

                var putFood = other.GetComponent<PutFood>();

                if (putFood.putCola.Count != foodlimit && carryCola.Count > 0 || putFood.putHamburger.Count != foodlimit && carryHamburger.Count > 0)
                {


                    if (carryCola.Count > 0)
                    {
                        putFood.AddPutCola();
                        RemoveCarryCola();
                    }
                    else if (carryHamburger.Count > 0)
                    {
                        putFood.AddPutHamburger();
                        RemoveCarryHamburger();
                    }

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
        var carryColas = Instantiate(colaObject, colaLocation.gameObject.transform);
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

    }
    public void desicion()
    {
        //if(WorkerSpawner.canteen.opencanteen.activeInHierarchy)
        //{
        //    var selectFood = GetComponent<Customers>().x;
        //    if (selectFood == 1 && carryCola.Count <= carrylimit)
        //    {
        //        agent.destination = colaLocation.transform.position;
        //    }
        //    if (selectFood == 2 && carryHamburger.Count <= carrylimit)
        //    {
        //        agent.destination = hamburgerLocation.transform.position;
        //    }
        //}
        //else
        //{

      

       

        if (carryCD.Count == 0)
        {
            agent.destination = collectCD.position;
        }
        else if (carryCD.Count == carrylimit)
        {
            var selectedStation = WorkerSpawner.Stations.Where(x => x.gameObject.activeInHierarchy && x.putcd.putCD.Count < carrylimit).OrderByDescending(x => x.putcd.putCD.Count).FirstOrDefault();
            if (selectedStation != null)
            {
                agent.destination = selectedStation.putcd.WorkerPutCD.transform.position;
                

            }
            else
            {
                agent.destination = WorkerSpawner.waiting.position;
            }
        }
        else
        {
            var selectedStation = WorkerSpawner.Stations.Where(x => x.gameObject.activeInHierarchy && x.putcd.putCD.Count < carrylimit).OrderByDescending(x => x.putcd.putCD.Count).FirstOrDefault();
            if (selectedStation != null)
            {
                agent.destination = selectedStation.putcd.WorkerPutCD.transform.position;

            }
            else
            {
                agent.destination = WorkerSpawner.waiting.position;
            }

        }

        }


    
    public void upgradeCapacity()
    {
        if (WorkerSpawner.character.currentMoney >= WorkerSpawner.character.WorkerCapacityMoney)
        {
            WorkerSpawner.character.currentMoney -= WorkerSpawner.character.WorkerCapacityMoney;
            WorkerSpawner. character.WorkerCapacityMoney *= 2;
            var x = carrylimit + 3;
            carrylimit = (int)x;
           
        }
       
        WorkerSpawner.CapacityText.text = WorkerSpawner.character.WorkerCapacityMoney.ToString();

    }
    //public void upgradeSpeed()
    //{
    //    if (WorkerSpawner.character.currentMoney >= WorkerSpawner.character.WorkerSpeedMoney)
    //    {
    //        WorkerSpawner.character.currentMoney -= WorkerSpawner.character.WorkerSpeedMoney;
    //        WorkerSpawner.character.WorkerSpeedMoney *= 2;
    //        var x = carrylimit + 3;
    //        carrylimit = (int)x;

    //    }

    //    WorkerSpawner.CapacityText.text = WorkerSpawner.character.WorkerSpeedMoney.ToString();
    //}
}
