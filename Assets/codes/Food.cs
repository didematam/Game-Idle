using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Food : MonoBehaviour
{
    public List<GameObject> foodlist;
    
    public GameObject Cola;
    public GameObject Hamburger;
    void Start()
    {
        foodlist = new List<GameObject> { Cola, Hamburger };
       
        
      
    }
    public int GetRandomFood()
    {

        var x = foodlist.Count;
        var randomfood = Random.Range(0, x);
        Debug.Log(randomfood);
        return randomfood;
    }
    void Update()
    {
        
    }
}
