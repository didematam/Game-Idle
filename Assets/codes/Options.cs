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
    public GameObject cashSound;
    public GameObject CDsound;
    public GameObject buttonSound;

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
            CDsound.SetActive(false);
            buttonSound.SetActive(false);
            cashSound.SetActive(false);
            isSoundClose = true;

        }

        else
        {

            OpenSoundImage.SetActive(true);
            CloseSoundImage.SetActive(false);
            CDsound.SetActive(true);
            buttonSound.SetActive(true);
            cashSound.SetActive(true);
            isSoundClose = false;
        }
    }
   
    void Update()
    {
        
    }
}
