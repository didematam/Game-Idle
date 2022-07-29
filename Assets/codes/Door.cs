using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;
    bool isOpen;
    void Start()
    {
        
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            if(!isOpen)
            {
                animator.SetBool("open›dle", true);
               
                
            }
            


        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            animator.SetBool("›dleDoor", true);
             animator.SetBool("open›dle", false);

        }
    }

    void Update()
    {
    //    if (isOpen)
    //    {
            
    //        //animator.SetBool("closeDoor", true);
    //    }
    }
}
