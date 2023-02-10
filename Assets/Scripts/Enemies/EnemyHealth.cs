using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public static event UnityAction<int> EnemyHitPlayer;

    public int enemyHealth = 3;
    //[SerializeField] int enemyTouchDamage = 1;
    [SerializeField] float knockbackResistance = 0f;
    EnemyMovement enemyMovement;
    ParticleSystem particleSys;
    [SerializeField] int minCoinDrop = 0;
    [SerializeField] int maxCoinDrop = 3;
    [SerializeField] float coinSpreadRadius = 0.2f;
    EnemyShoot enemyShoot;
    // [SerializeField] LootTable thisLoot;

    private void OnEnable()
    {
        Projectile.HitEnemy += HurtEnemy;
        //  ShipSmash.HitEnemy += HurtEnemy;
        PlayerHealthManager.CollideWithEnemy += HurtEnemy;

        enemyMovement = transform.parent.GetComponent<EnemyMovement>();
        particleSys = transform.parent.GetChild(1).GetComponent<ParticleSystem>();
        enemyShoot = GetComponent<EnemyShoot>();
    }

    void HurtEnemy(int enemyID, int damage, float knockback, Vector3 knockbackDirection)
    {
        if (gameObject.transform.parent.GetInstanceID() == enemyID)
        {
            enemyHealth -= damage;
/*            if (enemyHealth > 0)
            {
                float netKnockback = knockback - knockbackResistance;
                if (netKnockback > 0)
                {
                    knockbackDirection.Normalize();
                    enemyMovement.StartKnockbackCoroutine(knockbackDirection, netKnockback);
                }
            }*/
            if (enemyHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        particleSys.Play();
        DropCoins(minCoinDrop, maxCoinDrop);
        DisableSpriteAndColliderAndControls();
        StartCoroutine(WaitThenDie());
    }

    void DisableSpriteAndColliderAndControls()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        gameObject.GetComponent<EnemyShoot>().enabled = false;
        transform.parent.GetComponent<EnemyMovement>().enabled = false;
    }

    IEnumerator WaitThenDie()
    {
        yield return new WaitForSeconds(1.2f);
        transform.parent.gameObject.SetActive(false);
    }

/*    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            EnemyHitPlayer?.Invoke(enemyTouchDamage);
        }
    }*/

/*    private void MakeLoot()
    {
        if (thisLoot != null)
        {
            GameObject current = thisLoot.LootPowerup();
            if (current != null)
            {
                Instantiate(current, transform.position, Quaternion.identity);
            }
        }
    }*/

    void DropCoins(int min, int max)
    {
        int numberOfCoinsDropped = Random.Range(min, max);
        for (int i = 0; i < numberOfCoinsDropped; i++)
        {
            ActivateCoin();
        }
    }

    void ActivateCoin()
    {
        GameObject coin = ObjectPool.SharedInstance.GetPooledObject("Coin");
        if (coin != null)
        {
            // make better spread. this is a regular square. make a circle, with more the closer you get to the center
            float randX = Random.Range(-coinSpreadRadius, coinSpreadRadius);
            float randY = Random.Range(-coinSpreadRadius, coinSpreadRadius);
            coin.transform.position = new Vector3(transform.position.x + randX, transform.position.y + randY, 0f);
            coin.transform.rotation = transform.rotation;
            coin.SetActive(true);
        }
    }

    private void OnDisable()
    {
        Projectile.HitEnemy -= HurtEnemy;
        //ShipSmash.HitEnemy -= HurtEnemy;
        PlayerHealthManager.CollideWithEnemy -= HurtEnemy;
    }
}