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
    public GameObject DataManager;
    DataManager dataManager;
    public GameObject StuffText;
    Text stuffText;
    // Use this for initialization
    void Start()
    {
        raycast = raycastObject.GetComponent<Raycast>();
        _characterController = GetComponent<CharacterController>();
        dataManager = DataManager.GetComponent<DataManager>();
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
            if (raycast.objectTouch.tag == "Stuff")
            {
                Stuff s = raycast.objectTouch.GetComponent<Stuff>();
                stuffText.text = string.Format("{0}\nWorking Sandwiches: {1}/{2}"
                    , raycast.objectTouch.name, s.workingSandwiches, s.maximumSandwiches);
                //Click left button of Mouse
                if (Input.GetMouseButtonDown(0))
                {
                    s.IncreaseSandwich();
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
