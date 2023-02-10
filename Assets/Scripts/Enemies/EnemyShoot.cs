using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] List<GunData> guns = new List<GunData>();
    bool isShooting;

    public bool IsShooting
    {
        get { return isShooting; }
        set { isShooting = value; }
    }

    private void OnEnable()
    {
        isShooting = false;
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].CanShoot = true;
        }
    }

    private void Start()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            UpdateBurstTimerLength(i);
        }
    }

    private void Update()
    {
        if (isShooting)
        {
            for (int i = 0; i < guns.Count; i++)
            {
                if (guns[i].CanShoot == false)
                {
                    CountDownShootTimer(i);
                }
                else
                {
                    ResetShootTimer(i);
                    StartCoroutine(ShootProjectiles(i));
                }
            }
        }
    }

    void CountDownShootTimer(int i)
    {
        guns[i].BurstTimer -= Time.deltaTime;
        if (guns[i].BurstTimer <= 0)
        {
            guns[i].CanShoot = true;
        }
    }

    void ResetShootTimer(int i)
    {
        guns[i].CanShoot = false;
        guns[i].BurstTimer = guns[i].BurstTimerLength;
    }

    public IEnumerator ShootProjectiles(int i)
    {
        yield return new WaitForSeconds(guns[i].StaggerTime);
        for (int j = 0; j < guns[i].ShotsPerBurst; j++)
        {
            GameObject currentProjectile = ActivateProjectile(guns[i].ProjectileSpawnOffset, guns[i].BulletAngle, i);
            currentProjectile.GetComponent<EnemyProjectile>().LaunchEnemyProjectile(guns[i].LaunchVector.normalized);
            yield return new WaitForSeconds(guns[i].TimeBetweenShotsInBurst);
        }
    }

    GameObject ActivateProjectile(Vector3 spawnOffset, float spawnAngle, int i)
    {
        GameObject currentProjectile = ObjectPool.SharedInstance.GetPooledObject(guns[i].ProjectileTag);
        if (currentProjectile != null)
        {
            currentProjectile.transform.position = transform.position + spawnOffset;
            currentProjectile.transform.rotation = Quaternion.Euler(0, 0, spawnAngle);
            currentProjectile.SetActive(true);
        }

        return currentProjectile;
    }

    void UpdateBurstTimerLength(int i)
    {
        guns[i].BurstTimerLength = 1 / guns[i].BurstsPerSecond;
    }
}

[System.Serializable]
public class GunData
{
    // Bullet/Gun Info
    [SerializeField] string projectileTag = "EnemyBullet";
    [SerializeField] Vector2 projectileSpawnOffset;
    [SerializeField] Vector2 launchVector;
    [SerializeField] float bulletAngle;
    //angles measured like this (in degrees):
    //         0
    //     45    -45
    //    90       -90
    //     135   -135
    //        180

    // Timing Stuff
    float burstTimerLength;
    float burstTimer;
    [SerializeField] float staggerTime; // for staggering shots from multiple guns
    [SerializeField] float burstsPerSecond = 1;
    [SerializeField] float timeBetweenShotsInBurst;
    [SerializeField] int shotsPerBurst;
    bool canShoot = true;


    // Properties
    public string ProjectileTag { get { return projectileTag; } set { projectileTag = value; } }
    public Vector2 ProjectileSpawnOffset { get { return projectileSpawnOffset; } set { projectileSpawnOffset = value; } }
    public Vector2 LaunchVector { get { return launchVector; } set { launchVector = value; } }
    public float BulletAngle { get { return bulletAngle; } set { bulletAngle = value; } }
    public float BurstTimerLength { get { return burstTimerLength; } set { burstTimerLength = value; } }
    public float BurstTimer { get { return burstTimer; } set { burstTimer = value; } }
    public float StaggerTime { get { return staggerTime; } set { staggerTime = value; } }
    public float BurstsPerSecond { get { return burstsPerSecond; } set { burstsPerSecond = value; } }
    public float TimeBetweenShotsInBurst { get { return timeBetweenShotsInBurst; } set { timeBetweenShotsInBurst = value; } }
    public int ShotsPerBurst { get { return shotsPerBurst; } set { shotsPerBurst = value; } }
    public bool CanShoot { get { return canShoot; } set { canShoot = value; } }
}