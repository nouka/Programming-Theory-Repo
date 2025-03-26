using System.Collections;
using UnityEngine;

// TODO: クラスのプロパティについてカプセル化を検討する。
public class Bullet : MonoBehaviour
{
    public float speed = 100;
    public float attackPower = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.up * speed, ForceMode.Impulse);
        StartCoroutine(AutoDestroy());
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Vector3 direction = (transform.position - other.transform.position).normalized;
            other.GetComponent<Rigidbody>().AddForce(direction * attackPower, ForceMode.Impulse);
            Destroy(gameObject);
        }
    }

    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
