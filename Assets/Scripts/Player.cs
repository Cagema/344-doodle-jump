using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _jumpForce;
    [SerializeField] float _horSpeed;
    [SerializeField] bool _flip;

    [SerializeField] float _spawnPlatformDist;
    float _height = 0;
    float _toPlatformDist;

    Spawner _spawner;
    SpriteRenderer _sr;
    Rigidbody2D _rb;
    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _rb= GetComponent<Rigidbody2D>();
        _spawner = FindObjectOfType<Spawner>();
    }

    private void Update()
    {
        if (GameManager.Single.GameActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var mousePos = GameManager.Single.MainCamera.ScreenToWorldPoint(Input.mousePosition);
                _sr.flipX = _flip ^ (mousePos.x < 0);
            }

            if (Input.GetMouseButton(0))
            {
                var mousePos = GameManager.Single.MainCamera.ScreenToWorldPoint(Input.mousePosition);
                if (mousePos.x < 0)
                {
                    transform.Translate(_horSpeed * Time.deltaTime * Vector2.left);
                }
                else
                {
                    transform.Translate(_horSpeed * Time.deltaTime * Vector2.right);
                }
            }

            if (transform.position.x > GameManager.Single.RightUpperCorner.x + 0.5f)
            {
                transform.position = new Vector3(-GameManager.Single.RightUpperCorner.x - 0.5f, transform.position.y, transform.position.z);
            }

            if (transform.position.x < -GameManager.Single.RightUpperCorner.x - 0.5f)
            {
                transform.position = new Vector3(GameManager.Single.RightUpperCorner.x + 0.5f, transform.position.y, transform.position.z);
            }

            if (transform.position.y > _height)
            {
                _toPlatformDist += transform.position.y - _height;
                if (_toPlatformDist > _spawnPlatformDist)
                {
                    _toPlatformDist = 0;
                    _spawner.Spawn();
                }
                _height = transform.position.y;
                if (_height > GameManager.Single.Score)
                {
                    GameManager.Single.Score = (int)_height;
                }
            }

            if (transform.position.y < GameManager.Single.RightUpperCorner.y - 2f - 10f)
            {
                GameManager.Single.LostLive();
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            if (_rb.velocity.y <= 0)
            {
                Jump();
            }
        }

        if (collision.CompareTag("Fragile"))
        {
            if (_rb.velocity.y <= 0)
            {
                Jump();
                Destroy(collision.gameObject);
            }
        }

        if (collision.CompareTag("Fake"))
        {
            if (_rb.velocity.y <= 0)
            {
                Destroy(collision.gameObject);
            }
        }
    }

    private void Jump()
    {
        _rb.velocity = Vector2.zero;
        _rb.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
    }

    public void StartFall()
    {
        _rb.gravityScale = 1;
    }
}
