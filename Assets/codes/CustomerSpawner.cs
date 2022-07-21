using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public List<GameObject> CustomerSpawns;
    public List<Station> Stations;
    public GameObject CustomerSpawn;
    public GameObject CustomerSpawnLocation;
    void Start()
    {

        CustomerSpawns=new List<GameObject>();
       

    }


    public void AddCustomerSpawns()
    {
        var selectedStation = Stations.Where(x => x.currentCustomer == null && x.gameObject.activeInHierarchy).ToList();
        if(selectedStation.Count>0)
        {
            var x = Instantiate(CustomerSpawn, CustomerSpawnLocation.transform);
            CustomerSpawns.Add(x);
            x.GetComponent<Customers>().currentStation = selectedStation[0];
            x.GetComponent<Customers>().customerspawner = this;
            selectedStation[0].currentCustomer = x.GetComponent<Customers>();
            
        }
        
        
      
    }
    public void RemoveCustomerSpawns(GameObject removed)
    {
       
            CustomerSpawns.Remove(removed);
            Destroy(removed);
            
        
        
    }

    void Update()
    {
        AddCustomerSpawns();
    }
}
