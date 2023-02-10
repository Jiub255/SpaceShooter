using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlanePartsScriptableObject : ScriptableObject
{
    [SerializeField] PlanePartScriptableObject nose;
    [SerializeField] PlanePartScriptableObject rightWing;
    [SerializeField] PlanePartScriptableObject leftWing;
    [SerializeField] PlanePartScriptableObject body;
    [SerializeField] PlanePartScriptableObject tail;
}
