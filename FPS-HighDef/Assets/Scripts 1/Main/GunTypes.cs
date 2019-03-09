using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Gun Type", menuName = "Types/Gun Type")]
public class GunTypes : ScriptableObject
{   
    public Pickup ammoType;

    public bool Automatic;

    public float FireRate;
    public GameObject bulletHole;
    public bool isShotgunOrRocket;
    public float shotArea;
    public string fireButton;

    public Vector3 offset;
    public string Name;
    public Sprite image;
    public AudioClip fireSound;
    public AudioClip reloadSound;
    public AudioClip SwapInSound;
    public Vector3 holdPosition;
    public Quaternion holdRotation;
    public float timeInAir;
    public int damagePerShot;
    public float bulletSpeed;
    public int reloadAmount;
    public GameObject fireParticle;
    public float reloadTime;
}
