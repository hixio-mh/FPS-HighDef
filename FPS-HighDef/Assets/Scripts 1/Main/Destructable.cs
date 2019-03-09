using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public int health;
    public GameObject shatterObject;
    public Rigidbody rb;
    public float force;
    public float radius;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Hit(int amount)
    {
        health -= amount;
        if (health <= 0)
            FallApart();
    }
    public void FallApart()
    {

        Instantiate(shatterObject, transform.position, transform.rotation);

        rb.AddExplosionForce(force, transform.position, radius, 3.0f);
        
        Destroy(gameObject);
    }
}
