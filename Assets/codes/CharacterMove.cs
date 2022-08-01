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
    public Rigidbody Rigidbody;
    public string ID;
    public string Name;
    public TextMeshProUGUI speedText;

    [SerializeField] Animator animator;


    void Start()
    {
        characcontrol=GetComponent<CharacterController>();
        Rigidbody=GetComponent<Rigidbody>();
        loadData();


    }

    public bool canMove = true;
    void Update()
    {
        if (canMove)
        {
            move();
          

        }
        animators();
        saveData();
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
                Rigidbody.isKinematic = false;
                animator.SetBool("running", true);

            }
            else
            {
                Rigidbody.isKinematic = true;
                animator.SetBool("running", false);
            }


            if (CharacterCD.carryCD.Count != 0 || CharacterCD.carryHamburger.Count != 0 || CharacterCD.carryCola.Count != 0)
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

    public void saveData()
    {
        PlayerPrefs.SetFloat(ID + name + " player x konumu", transform.position.x);
        PlayerPrefs.SetFloat(ID + name + " player y konumu", transform.position.y);
        PlayerPrefs.SetFloat(ID + name + " player z konumu", transform.position.z);
    }
    public void loadData()
    {
        var x = PlayerPrefs.GetFloat(ID + name + " player x konumu", transform.position.x);
        var y = PlayerPrefs.GetFloat(ID + name + " player y konumu", transform.position.y);
        var z = PlayerPrefs.GetFloat(ID + name + " player z konumu", transform.position.z);
        transform.position = new Vector3(x, y, z);
        
    }
}





