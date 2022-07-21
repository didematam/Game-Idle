using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [SerializeField] public GameObject upgrade;
    public  void Start()
    {
        upgrade.SetActive(false);
    }

    public void exit()
    {
        this.exit();
    }


   public  void Update()
    {
        
    }
}
