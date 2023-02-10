using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
[System.Serializable]
public class ItemSO : ScriptableObject
{
    public UnityEvent itemEvent;
}