using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public AudioSource ButtonSource;
  public Animator animator;
    public GameObject OpenSoundImage;
    public GameObject CloseSoundImage;
    public GameObject sounds;
   
    bool isOpen;
    bool isSoundClose;
    void Start()
    {
        
    }

   public void openCloseOptions()
    {
        ButtonSource.Play();
        if (!isOpen)
        {
            
           
            animator.SetBool("open 0", true);
            isOpen = true;
            
        }
       
        else
        {

            animator.SetBool("open 0", false);
            isOpen = false;
        }


    }
    public void OpenCloseSound()
    {
        ButtonSource.Play();
        if (!isSoundClose)
        {
            OpenSoundImage.SetActive(false);
            CloseSoundImage.SetActive(true);
            sounds.SetActive(false);
            
            isSoundClose = true;

        }

        else
        {

            OpenSoundImage.SetActive(true);
            CloseSoundImage.SetActive(false);

            sounds.SetActive(true);
            isSoundClose = false;
        }
    }
   
    void Update()
    {
        
    }
}
