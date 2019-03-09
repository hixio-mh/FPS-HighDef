using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupController : MonoBehaviour
{

    public GameObject Gun;
    public GunContoroller controller;
    public InventoryController inventory;
    public GameObject pickup;
    public bool Pickupable;
    public int loadedAmmo;
    public Text PickupText;

    private void Start()
    {
        Pickupable = false;
        pickup = GameObject.Find(Gun.name + "_Pickup_Main");
        controller = Gun.GetComponent<GunContoroller>();

    }
    private void Update()
    {
        if (Pickupable)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Pickupable = false;
                controller.sound.clip = controller.SwapInSound;
                inventory.PickupGun(Gun, pickup, controller);
                PickupText.gameObject.SetActive(false);
                inventory.UpdateUI();
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickupable = true;
            PickupText.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickupable = false;
            PickupText.gameObject.SetActive(false);
        }
    }










}
