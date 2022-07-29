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
   

    void Start()
    {
        
        moneyText.text = totalMoney.ToString();
    }

    private IEnumerator opeenCanteen()
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



            if (paidMoney >= totalMoney)
            {
                StopCoroutine(opeenCanteen());
                Debug.Log("0 oldu");


                opencanteen.active = true;

                CloseCanvas.SetActive(false);




            }
            yield return null;


        }
       

    }
    public void openCanteen()
    {
        StartCoroutine(opeenCanteen());
        }
    
   
    void Update()
    {
        
    }
}
