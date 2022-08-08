using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class PutCD : MonoBehaviour
{
    public Station station;
    public List<GameObject> putCD;
    public GameObject spawnObject;
    public GameObject spawnLocation;
    public int putLimit=5;
    public Transform WorkerPutCD;
    public float spacing;
    public string ID;
    public string Name;



    void Start()
    {
        loadData();
    }
        

    public void AddPutCD()
    {
        var addedPutCD = Instantiate(spawnObject, spawnLocation.gameObject.transform);
        addedPutCD.transform.position += transform.up * spacing * putCD.Count;
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
        saveData();
    }
    public void saveData()
    {
        PlayerPrefs.SetInt(ID + Name + "putCD", putCD.Count);
       
      
    }
    public void loadData()
    {
        var x = PlayerPrefs.GetInt(ID + Name + "putCD", putCD.Count);
        for (int i = 0; i < x; i++)
        {
            AddPutCD();
        }
       
    }
}
