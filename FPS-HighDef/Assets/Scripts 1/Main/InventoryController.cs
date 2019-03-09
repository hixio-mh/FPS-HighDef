using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{


    #region Singleton
    public static InventoryController instance;
    #endregion

    public int Grenades;
    public int Meds;
    public int SmallAmmo;
    public int MedAmmo;
    public int LargeAmmo;
    public int Rockets;
    public int Shells;
   
    public GameObject gunPickupItem;

    public GameObject currentGun;
    public GunContoroller gunController;
    public int selection;
    public Vector3 dropOffset;
    public Quaternion dropRotation;
        
    public Text grenadeText;
    public Text medsText;
    public Text smallAmmoText;
    public Text medAmmoText;
    public Text largeAmmoText;
    public Text RocketsText;
    public Text ShellsText;
    public Text gunText;
    public Image gunImage;
    public Transform character;
    public Text loadedAmmoText;
    public GameObject[] selectionImages;
    public PickupController lastPickupController;
    public GameObject lastPickup;
    public float dropDistance;
    public GameController gameController;

    private void Update()
    {
    
     


        #region Selection Inputs
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Swap Selection");

            selection = 1;
            UpdateUI();
            Debug.Log("Swap Selection");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selection = 2;
            UpdateUI();
            Debug.Log("Swap Selection");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selection = 3;
            UpdateUI();
            Debug.Log("Swap Selection");
        }
        #endregion


    }

    public void PickupGun(GameObject gun, GameObject gunpPickup, GunContoroller controller)
    {




        if (gun == null)
            Debug.Log("Gun Null");
        if (gunpPickup == null)
            Debug.Log("gunpickup null");
        if(controller)


        if (currentGun != null)
        {  
            lastPickup = Instantiate(gunPickupItem, transform.position + transform.forward * dropDistance + dropOffset, dropRotation);
            lastPickupController = lastPickup.GetComponent<PickupController>();
            Destroy(currentGun);
            lastPickupController.loadedAmmo = gunController.loadedAmmo;
            controller.crossHair.SetActive(false);
        }



         



        currentGun = Instantiate(gun, controller.holdPosition, controller.holdRotation, character);
       
        gunController = currentGun.GetComponent<GunContoroller>();
        gameController.gunController = gunController;
    
       
        gunController.SetPosition();
        Debug.Log("Close");
        gunPickupItem = gunpPickup.gameObject;
        
        currentGun.SendMessage("SetFireable");
        UpdateUI();
        if(lastPickupController != null)
        gunController.loadedAmmo = lastPickupController.loadedAmmo;
        gunImage.color = new Color(0, 0, 0, 255);
        UpdateUI();

    }



    public void PickupItem(Pickup item, int amount)
    {
        if (item == Pickup.grenade)
            Grenades += amount;
        if (item == Pickup.meds)
            Meds += amount;
        if (item == Pickup.smallAmmo)
            SmallAmmo += amount;
        if (item == Pickup.medAmmo)
            MedAmmo += amount;
        if (item == Pickup.largeAmmo)
            LargeAmmo += amount;
        if (item == Pickup.shells)
            Shells += amount;
        if (item == Pickup.rockets)
        {
            Rockets += amount;
            if(gunController.ammoType == Pickup.rockets && gunController.loadedAmmo == 0)
            {
                Rockets -= 1;
                gunController.loadedAmmo += 1;
                
            }
            gunController.Reload();
        }

        UpdateUI();









    }


    private void Start()
    {
        Grenades = 0;
        Meds = 0;
        SmallAmmo = 0;
        MedAmmo = 0;
        LargeAmmo = 0;
        Shells = 0;
        Rockets = 0;
        selection = 1;
        

        UpdateUI();
    }


    public void UpdateUI()
    {
          grenadeText.text = Grenades.ToString();
          medsText.text = Meds.ToString();
         smallAmmoText.text = "Small Ammo: " + SmallAmmo.ToString();
         medAmmoText.text = "Medium Ammo: " + MedAmmo.ToString();
          largeAmmoText.text = "Large Ammo: " + LargeAmmo.ToString();
          ShellsText.text = "Shells: " + Shells.ToString();
   
       
        if(gunController != null)
            {
            loadedAmmoText.gameObject.SetActive(true);
            gunText.text = gunController.Name;
            gunImage.sprite = gunController.image;
            loadedAmmoText.text = "Loaded Ammo: "  + gunController.loadedAmmo.ToString();
            if(gunController.ammoType == Pickup.smallAmmo)
            {
                smallAmmoText.gameObject.SetActive(true);
                medAmmoText.gameObject.SetActive(false);
                largeAmmoText.gameObject.SetActive(false);
                ShellsText.gameObject.SetActive(false);
                RocketsText.gameObject.SetActive(false);
            }
            else if (gunController.ammoType == Pickup.medAmmo)
            {
                smallAmmoText.gameObject.SetActive(false);
                medAmmoText.gameObject.SetActive(true);
                largeAmmoText.gameObject.SetActive(false);
                ShellsText.gameObject.SetActive(false);
                RocketsText.gameObject.SetActive(false);
            }
            else if (gunController.ammoType == Pickup.largeAmmo)
            {
                smallAmmoText.gameObject.SetActive(false);
                medAmmoText.gameObject.SetActive(false);
                largeAmmoText.gameObject.SetActive(true);
                ShellsText.gameObject.SetActive(false);
                RocketsText.gameObject.SetActive(false);
            }
            else if (gunController.ammoType == Pickup.shells)
            {
                smallAmmoText.gameObject.SetActive(false);
                medAmmoText.gameObject.SetActive(false);
                largeAmmoText.gameObject.SetActive(false);
                ShellsText.gameObject.SetActive(true);
                RocketsText.gameObject.SetActive(false);
            }
            else if (gunController.ammoType == Pickup.rockets)
            {
                smallAmmoText.gameObject.SetActive(false);
                medAmmoText.gameObject.SetActive(false);
                largeAmmoText.gameObject.SetActive(false);
                ShellsText.gameObject.SetActive(false);
                RocketsText.gameObject.SetActive(true);
            }
            if (gunController != null)
                gunController.crossHair.SetActive(true);
            
        }
        
        RocketsText.text = "Rockets: " + Rockets.ToString();
        

        if (selection == 1)
        {
             selectionImages[0].SetActive(true);
                selectionImages[1].SetActive(false);
               selectionImages[2].SetActive(false);
        }
        if (selection == 2)
        {
              selectionImages[0].SetActive(false);
               selectionImages[1].SetActive(true);
               selectionImages[2].SetActive(false);
        }
        if (selection == 3)
        {
               selectionImages[0].SetActive(false);
               selectionImages[1].SetActive(false);
              selectionImages[2].SetActive(true);
        }

        ShowItems();
      
    }


    public void ShowItems()
    {
        if (currentGun != null)
        {
            if (selection == 1)
            {
                currentGun.SetActive(true);

            }
            else
            {
                currentGun.SetActive(false);
            }
        }
    
    }
    /*
    public void SwapSelection(bool forwards)
    {

        if (forwards)
        {
            if (selection == 3)
            {
                selection = 1;
            }
            else
                selection++;
        }
        else
        {
            if (selection == 1)
            {
                selection = 3;
            }
            else
                selection--;
        }
        UpdateUI();

    }
    */








}
public enum Pickup { grenade, meds, smallAmmo, medAmmo, largeAmmo , rockets, shells,}
