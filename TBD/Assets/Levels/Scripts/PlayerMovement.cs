using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public GameObject objectToSpawn;
    public GameObject checkpoint;
    public GameObject victoryScreen;
    public GameObject hints;

    public float runSpeed = 40f;

    bool jump = false;
    bool crouch = false;
    bool kill = false;

    float horizontalMove = 0f;
    float killDelay = 0f;

    private void Start()
    {
        victoryScreen.gameObject.SetActive(false);
        hints.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonDown("Kill"))
        {
            if (killDelay < 0)
            {
                kill = true;
                killDelay = 5;
            }
 
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;

        }
        if (Input.GetButtonDown("Reset"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
           
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

    }

    private void FixedUpdate()
    {
        // Move our Char
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
        killDelay -= Time.fixedDeltaTime;
        if (kill == true)
        {
            Instantiate(objectToSpawn, transform.position, transform.rotation);
            transform.position = new Vector3(-7.49f, -3.1f, 0f);
            kill = false;
        }

    }

   private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Checkpoint")
        {
            victoryScreen.gameObject.SetActive(true);
            hints.gameObject.SetActive(false);
        }

        
    }
}
