using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using System.Linq;

public class NewStationLoading : MonoBehaviour
{
    [SerializeField] public GameObject CharacterCD;
    [SerializeField] private Image uiFill; 
    public float totalMoney=100f;
    public float paidMoney;
    public GameObject openStation;
    public GameObject CloseCanvas;
  public AllStations allStations;
    public string ID;
    public string name;
    


    public TextMeshProUGUI moneyText;
 

    

    public void Awake()
    {
        
        moneyText.text = totalMoney.ToString();
      
        
    }
 
    private IEnumerator uptadeMoney(bool isload)
    {
        var x = CharacterCD.GetComponent<CharacterCD>();

        if (totalMoney >= 0)
        {
           
            if (x.currentMoney <= 0 && !isload)
            {
                yield break;
            }

            var a = totalMoney - paidMoney;

           

            if (a > 0)
            {
                if(!isload)
                {
                    x.currentMoney -= 1;
                    paidMoney += 1;
                }
                a = totalMoney - paidMoney;
                uiFill.fillAmount = Mathf.InverseLerp(0, totalMoney, paidMoney);
                moneyText.text = a.ToString();
            }

            
           
            if(paidMoney >= totalMoney)
            {
                saveData();
                StopCoroutine(uptadeMoney(false));
                Debug.Log("0 oldu");


               
                
                openStation.GetComponent<Station>().currLevel = allStations.GetLevel();
                openStation.SetActive(true);
                CloseCanvas.SetActive(false);




            }
          yield return null;
           
           
        }
       
        
    }

    public void çapýr(bool isload)
    {
        StartCoroutine(uptadeMoney(isload));
    }
    public void saveData()
    {
        
            PlayerPrefs.SetFloat(ID + name + "paidmoney ", paidMoney);
        
    }
    public void loadData()
    {
        paidMoney= PlayerPrefs.GetFloat(ID + name + "paidmoney ", paidMoney);
        çapýr(true);
    }

    void Start()
    {
        loadData();
    }

  
    void Update()
    {
        saveData();
    }
}
