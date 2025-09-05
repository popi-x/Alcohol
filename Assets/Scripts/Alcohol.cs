using UnityEngine;

[CreateAssetMenu(fileName = "Alcohol", menuName = "Alcohol/Variant")]
public class Alcohol : ScriptableObject
{
    public float conversion;
    public AlcoholType type;
}
