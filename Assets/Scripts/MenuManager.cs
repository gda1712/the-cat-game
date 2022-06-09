using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void PlayButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Update is called once per frame
    void QuitButton()
    {
        Debug.Log("CLosed App");
        Application.Quit();
    }
}
