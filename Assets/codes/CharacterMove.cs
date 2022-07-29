using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CharacterMove : MonoBehaviour
{
    [SerializeField] VariableJoystick joystick;
    float speed = 10f;
    CharacterController characcontrol;
    public CharacterCD CharacterCD;
   
    public TextMeshProUGUI speedText;

    [SerializeField] Animator animator;


    void Start()
    {
        characcontrol=GetComponent<CharacterController>();

      
    }


    void Update()
    {
        move();
        animators();



    }
    void move()
    {
        var x = joystick.Horizontal; // joyistic kodu
        var y = joystick.Vertical;
        Vector3 move = new Vector3(x, 0, y);

        characcontrol.Move(move * speed * Time.deltaTime);
        if (move != Vector3.zero)  // dünfürme kodu
        {
            gameObject.transform.forward = move;
        }
    }

    void animators()
    {
        var x = joystick.Horizontal;
        var y = joystick.Vertical;

        if (x != 0 || y != 0)
        {
            animator.SetBool("running", true);

        }
        else
        {
            animator.SetBool("running", false);
        }
       

        if (CharacterCD.carryCD.Count != 0 || CharacterCD.carryHamburger.Count != 0 || CharacterCD.carryCola.Count!=0 )
        {


            animator.SetBool("carry", true);
        }
        else
        {
            animator.SetBool("carry", false);
        }

    }
    public void upgradeSpeed()
    {
        if (CharacterCD.currentMoney >= CharacterCD.speedmoney)
        {
            CharacterCD.currentMoney -= CharacterCD.speedmoney;
            CharacterCD.speedmoney *= 2;
            var x = speed + 1f;
            speed = x;
           
        }
        speedText.text = CharacterCD.speedmoney.ToString();

    }
}





