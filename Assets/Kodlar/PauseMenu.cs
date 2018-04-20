using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour {

    public GameObject panel;
	void Start () {
        panel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) )
        {
            Time.timeScale = 0;
            panel.SetActive(true);
            GameObject.FindGameObjectWithTag("KusTag").GetComponent<KusHaraket>().enabled = false;
        }

    }

    public void ButtonGelen(string gelen)
    {
        if(gelen=="devam")
        {
            panel.SetActive(false);
            Time.timeScale = 1;
            GameObject.FindGameObjectWithTag("KusTag").GetComponent<KusHaraket>().enabled = true;
        }
        else if(gelen=="yenile")
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("oyun");
        }
        else if(gelen=="cik")
        {        
            PlayerPrefs.SetInt("reklamSayac", 0);
            Application.Quit();
        }
    }
}
