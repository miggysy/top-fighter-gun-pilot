using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    AudioPlayer audioPlayer;
    
    [Header("Bullet Settings")]
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] float bulletLifetime = 1f;

    [Header("Fire Rate")]
    [SerializeField] float fireRate = 1f;
    [SerializeField] float fireRateVariance = 0f;
    [SerializeField] float fireRateMinimum = 0.2f;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        while(true)
        {
            GameObject instance = Instantiate(bullet, transform.position, transform.rotation);

            if (audioPlayer != null)
                audioPlayer.PlayShootingClip();

            instance.layer = gameObject.layer;
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();

            if (rb != null)
                rb.velocity = transform.up * bulletSpeed;

            Destroy(instance, bulletLifetime);
            yield return new WaitForSeconds(Mathf.Clamp((Random.Range(fireRate - fireRateVariance, fireRate + fireRateVariance)), fireRateMinimum, float.MaxValue));
        }
       
    }
}
