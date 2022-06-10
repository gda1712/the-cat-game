using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    // Start is called before the first frame update
    public void PlayButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitMenuButton()
    {
        SceneManager.LoadScene("MenuScene");
    }

    // Update is called once per frame
    public void QuitButton()
    {
        Debug.Log("CLosed App");
        Application.Quit();
    }
}
