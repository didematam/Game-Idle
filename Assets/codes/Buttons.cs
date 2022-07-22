using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Buttons : MonoBehaviour
{
    public GameObject Upgrade;
    void Start()
    {
        Upgrade.SetActive(false);
    }
    public void exit()
    {

        Upgrade.SetActive(false);
    }
 
    void Update()
    {
        
    }
}
