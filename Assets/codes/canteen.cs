using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class canteen : MonoBehaviour
{
    
    public TextMeshProUGUI moneyText;
    [SerializeField] public GameObject CharacterCD;
    public float totalMoney = 100f;
    public float paidMoney;
    [SerializeField] private Image uiFill;
    public GameObject opencanteen;
    public GameObject opencanteen2;
    public GameObject CloseCanvas;
    public Customers customer;
    
    public string ID;
    public string Name;

    void Start()
    {
        loadData();
    }

    private IEnumerator opeenCanteen(bool isload)
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



            if (paidMoney >= totalMoney)
            {
                PlayerPrefs.SetFloat(ID + Name + "paidmoney ", paidMoney);
                StopCoroutine(opeenCanteen(false));
                Debug.Log("0 oldu");


                opencanteen.SetActive(true);
                opencanteen2.SetActive(true);

                CloseCanvas.SetActive(false);
                FirstCustomer();



            }
            yield return null;


        }


    }
  
    public void FirstCustomer()
    {
        bool isFirstCustomer = true;
        if (isFirstCustomer && customer!=null)
        {
            var x =customer.customerspawner.food.GetRandomFood();
            if(x==0)
            {
                customer.Hamburger.SetActive(true);
                customer.Cola.SetActive(false);
            }
            else if (x == 1)
            {
                customer.Hamburger.SetActive(false);
                customer.Cola.SetActive(true);
                customer.çaðýr();




            }
        }
        isFirstCustomer = false;
    }
    
    public void openCanteen(bool isload)
    {
        StartCoroutine(opeenCanteen(isload));
    }
    public void saveData()
    {

        PlayerPrefs.SetFloat(ID + Name + "paidmoney ", paidMoney);

    }
    public void loadData()
    {
        paidMoney = PlayerPrefs.GetFloat(ID + Name + "paidmoney ", paidMoney);
        openCanteen(true);
    }

 

   
    void Update()
    {
        saveData();
    }
}
