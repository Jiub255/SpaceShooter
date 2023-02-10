using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    public static event UnityAction<int> GotACoin;
    [SerializeField] ParticleSystem particleSys;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GotACoin?.Invoke(1);
        }
        DestroyCoin();
    }

    void DestroyCoin()
    {
        particleSys.Play();
        transform.parent.gameObject.SetActive(false);
    }
}