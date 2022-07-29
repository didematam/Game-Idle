using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Customers : MonoBehaviour
{

    public canteen canteen;
    public Station currentStation;
    public  NavMeshAgent agent;
    public Animator animator;
    public CustomerSpawner customerspawner;
    public float maxtime ;
    public float foodtime;
    public float currenttime;
    public float currentProgressBar;
    public float maxProgressBar;
    public float minProgressBar;
    [SerializeField] private Image uiFill;
    public float speed = 0.01f;
    public GameObject ProgressBar;
    public GameObject HappyFace;
    public GameObject AngryFace;
    public GameObject Cola;
    public GameObject Hamburger;
    public int a;
    bool isProgressBarStart;
    IEnumerator progress;
    

    bool isDestroy;

 
    private void Awake()
    {
        ProgressBar.SetActive(false);
        HappyFace.SetActive(false);
        AngryFace.SetActive(false);
        Cola.SetActive(false);
        Hamburger.SetActive(false);
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

        while (currentStation.putcd.putCD.Count == 0)
        {

            progress = progressBar(false);
            StartCoroutine(progress);


            yield return null;

        }


        currentStation.putcd.RemovePutCD();



        if (!customerspawner.canteen.opencanteen.activeInHierarchy )
        {
            yield return null;
        }
        else
        {


             a = Random.Range(1,1);
            if (a == 1)
            {
                
                var  x = customerspawner.food.GetRandomFood();
                if (x == 0)
                {
                    Hamburger.SetActive(true);

                }
                else if (x == 1)
                {
                    Cola.SetActive(true);

                }
                StartCoroutine(progressBar(true));
                while (customerspawner.putfood.putHamburger.Count == 0 || customerspawner.putfood.putCola.Count==0)
                {
                    


                    yield return null;

                }
                
            }

           

        }


        while (currenttime > 0)
        {
           

            currenttime -= Time.deltaTime;


            if (customerspawner.putfood.putHamburger.Count != 0 && a==1)
            {
                customerspawner.putfood.RemovePutHamburger();
                Hamburger.SetActive(false);
                currentStation.Gladness.addgladness(3);
                currentStation.money.AddMoney(50);
            }
           

            if (customerspawner.putfood.putCola.Count != 0 && a == 1)
            {
                customerspawner.putfood.RemovePutCola();
                Cola.SetActive(false);
                currentStation.Gladness.addgladness(3);
                currentStation.money.AddMoney(50);
            }



           

            if (currenttime <= 0)
            {
                //if (Cola.activeInHierarchy || Hamburger.activeInHierarchy)
                //{

                //    currentStation.Gladness.RemoveGladness(3);
                    
                   
                //}
                //else
                //{
                //    HappyFace.SetActive(true);
                //}
               

                
               
                currentStation.Gladness.addgladness(5);

                currentStation.money.AddMoney((int)maxtime * 5);

                
                currentStation.currentCustomer = null;
                //Cola.SetActive(false);
                //Hamburger.SetActive(false);
             
                animator.SetBool("sit", false);
                HappyFace.SetActive(true);
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

   private IEnumerator progressBar(bool isFood)
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

                if (!isFood && currentStation.putcd.putCD.Count > 0 )
                {
                    StopCoroutine(progress);
                    ProgressBar.SetActive(false);
                    yield break;
                }
                if(isFood && ( customerspawner.putfood.putHamburger.Count > 0 || customerspawner.putfood.putCola.Count > 0))

                {
                    StopCoroutine(progress);
                    ProgressBar.SetActive(false);
                   
                    yield break;
                }
                

                if (maxProgressBar - elapsed <= 0.33)
                    {
                    if (!isFood )
                    {
                        currentStation.Gladness.RemoveGladness(5);
                        currentStation.money.AddMoney((int)maxtime * 5);
                    }
                    if (isFood )

                    {
                        currentStation.Gladness.RemoveGladness(3);
                    }
                    //currentStation.Gladness.RemoveGladness(5);
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
        Hamburger.transform.LookAt(Camera.main.transform.position);
        Cola.transform.LookAt(Camera.main.transform.position);
    }
}
