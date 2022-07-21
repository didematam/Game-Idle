using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Customers : MonoBehaviour
{

    
    public Station currentStation;
    public  NavMeshAgent agent;
    public Animator animator;
    public CustomerSpawner customerspawner;
    public float maxtime ;
    public float currenttime;
    public float currentProgressBar;
    public float maxProgressBar;
    public float minProgressBar;
    [SerializeField] private Image uiFill;
    public float speed = 0.01f;
    public GameObject ProgressBar;
    public GameObject HappyFace;
    public GameObject AngryFace;
    bool isProgressBarStart;
    IEnumerator progress;
    

    bool isDestroy;

 
    private void Awake()
    {
        ProgressBar.SetActive(false);
        HappyFace.SetActive(false);
        AngryFace.SetActive(false);
        agent =GetComponent<NavMeshAgent>();
       


    }
    void Start()
    {
        
        agent.destination = currentStation.movePositionTransform.position;
        maxtime = Random.Range(5, 20);
        currenttime = maxtime;
       



    }
    private IEnumerator uptadetime()

    {

        while(currentStation.putcd.putCD.Count == 0)
        {
                             
            progress = progressBar();
            StartCoroutine(progress);

           
            yield return null;

        }       


        currentStation.putcd.RemovePutCD();
        

        while (currenttime > 0)
            {
           

            currenttime -= Time.deltaTime;



                if (currenttime <= 0)
                {
                currentStation.Gladness.addgladness(5);
                currentStation.money.AddMoney((int)maxtime * 5);
                currentStation.currentCustomer = null;
                HappyFace.SetActive(true);
                    animator.SetBool("sit", false);
                
                agent.destination = currentStation.exit.position;
                    isDestroy = true;


                }

                yield return null;

           
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Target" && Vector3.Distance(agent.destination, transform.position) < 2f)
        {
            gameObject.transform.position = currentStation.movePositionTransform.position;
            transform.LookAt(currentStation.Monitor);

         

            animator.SetBool("sit", true);
            
           StartCoroutine(uptadetime());
        }
    }

   private IEnumerator progressBar()
    {      


        if (!isProgressBarStart)
            {
                isProgressBarStart = true;
                ProgressBar.SetActive(true);

            
            float elapsed = 0;


                while (elapsed <= maxProgressBar)
                {
                    elapsed += Time.deltaTime;

                    uiFill.fillAmount = Mathf.InverseLerp(0, maxProgressBar, maxProgressBar - elapsed);
                    Debug.Log(maxProgressBar - elapsed);

                if (currentStation.putcd.putCD.Count > 0)
                {
                    StopCoroutine(progress);
                    ProgressBar.SetActive(false);
                    yield break;
                }

                if (maxProgressBar - elapsed <= 0.33)
                    {

                    currentStation.Gladness.RemoveGladness(5);
                    AngryFace.SetActive(true);
                    animator.SetBool("sit", false);
                    
                   
                    ProgressBar.SetActive(false);
                        agent.destination = currentStation.exit.position;
                        isDestroy = true;
                        yield break;
                    }

                    if (elapsed >= maxProgressBar)
                    {


                        yield break;

                    }



                    yield return null;



                }

                yield return null;
            }
           
        
    }
    
    //public void StopCoroutine()
    //{
    //    StopCoroutine(progressBar());
    //}
    void Update()
    {
       
        
        if (Vector3.Distance(agent.destination, transform.position) <0.1f)

        {
            animator.SetBool("walk", false);
            if(isDestroy)
            {
                customerspawner.RemoveCustomerSpawns(gameObject);
            }
           
            agent.isStopped = true;
        }
        else 
        {
            animator.SetBool("walk", true);
            
            agent.isStopped = false;
        }


        UIlook();


       

    }

    private void UIlook()
    {
        AngryFace.transform.LookAt(Camera.main.transform.position);
        HappyFace.transform.LookAt(Camera.main.transform.position);
        ProgressBar.transform.LookAt(Camera.main.transform.position);
    }
}
