using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;
    public bool canAttack = false;
    public float attackPower = 100;

    new Rigidbody rigidbody;
    GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        rigidbody.AddForce(lookDirection * speed);

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && canAttack)
        {
            Vector3 direction = (collision.transform.position - transform.position).normalized;
            player.GetComponent<Rigidbody>().AddForce(direction * attackPower);
        }
    }
}
