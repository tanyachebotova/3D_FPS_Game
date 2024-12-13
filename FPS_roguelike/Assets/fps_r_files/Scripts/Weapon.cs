using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform bullet;
    public int BulletForce = 5000; /*сила*/
    public int Magaz = 50;
    public AudioClip Reload;
    public AudioClip Fire;

    void Update(){
        if (Input.GetMouseButtonDown(0)&Magaz > 0){
            Transform BulletInstance = (Transform) Instantiate (bullet, GameObject.Find ("Spawn").transform.position, Quaternion.identity);
            BulletInstance.GetComponent<Rigidbody>().AddForce (transform.forward * BulletForce);
            Magaz = Magaz - 1;
            GetComponent<AudioSource>().PlayOneShot(Fire);
            GetComponent<AudioSource>().PlayOneShot(Reload);

        }
        /*перезарядка*/
        if (Input.GetKeyDown(KeyCode.R)){
            Magaz = 50;
        }
    }
}
