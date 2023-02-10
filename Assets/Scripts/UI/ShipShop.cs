using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipShop : MonoBehaviour
{
    [SerializeField] List<PlayerShipSO> playerShipSOs = new List<PlayerShipSO>();
    [SerializeField] List<Image> shipButtonImages = new List<Image>();

    //[SerializeField] GameObject shipMenuDisplay;
    private void OnEnable()
    {
        InitializeButtons();
    }

    // instead, make a menu display prefab, and get a shipSO reference on it
    // then go through the playerShipSO list, 
    // instantiate a display for each one, and attach the sprite/button text&function?/title to it   
    // then could just not make buttons for already owned ships. Maybe

    void InitializeButtons()
    {
        for (int i = 0; i < shipButtonImages.Count; i++)
        {
            if (playerShipSOs[i].ShipOwned)
            {
                DisableButton(i);
            }
        }

        // OR
/*        foreach (PlayerShipSO playerShipSO in playerShipSOs)
        {
            if (!playerShipSO.ShipOwned)
            {
                //instantiate/set position/initialize display

                // lot to do
            }
        }*/
    }

    public void BuyShip(int index)
    {
        playerShipSOs[index].ShipOwned = true;
        DisableButton(index);
    }

    void DisableButton(int index)
    {
      //  Debug.Log("index: " + index);
        shipButtonImages[index].gameObject.transform.GetChild(2).gameObject.SetActive(false);
        shipButtonImages[index].gameObject.transform.GetChild(3).gameObject.SetActive(true);
    }
}