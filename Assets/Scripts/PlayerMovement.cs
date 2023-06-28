using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed;

    [Header("Padding")]
    [SerializeField] float horizontalPadding;
    [SerializeField] float topPadding;
    [SerializeField] float bottomPadding;

    Vector2 rawInput;
    Vector2 minBounds;
    Vector2 maxBounds;

    Vector2 delta;
    Vector2 newPos;

    void Start()
    {
        InitBounds();
    }
    void Update()
    {
        Move();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void Move()
    {
        delta = rawInput * speed * Time.deltaTime;
        newPos = new Vector2(
            Mathf.Clamp(transform.position.x + delta.x, minBounds.x + horizontalPadding, maxBounds.x - horizontalPadding), 
            Mathf.Clamp(transform.position.y + delta.y, minBounds.y + bottomPadding, maxBounds.y - topPadding));
        transform.position = newPos;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }
}
