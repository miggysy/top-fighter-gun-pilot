using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] float score;
    [SerializeField] float scoreMultiplier = 1f;
    [SerializeField] float maxScore = 999999f;
    [SerializeField] bool isAlive;

    static ScoreKeeper instance;

    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        score = 0;
    }

    void Update()
    {
        if(isAlive)
        {
            if(score < maxScore)
            {
                score += Mathf.Clamp(scoreMultiplier, 1f, float.MaxValue) * 2 * Time.deltaTime;
            }
            else
            {
                score = maxScore;
            }
        }
    }

    public float GetScore() { return score; }
    public bool GetPlayerState() { return isAlive; }
    public void AddScore(int value) { score += value; }
    public void SetLife(bool value) { isAlive = value; }
    public void ResetScore() { score = 0; }
}
