using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 弾速
    readonly float speed = 100;
    // 弾丸が敵に与える力
    readonly float attackPower = 100;
    // 生存期間（秒）
    readonly float lifetime = 3;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.up * speed, ForceMode.Impulse);
        StartCoroutine(AutoDestroy());
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
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
