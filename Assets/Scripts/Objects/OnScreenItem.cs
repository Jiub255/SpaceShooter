using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class OnScreenItem : MonoBehaviour
{
    // GENERAL ITEM HEIRARCHY LAYOUT
    //   -Item
    //      -Sprite/Collider/This script
    //      -Particle System

    [SerializeField] ItemSO itemSO;
    [SerializeField] ParticleSystem particleSys;

    public static event UnityAction<int> onCoinCollected;

    private void OnEnable()
    {
        if (transform.parent.GetComponent<ItemMagnet>())
        {
            transform.parent.GetComponent<ItemMagnet>().enabled = true;
        }
        gameObject.GetComponent<Collider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(transform.parent.name + ", ID: " + transform.parent.GetInstanceID() + " hit " + collision.name);
        if (collision.CompareTag("Player"))
        {
            Debug.Log("hit player tagged thing");
            //itemSO.itemEvent.Invoke();

            onCoinCollected?.Invoke(1);
        }

        DisableObject();
    }

    void DisableObject()
    {
        StartCoroutine(WaitThenDisable());

        if (particleSys)
        {
            particleSys.Play();
        }
    }

    IEnumerator WaitThenDisable()
    {
        // not sure of the structure here yet
        if (transform.parent.GetComponent<ItemMagnet>())
            transform.parent.GetComponent<ItemMagnet>().enabled = false; // to stop it from moving
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Debug.Log("Started to disable " + transform.parent.name + ", ID: " + transform.parent.GetInstanceID());

        yield return new WaitForSeconds(particleSys.main.duration);

        Debug.Log("Disabled " + transform.parent.name + ", ID: " + transform.parent.GetInstanceID());
        transform.parent.gameObject.SetActive(false);
    }
}