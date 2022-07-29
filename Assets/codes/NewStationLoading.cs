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
    public Station station;
    public float paidMoney;
    public GameObject openStation;
    public GameObject CloseCanvas;
  public AllStations allStations;



    public TextMeshProUGUI moneyText;
 

    

    public void Awake()
    {
        
        moneyText.text = totalMoney.ToString();
      
        
    }

    private IEnumerator uptadeMoney()
    {
        var x = CharacterCD.GetComponent<CharacterCD>();

        if (totalMoney >= 0)
        {
           
            if (x.currentMoney <= 0)
            {
                yield break;
            }

            var a = totalMoney - paidMoney;

           

            if (a > 0)
            {
                x.currentMoney -= 1;
                paidMoney += 1;
                a = totalMoney - paidMoney;
                uiFill.fillAmount = Mathf.InverseLerp(0, totalMoney, paidMoney);
                moneyText.text = a.ToString();
            }

            
           
            if(paidMoney >= totalMoney)
            {
                StopCoroutine(uptadeMoney());
                Debug.Log("0 oldu");


                //openStation.active = true;
                allStations.GetLevel();
                CloseCanvas.SetActive(false);




            }
          yield return null;
           
           
        }
       
        
    }

    public void çapýr()
    {
        StartCoroutine(uptadeMoney());
    }

    void Start()
    {
     
    }

  
    void Update()
    {
        
    }
}
