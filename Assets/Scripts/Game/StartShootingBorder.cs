using UnityEngine;

public class StartShootingBorder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyShoot enemyShoot = collision.GetComponent<EnemyShoot>();
        if (enemyShoot)
        {
            if (enemyShoot.IsShooting)
            {
                enemyShoot.IsShooting = false;
                collision.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                enemyShoot.IsShooting = true;
            }
        }
        if (!enemyShoot && !collision.CompareTag("Border"))
        {
            if (collision.transform.parent)
            {
                collision.transform.parent.gameObject.SetActive(false);
            }
            collision.gameObject.SetActive(false); //for disabling coins and stuff after they go off screen
        }
    }
}