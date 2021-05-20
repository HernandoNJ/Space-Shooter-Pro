using UnityEngine;
[CreateAssetMenu(fileName = "ModelData", menuName = "ScriptableObject/Inventory/ModelData")]
public class ModelData : ScriptableObject
{
    public string modelName;
    public string description;
    public GameObject model;

}
