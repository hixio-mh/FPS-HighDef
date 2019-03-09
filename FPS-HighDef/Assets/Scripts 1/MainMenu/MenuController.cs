using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Text logoText;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;

        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {   
        logoText.color = Random.ColorHSV();
    }
    public void Options()
    {
        //When Options Become A thing
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }
}
