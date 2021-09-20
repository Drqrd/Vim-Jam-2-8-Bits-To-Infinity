using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [Header("References")]

    public GameObject targetObj;
    public GameObject objToShoot;

    [Header("Parameters")]
    [Range(0.5f,2f)]
    [SerializeField] private float particleSize;

    [Range(0.5f, 10f)]
    [SerializeField] private float detectionDistance = 2f;

    [Tooltip("If true, shoots periodically. Otherwise shoot when detecting player")]
    [SerializeField] private bool shootPeriodically;
    [Range(1f,5f)]
    [SerializeField] private float timeBetweenShots = 1f;
    [Range(0.1f, 1f)]
    [SerializeField] private float minimumBetweenShots = 0.1f;
    [Range(0f, 0.5f)]
    [SerializeField] private float shotVariability = 0.2f;

    private float timer = 0f, timeBetweenShotsActual;

    private void Awake()
    {
        timer = 0f;
        timeBetweenShotsActual = RandomizeTime();
    }

    private void Update()
    {
        if (shootPeriodically)
        {
            TimedShot();
        }
        else { DetectPlayer(); }
    }

    private void Shoot()
    {
        GameObject obj = Instantiate(objToShoot, transform.position, transform.rotation);
        obj.transform.localScale = new Vector3(particleSize, particleSize, particleSize);
    }

    private void TimedShot()
    {
        if (timer >= timeBetweenShotsActual)
        {
            timer -= timeBetweenShotsActual;
            timeBetweenShotsActual = RandomizeTime();
            Shoot();
        }
        else { timer += Time.deltaTime; }
    }

    private float RandomizeTime()
    {
        if (shotVariability > 0f)
        {
            float min = timeBetweenShots - shotVariability >= minimumBetweenShots ? minimumBetweenShots : timeBetweenShots - shotVariability;
            return Random.Range(min, timeBetweenShots + shotVariability);
        }
        else { return timeBetweenShots; }
    }

    private void DetectPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward * detectionDistance );
        if (hit.transform.tag == "Player") { Shoot(); }
    }
}
