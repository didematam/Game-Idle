using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class Gladness : MonoBehaviour
{
    public Station currentStation;

    public Customers customers;
    public int happy=0;
    public int sad = 0;
    public int happyamount;
    public TMP_Text text;

    public string ID;
    public string Name;


    void Start()
    {
        loadData();
    }
    
   public void addgladness(int countGladness)
    {

        happyamount += countGladness ;
       
    }
    public void RemoveGladness(int countGladness)
    {
        happyamount -= countGladness;
    }
    public void saveData()
    {

        PlayerPrefs.SetInt(ID + Name + "happyamount ", happyamount);

    }
    public void loadData()
    {
        happyamount = PlayerPrefs.GetInt(ID + Name + "happyamount ", happyamount);
    }

    void Update()
    {
        saveData();
        text.text = happyamount.ToString();
    }
}
