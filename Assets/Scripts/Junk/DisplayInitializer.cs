using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DisplayInitializer : MonoBehaviour
{



    // attempt at instantiating/initializing buttons only for unowned ships
/*    public PlayerShipSO playerShipSO;

    TextMeshProUGUI tmproUgui;

    Image image;

    Button button;
    public UnityEvent buttonEvent;
    public string buttonText;

    private void Start()
    {
        tmproUgui = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        image = gameObject.transform.GetChild(1).GetComponent<Image>();

        button = gameObject.transform.GetChild(2).GetComponent<Button>();

        InitializeDisplay(playerShipSO, buttonText);
    }

    public void InitializeDisplay(PlayerShipSO playerShipSO, string buttonText)
    {
        tmproUgui.text = playerShipSO.ShipName;

        image.sprite = playerShipSO.PlayerShip.GetComponent<SpriteRenderer>().sprite;

        // how to assign button function through argument?
        button.onClick.AddListener(buttonEvent.Invoke);

        button.GetComponentInChildren<Text>().text = buttonText;
    }*/
}