using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewScene : MonoBehaviour
{
    [SerializeField] public GameObject Gladness;
    [SerializeField] private Image uiFill;
    public float totalGladness = 100f;
    public float paidGladness;
    public GameObject openScene;
    public GameObject CloseCanvas;
    public AllStations allStations;
    public string ID;
    public string name;



    public TextMeshProUGUI GladnessText;




    public void Awake()
    {

        GladnessText.text = totalGladness.ToString();


    }

    private IEnumerator uptadeGladness(bool isload)
    {
        var x = Gladness.GetComponent<Gladness>();

        if (totalGladness >= 0)
        {

            if (x.happyamount <= 0 && !isload)
            {
                yield break;
            }

            var a = totalGladness - paidGladness;



            if (a > 0)
            {
                if (!isload)
                {
                    x.happyamount -= 1;
                    paidGladness += 1;
                }
                a = totalGladness - paidGladness;
                uiFill.fillAmount = Mathf.InverseLerp(0, totalGladness, paidGladness);
                GladnessText.text = a.ToString();
            }



            if (paidGladness >= totalGladness)
            {
                saveData();
                StopCoroutine(uptadeGladness(false));
                Debug.Log("0 oldu");




                
                openScene.SetActive(true);
                CloseCanvas.SetActive(false);




            }
            yield return null;


        }


    }

    public void çapýr(bool isload)
    {
        StartCoroutine(uptadeGladness(isload));
    }
    public void saveData()
    {

        PlayerPrefs.SetFloat(ID + name + "paidGladness ", paidGladness);

    }
    public void loadData()
    {
        paidGladness = PlayerPrefs.GetFloat(ID + name + "paidGladness ", paidGladness);
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


