using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunContoroller : MonoBehaviour
{
    public Pickup ammoType; 
    public GameObject FireParticle;
    public Animator gunAnim;
    public bool Automatic;
    public AudioSource sound;
    public AudioClip fireSound;
    public AudioClip reloadSound;
    public AudioClip SwapInSound;
    public float FireRate;
    public bool isFireable;
    public string fireButton;
    public Vector3 offset;
    public string Name;
    public Sprite image;
    public InventoryController inventory;
    public Vector3 holdPosition;
    public Quaternion holdRotation;
    public GunTypes type;
    public GameObject shootPoint;   
    public GameObject playerCharacter;
    public GameObject crossHair;
    public bool isShotgunOrRocket;
    public GameObject bullet;

    public GameObject bulletHole;
    public int damagePerShot;
    public float timeInAir;
    public float bulletSpeed;
    public int reloadAmount;
    public int loadedAmmo;
    public BulletController lastBullet;
    public float reloadTime;
    public Camera playerCam;
    // Start is called before the first frame update
    void Start()
    {
        playerCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        reloadAmount = type.reloadAmount;
        reloadTime = type.reloadTime;
           ammoType = type.ammoType;
      //  bulletHole = type.bulletHole;
        Name = type.Name;
        inventory = GameObject.Find("FPSController").GetComponent<InventoryController>();
        gunAnim = GetComponent<Animator>();
     //   crossHair = GameObject.Find(Name + "CrossHair" );
        shootPoint = transform.Find("ShootPoint").gameObject;
        FireParticle = type.fireParticle;
        Automatic = type.Automatic;
        sound = GetComponent<AudioSource>();
        fireSound = type.fireSound;
        reloadSound = type.reloadSound;
        SwapInSound = type.SwapInSound;
        FireRate = type.FireRate;
        fireButton = type.fireButton;
        sound = GetComponent<AudioSource>();
        image = type.image;
        holdPosition = type.holdPosition;
        holdRotation = type.holdRotation;
        isShotgunOrRocket = type.isShotgunOrRocket;
       
        damagePerShot = type.damagePerShot;
        timeInAir = type.timeInAir;
        bulletSpeed = type.bulletSpeed;
        
        if(Name == "Shotgun")
        {
            Debug.Log("Name Correct");
        }

     }

  
    public void SetPosition()
    {
        //Debug.Log("Doing");

        transform.localPosition = holdPosition;
        transform.localRotation = holdRotation;
        if(transform.localPosition == new Vector3(-0.2f, -0.3f, 1))
        {
          //  Debug.Log("Correct");
        }

        
    }




    // Update is called once per frame
    void Update()
    {
        if(isShotgunOrRocket == false)
        {
  if(loadedAmmo == 0)
        {
                gunAnim.SetBool("Firing", false);
        }
        }
      

        if (Input.GetKeyDown(KeyCode.R))
        {
            if(isFireable == true)
                StartCoroutine(Reload());
        }
            
        if (Automatic)
        { 
            if (Input.GetButtonUp(fireButton))
            {
                gunAnim.SetBool("Firing", false);
            }
        }
        if (isFireable)
        {

          
            if (loadedAmmo >= 1)
            {
                if (Automatic)
                {
                    if (Input.GetButton(fireButton))
                    {
                        loadedAmmo -= 1;
                        transform.rotation = new Quaternion(0, 0, 0, 0);
                        Fire();
                        Debug.Log("ShouldBe");
                    }

                    if (Input.GetButtonUp(fireButton))
                    {
                        gunAnim.SetBool("Firing", false);
                    }

                }
                else
                {
                    if (Input.GetButtonDown(fireButton))
                    {
                        loadedAmmo -= 1;
                        transform.rotation = new Quaternion(0, 0, 0, 0);
                        Fire();
                    }
                }
            }
        }
        






    }

    public void Fire()

    {
        
        if (isFireable) 
        {
            Debug.Log("Fire");
            if (Automatic)
                gunAnim.SetBool("Firing", true);
            else
            {
                gunAnim.SetTrigger("Fire");
            }
                
                StartCoroutine(FireLight());
        }
        else
        {   
    
        }
        inventory.UpdateUI();

    }
  
    public IEnumerator Reload()
    {
        

        bool correct = false;
        if (ammoType == Pickup.smallAmmo)
            if(inventory.SmallAmmo > 0)
                correct = true;
        if (ammoType == Pickup.medAmmo)
            if (inventory.MedAmmo > 0)
                correct = true;
        if (ammoType == Pickup.largeAmmo)
            if (inventory.LargeAmmo > 0)
                correct = true;
        if (correct)
        {
            Debug.Log("Reload");
            if (isShotgunOrRocket != true)
            {
                if (sound.clip != reloadSound)
                    sound.clip = reloadSound;
                sound.Play();

                if (ammoType == Pickup.smallAmmo)
                {
                    if (inventory.SmallAmmo < reloadAmount)
                    {
                        loadedAmmo += inventory.SmallAmmo;
                        inventory.SmallAmmo = 0;
                    }
                    else
                    {
                        loadedAmmo += reloadAmount;
                        inventory.SmallAmmo -= reloadAmount;
                    }

                }
                if (ammoType == Pickup.medAmmo)
                {
                    if (inventory.MedAmmo < reloadAmount)
                    {
                        loadedAmmo += inventory.MedAmmo;
                        inventory.MedAmmo = 0;
                    }
                    else
                    {
                        loadedAmmo += reloadAmount;
                        inventory.MedAmmo -= reloadAmount;
                    }

                }
                if (ammoType == Pickup.largeAmmo)
                {
                    if (inventory.LargeAmmo < reloadAmount)
                    {
                        loadedAmmo += inventory.LargeAmmo;
                        inventory.LargeAmmo = 0;
                    }
                    else
                    {
                        loadedAmmo += reloadAmount;
                        inventory.LargeAmmo -= reloadAmount;
                    }

                }
            }

            isFireable = false;
            gunAnim.SetTrigger("Reload");
            yield return new WaitForSeconds(reloadTime);
            SetPosition();


            isFireable = true;
            inventory.UpdateUI();
        }
        
    }
    public void Hit()
    {
        
    }
    public void ShootBullet()
    {
        
        
        if(ammoType != Pickup.rockets)
        {

            RaycastHit hit;
            Debug.Log("Test111");
            if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit))
            {


                if (hit.transform.CompareTag("Enemy"))
                {
                    Debug.Log("Hit Enemy");
                    hit.transform.SendMessage("BeenHit", damagePerShot);
                }
                else if (hit.transform.CompareTag("Destructable"))  
                    hit.transform.GetComponent<Destructable>().Hit(damagePerShot);
                /*
                if(hit.transform.gameObject.CompareTag("Hitable"))
                    Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                    */
            }
        }
        
        
        lastBullet = Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation).GetComponent<BulletController>();
        lastBullet.enabled = true;
    }
   public void SetFireable()
    {
        isFireable = true;
    }

    public IEnumerator FireLight()
    {
   
        isFireable = false;
        ParticleSystem particle = Instantiate(FireParticle, shootPoint.transform.position, shootPoint.transform.rotation).GetComponent<ParticleSystem>();
        particle.Play();
        if(sound.clip != fireSound)
            sound.clip = fireSound;
     
              
        sound.Play();
        ShootBullet();
        if(isShotgunOrRocket)
        {
            yield return new WaitForSeconds(reloadTime);
           
            sound.clip = reloadSound;
            sound.Play();
            Debug.Log("ReloadShotgunorrocket");
            if (ammoType == Pickup.shells)
            {
                if (inventory.Shells >= 1)
                {
                    loadedAmmo += 1;
                    inventory.Shells -= 1;
                    
                }


                
                
            }
            if(ammoType == Pickup.rockets)
            {
                if(inventory.Rockets >= 1)
                {
                    loadedAmmo += 1;
                    inventory.Rockets -= 1;
                }   
            }

           
           

        }
        yield return new WaitForSeconds(FireRate);
       
        SetPosition();
        isFireable = true;
        inventory.UpdateUI();

    }
}
