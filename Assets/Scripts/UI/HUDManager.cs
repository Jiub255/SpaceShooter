using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField] StatsSO playerStatsSO; // for money (at the moment, may change)
    [SerializeField] PlayerShipSO playerShipSO; // for health
    [SerializeField] PlayerAmmoSO playerAmmoSO; // for ammo

    [SerializeField] TextMeshProUGUI playerHealthText;
    [SerializeField] TextMeshProUGUI playerMoneyText;
    [SerializeField] List<Image> bombs = new List<Image>();

    private void OnEnable()
    {
        PlayerHealthManager.PlayerGotHit += UpdateHealthText;
        Coin.GotACoin += GetMoney;
        OnScreenItem.onCoinCollected += GetMoney;
    }

    private void Start()
    {
        UpdateHealthText();
        UpdateMoneyText();
        UpdateBombsDisplay();
    }

    public void UpdateHealthText()
    {
        playerHealthText.text = "Health: " + playerShipSO.ShipCurrentHealth + " / " + playerShipSO.ShipMaxHealth;
    }

    public void EventWorks()
    {
        Debug.Log("UnityEvent invoked");
    }

    public void GetMoney(int numberOfCoinsGained)
    {
        Debug.Log("GetMoney called");
        playerStatsSO.money += numberOfCoinsGained;
        UpdateMoneyText();
    }

    public void UpdateMoneyText()
    {
        Debug.Log("UpdateMoneyText called");

        // changing the text in the inspector and debugs, but not on screen in game.
        // identical code and text gameobject to UpdateHealthText, which works fine

        Debug.Log("Before: " + playerMoneyText.text);
        playerMoneyText.text = "Money: " + playerStatsSO.money; // this line is working, but not showing in game. why?
        Debug.Log("After: " + playerMoneyText.text); // above line works

        // playerMoneyText.ForceMeshUpdate(true); // doesn't help
        // playerMoneyText.SetAllDirty(); // doesn't help
        playerMoneyText.enabled = false;
        playerMoneyText.enabled = true;
    }

    public void GetBombs(int ammoGained)
    {
        playerAmmoSO.Bombs += ammoGained;
        if (playerAmmoSO.Bombs > playerAmmoSO.MaxBombs)
            playerAmmoSO.Bombs = playerAmmoSO.MaxBombs;
        UpdateBombsDisplay();
    }

    void UpdateBombsDisplay()
    {
        int numberOfBombs = playerAmmoSO.Bombs;
        for (int i = 0; i < bombs.Count; i++)
        {
            if (i < numberOfBombs)
            {
                bombs[i].gameObject.SetActive(true);
            }
            else
            {
                bombs[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnDisable()
    {
        PlayerHealthManager.PlayerGotHit -= UpdateHealthText;
        Coin.GotACoin -= GetMoney;
        OnScreenItem.onCoinCollected -= GetMoney;
    }
}