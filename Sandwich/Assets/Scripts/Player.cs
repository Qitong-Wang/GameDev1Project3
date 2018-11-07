using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 6.0f;
    private CharacterController _characterController;
    public float gravity = -9.8f;
    public GameObject raycastObject;
    public Raycast raycast; //Raycast
    //Text
    public GameObject StuffText;
    Text stuffText;
    //Sound
    public AudioClip clipYep1;
    public AudioClip clipYep2;
    public AudioClip clipSweep1;
    public AudioClip clipSweep2;
    public AudioClip clipSweep3;
    public AudioClip clipSweep4;
    public AudioClip clipRandom1;
    public AudioClip clipRandom2;
    AudioSource audioPlay;
    public GameObject DataManager;
    DataManager dataManager;

    // Use this for initialization
    void Start()
    {

        audioPlay = GetComponent<AudioSource>();
        raycast = raycastObject.GetComponent<Raycast>();
        _characterController = GetComponent<CharacterController>();
        stuffText = StuffText.GetComponent<Text>();
        dataManager = DataManager.GetComponent<DataManager>();
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
                        stuffText.text = string.Format("{0}\nWorking Sandwiches:  {1}/{2}\nWork Progress:       {3}/{4}"
                                                                , raycast.objectTouch.name, k.workingSandwiches, k.maximumSandwiches, k.currentTime, k.maximumTime);

                    }
                    else //Is producing new sandwiches
                    {
                        stuffText.text = string.Format("{0}\nWorking Sandwiches:  {1}/{2}\nWork Progress:       {3}/{4}"
                                                               , raycast.objectTouch.name, k.workingSandwiches, k.maximumSandwiches, k.currentTime, k.workingTime);

                    }

                }
                else
                {

                    stuffText.text = string.Format("{0}\nWorking Sandwiches:  {1}/{2}\nWork Progress:       {3}/{4}"
                                        , raycast.objectTouch.name, s.workingSandwiches, s.maximumSandwiches, s.currentTime, s.maximumTime);
                }

                //Click left button of Mouse. Set the goal of sandwiches
                if (Input.GetMouseButtonDown(0))
                {
                    if (dataManager.sandwichHolding > 0)
                    {
                        int randomSound = Random.Range(1, 5);
                        if (randomSound == 1)
                        {
                            audioPlay.clip = clipSweep1;
                        }
                        else if (randomSound == 2)
                        {
                            audioPlay.clip = clipSweep2;
                        }
                        else if (randomSound == 2)
                        {
                            audioPlay.clip = clipSweep3;
                        }
                        else
                        {
                            audioPlay.clip = clipSweep4;
                        }
                        audioPlay.Play();
                    }
                    s.OrderSandwich();
                }
                //Click right button of Mouse. Cancel a work.
                else if (Input.GetMouseButtonDown(1))
                {
                    if (s.workingSandwiches >0)
                    {
                        int randomSound = Random.Range(1, 3);
                        if (randomSound == 1)
                        {
                            audioPlay.clip = clipRandom1;
                        }
                        else
                        {
                            audioPlay.clip = clipRandom2;
                        }
                        audioPlay.Play();
                    }
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
                    int randomSound = Random.Range(1, 3);
                    if (randomSound == 1)
                    {
                        audioPlay.clip = clipYep1;
                    }
                    else
                    {
                        audioPlay.clip = clipYep2;
                    }
                    audioPlay.Play();
                    s.SelectSandwich();

                }
                //Click right button of Mouse. Deselect a sandwich
                else if (Input.GetMouseButtonDown(1))
                {
                    
                    if (s.DeselectSandwich())
                    {
                        int randomSound = Random.Range(1, 3);
                        if (randomSound == 1)
                        {
                            audioPlay.clip = clipRandom1;
                        }
                        else
                        {
                            audioPlay.clip = clipRandom2;
                        }
                        audioPlay.Play();
                    }
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

        //End Game
        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene("EndingScene");
        }

    }

}
