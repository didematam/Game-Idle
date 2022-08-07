using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class PutFood : MonoBehaviour
{
    public Station station;
    public List<GameObject> putHamburger;
    public List<GameObject> putCola;
    public GameObject HamburgerObject;
    public GameObject HamburgerLocation;
    public GameObject ColaObject;
    public GameObject ColaLocation;
    void Start()
    {
        putHamburger = new List<GameObject>();
        putCola = new List<GameObject>();
    }

    public void AddPutHamburger()
    {
        var addedPutHamburger = Instantiate(HamburgerObject, HamburgerLocation.gameObject.transform);
        putHamburger.Add(addedPutHamburger);
    }
    public void RemovePutHamburger()
    {
        if (putHamburger.Count > 0)
        {

            var removedPutHamburger = putHamburger.Last();
            putHamburger.Remove(removedPutHamburger);
            Destroy(removedPutHamburger);


        }

    }
    public void AddPutCola()
    {
        var addedPutCola = Instantiate(ColaObject, ColaLocation.gameObject.transform);
        putCola.Add(addedPutCola);
    }
    public void RemovePutCola()
    {
        if (putCola.Count > 0)
        {

            var removedPutCola = putCola.Last();
            putCola.Remove(removedPutCola);
            Destroy(removedPutCola);


        }

    }
    void Update()
    {
        
    }

}
