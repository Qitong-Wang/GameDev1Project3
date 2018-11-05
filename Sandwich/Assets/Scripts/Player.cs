using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 6.0f;
    private CharacterController _characterController;
    public float gravity = -9.8f;
    public GameObject raycastObject;
    public Raycast raycast;
    public GameObject StuffText;
    Text stuffText;
    // Use this for initialization
    void Start()
    {
        raycast = raycastObject.GetComponent<Raycast>();
        _characterController = GetComponent<CharacterController>();
        stuffText = StuffText.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movement

        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed); //Limits the maximum speed

        movement.y = gravity;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _characterController.Move(movement);

        //Raycast
        if (raycast.objectTouch != null)
        {
            if (raycast.objectTouch.tag == "Stuff") //The raycast touch a stuff
            {
                Stuff s = raycast.objectTouch.GetComponent<Stuff>();
                if (raycast.objectTouch.name == "Kitchen")
                {
                    Kitchen k = raycast.objectTouch.GetComponent<Kitchen>();
                    if (k.clean == false)//Is cleaning
                    {
                        stuffText.text = string.Format("{0}\nWorking Sandwiches:  {1}/{2}\nWorking Process:       {3}/{4}"
                                                                , raycast.objectTouch.name, k.workingSandwiches, k.maximumSandwiches, k.currentTime, k.maximumTime);

                    }
                    else //Is producing new sandwiches
                    {
                        stuffText.text = string.Format("{0}\nWorking Sandwiches:  {1}/{2}\nWorking Process:       {3}/{4}"
                                                               , raycast.objectTouch.name, k.workingSandwiches, k.maximumSandwiches, k.currentTime, k.workingTime);

                    }

                }
                else
                {

                    stuffText.text = string.Format("{0}\nWorking Sandwiches:  {1}/{2}\nWorking Process:       {3}/{4}"
                                        , raycast.objectTouch.name, s.workingSandwiches, s.maximumSandwiches, s.currentTime, s.maximumTime);
                }

                //Click right button of Mouse. Set the goal of sandwiches
                if (Input.GetMouseButtonDown(0))
                {
                    s.OrderSandwich();
                }
                //Click left button of Mouse. Cancel a work.
                else if (Input.GetMouseButtonDown(1))
                {
                    s.CancelWork();

                }
            }
            else if (raycast.objectTouch.tag == "Sandwich")//The raycast touch a sandwich
            {
                Sandwich s = raycast.objectTouch.GetComponent<Sandwich>();
                stuffText.text = string.Format("An Idle Sandwich");
                //Click left button of Mouse. Select a sandwich
                if (Input.GetMouseButtonDown(0))
                {
                    s.SelectSandwich();

                }
                //Click right button of Mouse. Deselect a sandwich
                else if (Input.GetMouseButtonDown(1))
                {
                    s.DeselectSandwich();
                }

            }
            else
            {
                stuffText.text = "";
            }
        }
        else
        {
            stuffText.text = "";
        }

    }

}
