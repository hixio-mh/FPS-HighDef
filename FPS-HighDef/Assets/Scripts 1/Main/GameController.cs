using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public int kills;
    public Text eleminationsText;
    public InventoryController inventory;
    public int health;
    public bool paused;
    public GameObject pauseMenu;
    public GameObject player;
    public Animator playerAnim;
    public float timeSpeed;
    public float deathAfterTime;
    public float deathMiddleTime;
    public GameObject deathBackground;
    public GameObject deathWords;
    public AudioSource backgroundSound;
    public AudioClip deathSound;
    public bool dead;
    public GameObject playerParent;
    public GunContoroller gunController;
    public AudioClip[] hurtSounds;

    public Slider healthUI;
    // public MouseLook m_MouseWLook;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        player.GetComponent<FirstPersonController>().enabled = true;
        kills = 0;
        UpdateUI();
        backgroundSound = GetComponent<AudioSource>();
    }
    public void Hurt(int amount)
    {
        health -= amount;
        backgroundSound.clip = hurtSounds[Random.Range(0, 3)];
        backgroundSound.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused == false)
                Pause();
          //  else
            //    UnPause();
        }
        if (health <= 0)
            StartCoroutine(Die());

        if (transform.position.y < 40)
            health = 0;


        healthUI.value = health;
        if (health <= 0)
            healthUI.gameObject.SetActive(false);

     /*   if(health == 80)
        {
                if(Health04.transform.localScale.y <= 0.0f)
                {
                    Health04.SetActive(false);
                }
                else
                { 
                Health04.transform.localScale -= new Vector3(0.0f, 0.05f, 0.0f);
                }
        }
        */
    }
   


    public IEnumerator Die()
    {
        if (dead != true)
        {
            if(gunController != null)
                gunController.crossHair.SetActive(false);

            playerAnim.enabled = true;
            backgroundSound.clip = deathSound;
            backgroundSound.Play(); 
            dead = true;
            player.GetComponent<FirstPersonController>().enabled = false;
            Time.timeScale = timeSpeed;
            playerAnim.SetTrigger("Die");
            deathBackground.SetActive(true);
            yield return new WaitForSeconds(deathMiddleTime);
            deathWords.SetActive(true);
            yield return new WaitForSeconds(deathAfterTime);
            SceneManager.LoadScene("MainMenu");
        }
    }
    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void UnPause()
    {
        if (dead)
            return;
        Time.timeScale = 1;
        paused = false;
        player.GetComponent<FirstPersonController>().m_MouseLook.SetCursorLock(true);
        if(gunController)
        gunController.crossHair.SetActive(true);
        player.GetComponent<FirstPersonController>().enabled = true;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
    }

    public void Pause()
    {
        if (dead)
            return;
        
        Time.timeScale = 0;
        paused = true;
        player.GetComponent<FirstPersonController>().m_MouseLook.SetCursorLock(false);
        if(gunController)
            gunController.crossHair.SetActive(false);


        player.GetComponent<FirstPersonController>().enabled = false;
        Cursor.visible = true;
        pauseMenu.SetActive(true);
        
        

    }
    public void UpdateUI()
    {
        eleminationsText.text = "Eliminations: " + kills;
    }
}
