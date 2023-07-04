using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    float health = 100f;
    [SerializeField] float maxHealth = 100f;
    [SerializeField] int pointValue;
    [SerializeField] ParticleSystem hitEffect;

    UIDisplay ui;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;
    CameraShake cameraShake;
    AudioPlayer audioPlayer;

    bool isPlayer = false;
    bool isEnemy = false;
    bool isPickup = false;

    void Awake()
    {
        ui = FindObjectOfType<UIDisplay>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
        cameraShake = FindObjectOfType<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    void Start()
    {
        health = maxHealth;

        if (gameObject.CompareTag("Enemy"))
        {
            isEnemy = true;
        }
        else if (gameObject.CompareTag("Pickup"))
        {
            isPickup = true;
        }
        else if (gameObject.CompareTag("Player"))
        {
            audioPlayer.PlayAnnouncerClip();
            isPlayer = true;
            scoreKeeper.ResetScore();
            scoreKeeper.SetLife(true);
        }
        
    }

    public float GetHealth() { return health; }
    public float GetMaxHealth() { return maxHealth; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isPickup)
        {
            if(collision.CompareTag("Player"))
                TakeDamage(collision.GetComponent<Damage>());
        }
        else
        {
            TakeDamage(collision.GetComponent<Damage>());
        }
    }

    void TakeDamage(Damage damage)
    {
        if(damage != null)
        {
            float damageValue = damage.GetDamage();

            if(damageValue > 0)
            {
                if (hitEffect != null)
                    PlayHitEffect();

                if (audioPlayer != null)
                    audioPlayer.PlayExplosionClip();

                if (isPlayer)
                {
                    if (cameraShake != null)
                        cameraShake.Play();     
                }
            }

            health -= damageValue;

            if (health > maxHealth)
                health = maxHealth;
            else if (health <= 0)
                Die();

            if (ui != null)
                ui.UpdateHealth();
        }
    }

    void PlayHitEffect()
    {
        ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
    }

    //Health Pickup Method

    void Die()
    {
        if(isEnemy)
        {
            if (scoreKeeper != null)
            {
                scoreKeeper.AddScore(pointValue);
            }
            EnemyAI enemy = GetComponent<EnemyAI>();
            if(enemy != null)
            {
                enemy.SpawnHealth();
            }
        }

        if(isPickup)
            audioPlayer.PlayPickupClip();
       

        if (levelManager != null && isPlayer)
        {
            levelManager.LoadGameOver();
            scoreKeeper.SetLife(false);
        }

        gameObject.SetActive(false);
    }
}
