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
    public GameObject CloseCanvas;

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


                opencanteen.active = true;

                CloseCanvas.SetActive(false);




            }
            yield return null;


        }


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
