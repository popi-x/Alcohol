using UnityEngine;

[CreateAssetMenu(fileName = "Beer", menuName = "Alcohol/Types/Beer")]
public class Beer : AlcoholType
{
    public string typeName { get; private set; } = "Beer";
    public override void Effect()
    {
        throw new System.NotImplementedException();
    }
}
