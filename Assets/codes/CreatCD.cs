using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreatCD : MonoBehaviour
{
    public List<GameObject> CD;
    [SerializeField]public GameObject spawnObject;
    [SerializeField] public GameObject spawnLocation;
    [SerializeField] public float timemax;
    [SerializeField] public float elapsed = 0f; 

    void Start()
    {
        CD = new List<GameObject>();

        elapsed = 0f;
    }

   
    public void AddCD()
    {
        var addedCD = Instantiate(spawnObject, spawnLocation.gameObject.transform);
        CD.Add(addedCD);
        
    }
    public void RemoveCD()
    {
        if(CD.Count > 0)
        {
            var removedCD = CD.Last();
            
                CD.Remove(removedCD);
            


            Destroy(removedCD);
        }
       

    }
    void Update()
    {
        elapsed += Time.deltaTime;
       if (elapsed >= timemax )
        {
           
            if (CD.Count!=5)
            {
                AddCD();
            }
            elapsed = 0;
            

        }
       
        

    }
}
