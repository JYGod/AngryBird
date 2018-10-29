using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour {

    public float minSpeed = 4f;
    public float maxSpeed = 8f;
    public Sprite hurtPig;
    public GameObject boom;
    public GameObject score;
    public bool isPig = false;

    private SpriteRenderer render;

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float collideSpeed = collision.relativeVelocity.magnitude;
        if (collideSpeed > maxSpeed) // 直接死亡 
        {
            Dead();
        }
        else if (collideSpeed < maxSpeed && collideSpeed > minSpeed)
        {
            render.sprite = hurtPig;
        }
    }

    private void Dead()
    {
        if (isPig)
        {
            GameManager._instance.pigList.Remove(this);
        }
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameObject scoreGo = Instantiate(score, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
        Destroy(scoreGo, 1.5f);
    }
}
