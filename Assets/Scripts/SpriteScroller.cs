using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpriteScroller : MonoBehaviour
{
    [SerializeField] Vector2 moveSpeed;
    [SerializeField] float multiplier = 1f;

    Vector2 offset;
    Material material;

    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }
    void Update()
    {
        offset = moveSpeed * Time.deltaTime;
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
            offset *= multiplier;
        material.mainTextureOffset += offset;
    }
}
