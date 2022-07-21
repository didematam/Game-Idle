using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class Gladness : MonoBehaviour
{
    public Station currentStation;

    public Customers customers;
    public List<int> gladdness;
    public int happy=0;
    public int sad = 0;
    public int happyamount;
    public TMP_Text text;
 

    void Start()
    {
        gladdness = new List<int>();
    }
    
   public void addgladness(int countGladness)
    {

        happyamount += countGladness ;
        gladdness.Add(happyamount);
       
       
    }
    public void RemoveGladness(int countGladness)
    {
        happyamount -= countGladness;

        if(happyamount >= 0)
        {
            gladdness.Remove(happyamount);
        }

          
        
        
      
    }
    void Update()
    {
        text.text = happyamount.ToString();
    }
}
