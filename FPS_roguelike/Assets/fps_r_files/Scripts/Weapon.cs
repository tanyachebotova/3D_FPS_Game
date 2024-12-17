using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform bullet;
    public int BulletForce = 5000; /*сила*/
    public int Magaz = 50;
    public AudioClip Reload;
    public AudioClip Fire;
    public float fireRate = 0.1f; // периодичность стрельбы в секундах

    private bool isFiring = false;

    void Update()
    {
        if (Input.GetMouseButton(0) && Magaz > 0 && !isFiring)
        {
            StartCoroutine(FireWeapon());
        }

        /*перезарядка*/
        if (Input.GetKeyDown(KeyCode.R))
        {
            Magaz = 50;
        }
    }

    IEnumerator FireWeapon()
    {
        isFiring = true;

        while (Input.GetMouseButton(0) && Magaz > 0)
        {
            Transform BulletInstance = Instantiate(bullet, GameObject.Find("Spawn").transform.position, Quaternion.identity);
            BulletInstance.GetComponent<Rigidbody>().AddForce(transform.forward * BulletForce);


            Magaz = Magaz - 1;
            GetComponent<AudioSource>().PlayOneShot(Fire);

            yield return new WaitForSeconds(fireRate);
        }

        isFiring = false;
    }
}


