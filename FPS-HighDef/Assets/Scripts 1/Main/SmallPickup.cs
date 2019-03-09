using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmallPickup : MonoBehaviour
{
    public Pickup item;
    public bool pickupable;
    public int amount;

    public InventoryController inventory;
    public AudioSource audioSource; 
    public float WaitTime;
    public Text pickupText;
    public bool pickupNow;
    public bool outSide;
    
    public float timeLeft;
    public bool isPickingUp;
    public Image timer;
 //   public Text timeText;
    public GameObject showTimer;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timeLeft = WaitTime;

        pickupable = false;
        pickupNow = false;
        outSide = true;
    }
    private void Update()
    {
        //timeText.text = "" + timeLeft;

        if (isPickingUp)
        {
            timeLeft -= Time.deltaTime;
            showTimer.SetActive(true);
            
            timer.fillAmount = timeLeft / WaitTime;
        }


      /*  else
        {
           
            showTimer.SetActive(false);
        

        }
        */
        if (pickupable)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {

                StartStop(true);
                pickupable = false;
                audioSource.Play();
                isPickingUp = true;


            }
        }
            if(pickupNow)
            {
                inventory.PickupItem(item, amount);
                Destroy(gameObject);
                inventory.UpdateUI();
                pickupText.gameObject.SetActive(false);
                showTimer.SetActive(false);
            }
        
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            {
            
            pickupable = true;
            pickupText.gameObject.SetActive(true);
            outSide = false;

            }

        
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {   
            outSide = true;
            pickupable = false;
            pickupText.gameObject.SetActive(false);
            StartStop(false);
            isPickingUp = false;
            timeLeft = WaitTime;
            audioSource.Stop();
            showTimer.SetActive(false);
        }
    }

    public void StartStop(bool start)
    {

        
        IEnumerator co = PickupNow();

        if (start)
            StartCoroutine(co);
        else
            StopCoroutine(co);
        
       
    }
 

    public IEnumerator PickupNow()
    {


        yield return new WaitForSeconds(WaitTime);
       
        if(outSide != true)
            pickupNow = true;
    }

}
