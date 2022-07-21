using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PutCD : MonoBehaviour
{
    public Customers customer;
    public List<GameObject> putCD;
    public GameObject spawnObject;
    public GameObject spawnLocation;

    void Start()
    {
        putCD = new List<GameObject>();
    }
        

    public void AddPutCD()
    {
        var addedPutCD = Instantiate(spawnObject, spawnLocation.gameObject.transform);
        putCD.Add(addedPutCD);
    }
   public void RemovePutCD()
    {
        if(putCD.Count > 0)
        {
           
            var removedPutCD = putCD.Last();
            putCD.Remove(removedPutCD);
            Destroy(removedPutCD);
            
           
        }
       
    }
    void Update()
    {
       
    }
}
