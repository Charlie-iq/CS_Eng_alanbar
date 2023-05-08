using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float minX, maxX;
    Rigidbody2D rb;

    private void Awake()
    {
        rb= GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        Destroy(gameObject,4f);
        rb.gravityScale = GameManger._inst.globalSpeed;
    }
    
}
