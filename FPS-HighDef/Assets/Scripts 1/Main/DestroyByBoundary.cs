using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
    public Transform playerTransform;

    public GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        
    }   

    // Update is called once per frame
    void Update()
    {
        if(playerTransform.position.y < -100)
        {
            gameController.health = 0;
            Debug.Log("Die");
        }

    }
    
    public void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Player"))
        {
            gameController.health = 0;

        }
    }
}
