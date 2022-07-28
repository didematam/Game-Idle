using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AllStations : MonoBehaviour
{
    public List<Station> Stations;

    void Start()
    {
        Stations= new List<Station>();
    }

    
    void Update()
    {
        
    }

    internal int GetLevel()
    {
     var x=  Stations.Where(x => x.gameObject.activeInHierarchy).FirstOrDefault();
        return x.currLevel;
    }
}
