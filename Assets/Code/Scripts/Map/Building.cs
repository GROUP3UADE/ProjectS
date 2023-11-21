using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MapNode
{
    [SerializeField]
    public BuildingMapNodeSO Settings;

    public int TotalTileSize;

    private void Awake()
    {
        TotalTileSize = Settings.WidthOnBlock + Settings.HeightOnBlock;
    }
}
