using System.Linq;
using UnityEngine;

// TODO: 継承を使用して敵キャラクターを作成する。現状は「通常敵」「強化敵」「ボス」「取り巻き」の4種が存在する。
// TODO: 同時にポリモーフィズム/抽象化についても検討する。敵であれば必ずプレーヤーに向かっていく。攻撃方法や強度は異なる。
// TODO: クラスのプロパティについてカプセル化を検討する。
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
