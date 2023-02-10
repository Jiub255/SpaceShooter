using UnityEngine;

public enum PartType
{
    Wing,
    Nose,
    Body,
    Tail,
    Gun
}

[CreateAssetMenu]
public class PlanePartScriptableObject : ScriptableObject
{
    [SerializeField] PartType partType;
    [SerializeField] Sprite sprite;
    [SerializeField] string partName;
    [TextArea(3,20)]
    [SerializeField] string description;
}