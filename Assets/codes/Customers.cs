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
    public float foodtime;
    public float currenttime;
    public float currentProgressBar;
    public float maxProgressBar;
    public float minProgressBar;
    [SerializeField] private Image uiFill;
    [SerializeField] private Image uiFillFood;
    [SerializeField] private Image uiFillBreak;

    public float speed = 0.01f;
    public GameObject ProgressBar;
    public GameObject ProgressBarFoods;
    public GameObject ProgressBarBroke;
    public GameObject HappyFace;
    public GameObject AngryFace;
    public GameObject Cola;
    public GameObject Hamburger;
  
   
    public int a;
    public int x;
    bool isProgressBarStart;
    bool isProgressBarFoodStart;
    bool isProgressBarBreakStart;
    IEnumerator progress;
    IEnumerator progressFood;
    IEnumerator progressBroke;
    bool isDestroy;
    
  
    
   
    //bool isStartForFood 


    private void Awake()
    {
        ProgressBar.SetActive(false);
        ProgressBarBroke.SetActive(false);
        ProgressBarFoods.SetActive(false);
        HappyFace.SetActive(false);
        AngryFace.SetActive(false);
        Cola.SetActive(false);
        Hamburger.SetActive(false);
        agent =GetComponent<NavMeshAgent>();
       


    }
    void Start()
    {
        
        agent.destination = currentStation.movePositionTransform.position;
        maxtime = Random.Range(7, 20);
        currenttime = maxtime;
     




    }
    private IEnumerator uptadetime()

    {
        if(!currentStation.gameMashine)
        {

        
        while (currentStation.smokeBreak.activeInHierarchy)
        {
            progressBroke = progressBarBroke();
            StartCoroutine(progressBroke);
            yield return null;
        }

        
            


            while (currentStation.putcd.putCD.Count == 0)
        {

            progress = progressBar();
            StartCoroutine(progress);
          

            yield return null;

        }


        currentStation.putcd.RemovePutCD();

        }
       

        var randomProgressFoodTime = Random.Range(5, currenttime);
        bool isStartForFood = false;
        
        while (currenttime > 0)
        {

            
           




            if(currenttime<= randomProgressFoodTime && !isStartForFood && !currentStation.gameMashine)
            {
                isStartForFood=true;
                if (!customerspawner.canteen.opencanteen.activeInHierarchy)
                {
                    yield return null;
                }
                else
                {

                    a = Random.Range(1, 3);
                    if (a == 2 || a==3)
                    {

                        x = customerspawner.food.GetRandomFood();
                        if (x == 0)
                        {
                            Hamburger.SetActive(true);
                            Cola.SetActive(false);
                        }
                        else if (x == 1)
                        {
                            Hamburger.SetActive(false);
                            Cola.SetActive(true);

                        }
                        progressFood = progressBarFood();
                        StartCoroutine(progressFood);


                        while (ProgressBarFoods.activeInHierarchy == true)
                        {




                            yield return null;

                        }

                    }



                }
            }
            

            currenttime -= Time.deltaTime;
           
            if(!currentStation.gameMashine)
            {

           
            while (currentStation.smokeBreak.activeInHierarchy)
            {
                progressBroke = progressBarBroke();
                StartCoroutine(progressBroke);
                
                yield return null;
            }
           
            ProgressBarBroke.SetActive(false);
         


                if (currentStation.putFood.putHamburger.Count != 0 && a == 1)
                {
                    a = 0;
                    Hamburger.SetActive(false);
                    currentStation.Gladness.addgladness(3);
                    currentStation.money.AddMoney(50);
                }


                if (currentStation.putFood.putCola.Count != 0 && a == 1)
                {
                    a = 0;
                    Cola.SetActive(false);
                    currentStation.Gladness.addgladness(3);
                    currentStation.money.AddMoney(50);
                }


            }
           

            if (currenttime <= 0)
            {

                if(!currentStation.gameMashine)
                    {

                
                if (currentStation.putFood.putHamburger.Count != 0 )
                {
                    currentStation.putFood.RemovePutHamburger();
                    Hamburger.SetActive(false);
                    
                   
                }


                if (currentStation.putFood.putCola.Count != 0 )
                {
                    currentStation.putFood.RemovePutCola();
                    Cola.SetActive(false);
                    
                   
                }
                }
                if (currentStation.gameMashine)
                {
                    currentStation.Gladness.addgladness(5);


                    currentStation.money.AddMoney((int)maxtime * 7);
                }
                else if(currentStation.ps)
                {
                    currentStation.Gladness.addgladness(5);


                    currentStation.money.AddMoney((int)maxtime * 10);
                }
                else
                {
                    currentStation.Gladness.addgladness(5);


                    currentStation.money.AddMoney((int)maxtime * 5);
                }

                

                
                currentStation.currentCustomer = null;
                
             
                animator.SetBool(currentStation.CustomerAnimasion, false);
                animator.SetBool("happy", true);
                currentStation.openPCScreen.SetActive(false);
                currentStation.OpenScreen.SetActive(false);
                if (currentStation.joystick != null)
                {
                    currentStation.joystick.transform.position = currentStation.oldJoystick.transform.position;

                }

               
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

         if(currentStation.joystick != null)
            {
                currentStation. joystick.transform.position=currentStation.newJoystick.transform.position;
               


            }

            animator.SetBool(currentStation.CustomerAnimasion, true);
            currentStation.openPCScreen.SetActive(true);
            currentStation.OpenScreen.SetActive(true);

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

                if ( currentStation.putcd.putCD.Count > 0 )
                {
                    StopCoroutine(progress);
                    ProgressBar.SetActive(false);

                    yield break;
                }
               




                if (maxProgressBar - elapsed <= 0.33)
                    {
                    currentStation.Gladness.RemoveGladness(5);
                    AngryFace.SetActive(true);
                    animator.SetBool(currentStation.CustomerAnimasion, false);
                    animator.SetBool("sad", true);
                    if (currentStation.joystick != null)
                    {
                        currentStation.joystick.transform.position = currentStation.oldJoystick.transform.position;

                    }
                    currentStation.openPCScreen.SetActive(false);
                    currentStation.OpenScreen.SetActive(false);
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
    private IEnumerator progressBarBroke()
    {


        if (!isProgressBarBreakStart)
        {
            isProgressBarBreakStart = true;
            ProgressBarBroke.SetActive(true);
           

            float elapsed = 0;


            while (elapsed <= maxProgressBar)
            {
                elapsed += Time.deltaTime;

                uiFillBreak.fillAmount = Mathf.InverseLerp(0, maxProgressBar, maxProgressBar - elapsed);
                Debug.Log(maxProgressBar - elapsed);

               if(!currentStation.smokeBreak.activeInHierarchy)
                {
                    StopCoroutine(progressBroke);
                    ProgressBarBroke.SetActive(false);
                    isProgressBarBreakStart = false;
                    yield break;
                }


                if (maxProgressBar - elapsed <= 0.33)
                {

                    if (currentStation.putFood.putHamburger.Count != 0 && a == 1)
                    {
                        currentStation.putFood.RemovePutHamburger();
                        Hamburger.SetActive(false);
                        
                    }


                    if (currentStation.putFood.putCola.Count != 0 && a == 1)
                    {
                        currentStation.putFood.RemovePutCola();
                        Cola.SetActive(false);
                        
                    }
                    currentStation.Gladness.RemoveGladness(5);
                    AngryFace.SetActive(true);
                    animator.SetBool(currentStation.CustomerAnimasion, false);
                    animator.SetBool("sad", true);
                    currentStation.openPCScreen.SetActive(false);
                    currentStation.OpenScreen.SetActive(false);
                    if (currentStation.joystick != null)
                    {
                        currentStation.joystick.transform.position = currentStation.oldJoystick.transform.position;

                    }
                    ProgressBarBroke.SetActive(false);
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
    private IEnumerator progressBarFood()
    {
        if (!isProgressBarFoodStart)
        {
            isProgressBarFoodStart = true;
            ProgressBarFoods.SetActive(true);


            float elapsed = 0;


            while (elapsed <= maxProgressBar)
            {
                elapsed += Time.deltaTime;

                uiFillFood.fillAmount = Mathf.InverseLerp(0, maxProgressBar, maxProgressBar - elapsed);
                Debug.Log(maxProgressBar - elapsed);

                
                if (currentStation.putFood.putHamburger.Count > 0 || currentStation.putFood.putCola.Count > 0)

                {
                    ProgressBarFoods.SetActive(false);
                    StopCoroutine(progressFood);
                    

                    yield break;
                }


                if (maxProgressBar - elapsed <= 0.33)
                {

                    Cola.SetActive(false);
                    Hamburger.SetActive(false);

                    AngryFace.SetActive(true);

                    ProgressBarFoods.SetActive(false);
                    currentStation.Gladness.RemoveGladness(3);

                    StopCoroutine(progressFood);
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
        ProgressBarFoods.transform.LookAt(Camera.main.transform.position);
        ProgressBarBroke.transform.LookAt(Camera.main.transform.position);
    }
    public void �a��r()
    {
        StartCoroutine(progressBarFood());
    }
}
