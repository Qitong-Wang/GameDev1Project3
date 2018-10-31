using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleScreen : MonoBehaviour {
    public int currentPanel;
    public List<Sprite> panels;
    public GameObject button1, button2,button3, button4;


    public void TitleSlowStart() {
        if (button2 != null)
        {
            Destroy(button2);
            Destroy(button3);
            Destroy(button1);
            button4.SetActive(true);
        }


        if (currentPanel < panels.Count)
        {
            GetComponent<SpriteRenderer>().sprite = (panels[currentPanel]);
            currentPanel++;
        }
        else {
            SceneManager.LoadScene(0);
        }
    }

    public void TitleFastStart() {
        SceneManager.LoadScene(0);
    }
    public void TitleExitGame() {
        Application.Quit();

    }
}
