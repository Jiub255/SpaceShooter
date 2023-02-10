using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject shipMenu;
    [SerializeField] GameObject shipShopMenu;
    PlayerMovement playerMovement;
    PlayerShoot playerShoot;
    HUDManager hudManager;

    private void OnEnable()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        playerShoot = player.GetComponent<PlayerShoot>();
        hudManager = GameObject.FindWithTag("HUD").GetComponent<HUDManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // toggle ship menu and pause

            if (shipMenu.activeInHierarchy)
            {
                SetShipInScripts();
                shipMenu.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                shipMenu.SetActive(true);
                Time.timeScale = 0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            // toggle shipShop menu and pause

            if (shipShopMenu.activeInHierarchy)
            {
                shipShopMenu.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                shipShopMenu.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    void SetShipInScripts()
    {
        playerMovement.ChangeShip();
        playerShoot.ResetGuns();
        hudManager.UpdateHealthText();
    }
}