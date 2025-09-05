using UnityEngine;

[CreateAssetMenu(fileName = "RedWine", menuName = "Alcohol/Types/RedWine")]
public class RedWine : AlcoholType
{
    public string typeName { get; private set; } = "Red Wine";    

    public override void Effect()
    {
        throw new System.NotImplementedException();
    }
}
