using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    private Rigidbody rb;
    public float speed;
    public GunContoroller parentController;
    public GameObject character;
    public int damage;
    public Quaternion rotation;
    public float timeInAir;
    public GameObject bulletHole;

    // Start is called before the first frame update




    void Start()
    {
        parentController = character.GetComponentInChildren<GunContoroller>();
        speed = parentController.bulletSpeed;
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        timeInAir = parentController.timeInAir;
        rb.velocity = transform.up * parentController.bulletSpeed;
        transform.rotation = rotation;
        damage = parentController.damagePerShot;
        StartCoroutine(Time());
        
    }

    private void OnTriggerEnter(Collider other)
    {   
        /*
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SendMessage("BeenHit", damage);
        }
        */
        
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
         
    }
    public IEnumerator Time()
    {
        yield return new WaitForSeconds(timeInAir);
        Destroy(this.gameObject);
    }


}
    