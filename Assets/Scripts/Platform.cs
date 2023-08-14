using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private void Start()
    {
        transform.position = new Vector3(Random.Range(-GameManager.Single.RightUpperCorner.x, GameManager.Single.RightUpperCorner.x), transform.position.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        if (transform.position.y < GameManager.Single.RightUpperCorner.y - 2f - 10f)
        {
            Destroy(gameObject);
        }
    }
}
