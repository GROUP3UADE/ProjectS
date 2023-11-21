using UnityEngine;
using System.Collections.Generic;

public enum DoorSide
{ 
    Top,
    Left,
    Right,
    Bottom
}

public abstract class MapNode : MonoBehaviour
{
    public List<MapNode> Children { get; private set; }
}

[CreateAssetMenu(fileName = "New block stats", menuName = "Scriptables/Block")]
public class BlockMapNodeSO : ScriptableObject
{
    #region Sets

    [SerializeField]
    protected int _children;
    [SerializeField]
    protected int _width;
    [SerializeField]
    protected int _height;

    #endregion

    #region Gets

    public int Children { get => _children; }
    public int Width { get => _width; }
    public int Height { get => _height; }
    
    #endregion
}

[CreateAssetMenu(fileName = "New building stats", menuName = "Scriptables/Building")]
public class BuildingMapNodeSO : ScriptableObject
{
    #region Sets

    [SerializeField]
    protected int _amountPerWorld;
    [SerializeField]
    protected int _amountPerCity;
    [SerializeField]
    protected bool _isKeyBuilding;
    [SerializeField]
    protected bool _isImportantBuilding;
    [SerializeField]
    [Range(1, 2)]
    /// <summary>Ancho que ocupa el edificio en la manzana.</summary>
    protected int _widthOnBlock;
    [SerializeField]
    [Range(1, 2)]
    ///<summary>Alto que ocupa el edificio en la manzana.</summary>
    protected int _heightOnBlock;
    [SerializeField]
    protected DoorSide _door;

    #endregion

    #region Gets

    public int AmountPerWorld => _amountPerWorld; 
    public int AmountPerCity => _amountPerCity; 
    public bool IsKeyBuilding => _isKeyBuilding;
    public bool IsImportantBuilding => _isImportantBuilding;
    public int WidthOnBlock => _widthOnBlock;
    public int HeightOnBlock => _heightOnBlock;
    public DoorSide Door => _door;

    #endregion
}