using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    new Rigidbody rigidbody;
    GameObject player;

    readonly float speed = 1.0f;
    readonly float downBound = -10;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        ChasePlayer();
        DestroyOutBound();
    }

    void ChasePlayer()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        rigidbody.AddForce(lookDirection * speed);
    }

    void DestroyOutBound()
    {
        if (transform.position.y < downBound)
        {
            Destroy(gameObject);
        }
    }
}
