using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipManager : MonoBehaviour
{
    [SerializeField] PlayerShipSO actualPlayerShipSO;
    [SerializeField] List<PlayerShipSO> playerShipSOs = new List<PlayerShipSO>();
    [SerializeField] List<Image> shipButtonImages = new List<Image>();

    private void OnEnable()
    {
        InitializeButtons();
    }

    void InitializeButtons()
    {
        for (int i = 0; i < shipButtonImages.Count; i++)
        {
            if (!playerShipSOs[i].ShipOwned)
            {
                DisableButton(i);
                ChangeTextTo("Ship Not Owned", i);
            }

            if (actualPlayerShipSO.CurrentShipIndex == i)
            {
                DisableButton(i);
                ChangeTextTo("Currently Using", i);
            }

            if (playerShipSOs[i].ShipOwned && actualPlayerShipSO.CurrentShipIndex != i)
            {
                EnableButton(i);
            }
        }
    }

    void ChangeTextTo(string newText, int index)
    {
        shipButtonImages[index].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = newText;
    }

    public void SetPlayerShipSO(int shipIndex)
    {
        var ship = playerShipSOs[shipIndex];
        actualPlayerShipSO.PlayerShip = ship.PlayerShip;
        actualPlayerShipSO.Guns.Clear();
        for (int i = 0; i < ship.Guns.Count; i++)
        {
            actualPlayerShipSO.Guns.Add(ship.Guns[i]);
        }
        actualPlayerShipSO.ShipMaxHealth = ship.ShipMaxHealth;
        actualPlayerShipSO.ShipCurrentHealth = ship.ShipCurrentHealth;

        actualPlayerShipSO.CurrentShipIndex = shipIndex;

        InitializeButtons();
    }
    void EnableButton(int index)
    {
        // Debug.Log("index: " + index);
        shipButtonImages[index].gameObject.transform.GetChild(2).gameObject.SetActive(true);
        shipButtonImages[index].gameObject.transform.GetChild(3).gameObject.SetActive(false);
    }

    void DisableButton(int index)
    {
       // Debug.Log("index: " + index);
        shipButtonImages[index].gameObject.transform.GetChild(2).gameObject.SetActive(false);
        shipButtonImages[index].gameObject.transform.GetChild(3).gameObject.SetActive(true);
    }
}