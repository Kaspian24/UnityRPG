using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// With a click of the button you can remove any tree below certain height
/// </summary>
public class RemoveUnderwaterTrees : MonoBehaviour
{
    /// <summary>
    /// Class object
    /// </summary>
    public Terrain terrain;
    /// <summary>
    /// Setting water level
    /// </summary>
    public float waterLevel = 20.0f;

    /// <summary>
    /// Function responsible for removing ceratin trees from the scene
    /// </summary>
    public void RemoveTreesUnderWater()
    {
        TerrainData terrainData = terrain.terrainData;
        TreeInstance[] treeInstances = terrainData.treeInstances;
        List<TreeInstance> updatedTreeInstances = new List<TreeInstance>();

        foreach (TreeInstance treeInstance in treeInstances)
        {
            Vector3 treePosition = Vector3.Scale(treeInstance.position, terrainData.size) + terrain.transform.position;

            if (treePosition.y >= waterLevel)
            {
                updatedTreeInstances.Add(treeInstance);
            }
        }

        terrainData.treeInstances = updatedTreeInstances.ToArray();
    }
}
