using UnityEngine;

public class AttackableEnemy : Enemy
{
    [SerializeField] float attackPower = 100;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 direction = (collision.transform.position - transform.position).normalized;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(direction * attackPower);
        }
    }
}
