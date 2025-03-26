using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// TODO: クラスのプロパティについてカプセル化を検討する。
public class PlayerController : MonoBehaviour
{
    public float speed = 3;
    public float jumpForce = 5;
    public bool hasPowerUp = false;
    public GameObject powerUpIndicator;
    public GameObject bulletPrefab;
    public GameObject fxSmash;

    new Rigidbody rigidbody;

    GameObject focalPoint;
    ParticleSystem smashParticle;

    readonly float powerUpStrength = 15.0f;
    POWER_UP_TYPE powerUpType;
    bool smashed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        smashParticle = fxSmash.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        fxSmash.transform.position = transform.position;

        float verticalInput = Input.GetAxis("Vertical");

        rigidbody.AddForce(speed * verticalInput * focalPoint.transform.forward);

        if (hasPowerUp && (powerUpType == POWER_UP_TYPE.BULLET || powerUpType == POWER_UP_TYPE.SMASH))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (powerUpType == POWER_UP_TYPE.BULLET)
                {
                    Instantiate(bulletPrefab, transform.position, focalPoint.transform.rotation * bulletPrefab.transform.rotation);
                }
                if (powerUpType == POWER_UP_TYPE.SMASH)
                {
                    rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    smashed = true;
                }
            }
        }

        if (transform.position.y < -5)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            powerUpType = other.GetComponent<PowerUp>().type;
            powerUpIndicator.SetActive(true);
            Destroy(other.gameObject);
            StopCoroutine(PowerUpCountdownRoutine());
            StartCoroutine(PowerUpCountdownRoutine());
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp && powerUpType == POWER_UP_TYPE.STRONGER)
        {
            GameObject enemy = collision.gameObject;
            Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (enemy.transform.position - transform.position).normalized;

            enemyRb.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
        }
        if (collision.gameObject.CompareTag("Ground") && smashed)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Vector3 awayFromPlayer = enemy.transform.position - transform.position;
                Vector3 direction = awayFromPlayer.normalized;
                float strength = 100 / awayFromPlayer.magnitude;
                enemy.GetComponent<Rigidbody>().AddForce(direction * strength, ForceMode.Impulse);
            }
            // 衝撃波
            smashParticle.Play();
            smashed = false;
        }
    }

    IEnumerator PowerUpCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        powerUpIndicator.SetActive(false);
    }
}
