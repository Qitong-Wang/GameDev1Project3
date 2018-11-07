using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleScreen : MonoBehaviour {
    public int currentPanel;
    public List<Vector3> panels;
    public Sprite bg;
    public Sprite comic;
    public GameObject cam;
    float lerpPercent;
    public GameObject button1, button2,button3, button4;
    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = bg;
    }

    public void TitleSlowStart() {
        if (button2 != null)
        {
            Destroy(button2);
            Destroy(button3);
            GetComponent<SpriteRenderer>().sprite = comic;
            Destroy(button1);
            button4.SetActive(true);
        }


        if (currentPanel < panels.Count-1)
        {
            currentPanel++;
            lerpPercent = 0;
        }
        else {
            SceneManager.LoadScene(0);
        }
    }
    private void Update()
    {
        if (currentPanel > 0)
        {
            lerpPercent += Time.deltaTime;
            //lerp camera
            cam.transform.position = Vector3.Lerp(panels[currentPanel - 1], panels[currentPanel], lerpPercent);
        }
    }
    public void TitleFastStart() {
        SceneManager.LoadScene(0);
    }
    public void TitleExitGame() {
        Application.Quit();

    }
}
