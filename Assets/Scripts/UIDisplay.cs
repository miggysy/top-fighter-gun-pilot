using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Slider healthBar;
    [SerializeField] Health playerHealth;
    [SerializeField] Image fillImage;
    [SerializeField] Sprite highHealthSprite;
    [SerializeField] Sprite mediumHealthSprite;
    [SerializeField] Sprite lowHealthSprite;

    [Header("Score")]
    [SerializeField] ScoreKeeper scoreKeeper;
    [SerializeField] TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    void Start()
    {
        healthBar.maxValue = playerHealth.GetMaxHealth();
        healthBar.value = playerHealth.GetHealth();
    }

    void Update()
    {
        UpdateScore();
    }

    public void UpdateHealth()
    {
        healthBar.value = playerHealth.GetHealth();
        if(healthBar.value > healthBar.maxValue * 0.6)
        {
            fillImage.sprite = highHealthSprite;
        }
        else if (healthBar.value > healthBar.maxValue * 0.2)
        {
            fillImage.sprite = mediumHealthSprite;
        }
        else
        {
            fillImage.sprite = lowHealthSprite; 
        }
    }
    public void UpdateScore()
    {
        if(scoreKeeper != null)
        {
            scoreText.text = Mathf.Round(scoreKeeper.GetScore()).ToString("000000");
        }
    }
}
