using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] string bulletID;
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
    void OnEnable()
    {
        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        while(true)
        {
            GameObject instance = ObjectPoolManager.Instance.GetPooledObject(bulletID);

            if(instance != null) 
            {
                instance.transform.position = transform.position;
                instance.transform.rotation = transform.rotation;
                instance.layer = gameObject.layer;
                instance.GetComponent<Bullet>().Lifetime = bulletLifetime;
                instance.SetActive(true);

                Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();

                if (audioPlayer != null)
                    audioPlayer.PlayShootingClip();

                if (rb != null)
                rb.velocity = transform.up * bulletSpeed;
            }
            yield return new WaitForSeconds(Mathf.Clamp((Random.Range(fireRate - fireRateVariance, fireRate + fireRateVariance)), fireRateMinimum, float.MaxValue));
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
