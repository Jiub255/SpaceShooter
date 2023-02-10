using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Collections.Generic;

public class PlayerHealthManager : MonoBehaviour
{
    // Health Stuff
    [SerializeField] PlayerShipSO playerShipSO; // get ship health from here
    public static event UnityAction PlayerGotHit;

    // Invulnerability Stuff
    [SerializeField] float invulnerabilityDuration = 0.25f;
    [SerializeField] float flashDuration = 0.05f;
    [SerializeField] List<SpriteRenderer> spriteRenderers; 
    bool invulnerable;

    // Collision stuff
    public static event UnityAction<int, int, float, Vector3> CollideWithEnemy;

    void OnEnable()
    {
        EnemyHealth.EnemyHitPlayer += HurtPlayer;
        EnemyProjectile.EnemyProjectileHitPlayer += HurtPlayer;
        InitializeHealth();
        StartCoroutine(SetSpriteRenderers());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.GetComponent<EnemyHealth>())
        {
            int enemyID = collision.collider.gameObject.transform.parent.GetInstanceID();

            int minHealth = Mathf.Min(collision.collider.gameObject.GetComponent<EnemyHealth>().enemyHealth,
                playerShipSO.ShipCurrentHealth);

           // Debug.Log("Enemy Health " + collision.collider.GetComponent<EnemyHealth>().enemyHealth);
         //   Debug.Log("Player Health " + playerShipSO.ShipCurrentHealth);
           // Debug.Log("Min Health " + minHealth);

            CollideWithEnemy?.Invoke(enemyID, minHealth, 0f, Vector3.up);
            HurtPlayer(minHealth);
        }
    }

    void InitializeHealth()
    {
        playerShipSO.ShipCurrentHealth = playerShipSO.ShipMaxHealth;
    }

    IEnumerator SetSpriteRenderers()
    {
        yield return new WaitForSeconds(0.2f);

        foreach (Transform child in transform)
        {
            spriteRenderers.Add(child.GetComponent<SpriteRenderer>());
        }
    }

    void HurtPlayer(int damage)
    {
        if (!invulnerable)
        {
            playerShipSO.ShipCurrentHealth -= damage;
            //Debug.Log("player health: " + playerShipSO.ShipCurrentHealth);
            if (playerShipSO.ShipCurrentHealth > 0)
            {
                PlayerGotHit?.Invoke();
                StartCoroutine(FlashCoroutine());
            }
            else
            {
                Die();
            }
        }
    }

    IEnumerator FlashCoroutine()
    {
        invulnerable = true;

        for (int i = 0; i < Mathf.Floor(invulnerabilityDuration / flashDuration); i++)
        {
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                ChangeAlpha(0.5f);
            }
            yield return new WaitForSeconds(flashDuration / 2f);

            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                ChangeAlpha(1f);
            }
            yield return new WaitForSeconds(flashDuration / 2f);
        }

        invulnerable = false;
    }

    void ChangeAlpha(float newAlpha)
    {
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            Color temp = spriteRenderer.color;
            temp.a = newAlpha;
            spriteRenderer.color = temp;
        }
    }

    void Die()
    {
        playerShipSO.ShipCurrentHealth = 0;
        PlayerGotHit?.Invoke();
        DisablePlayerAndControls();
        StartCoroutine(WaitThenResetLevel());
    }

    void DisablePlayerAndControls()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponent<PlayerShoot>().enabled = false;
    }

    IEnumerator WaitThenResetLevel()
    {
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    void OnDisable()
    {
        EnemyHealth.EnemyHitPlayer -= HurtPlayer;
        EnemyProjectile.EnemyProjectileHitPlayer -= HurtPlayer;
    }
}