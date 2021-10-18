using UnityEngine;
using System.Collections.Generic;

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 *  Abstract: Create a 3D tile that will change its mesh, and XZ orientation, based on its 4 neighbors. North (+Z), South (-Z), East (+X), and West (-X).
 *            The tile should thus seamlessly create a 2.5D environment using a single base tile.
 *            
 *            Eventually, I would like to create a "brush" tool that will paint these tiles onto a grid (layer object?). Not worried about that now, but
 *            keep in mind to make sure this is compatible with any future tool like that.
 *            
 *            Note: Make sure its neighbors are not affected by its own orientation. A "North" neighbor should still be "North," even if a tile is rotated.
 *            Should be intuitively handled by using grid as reference, but thing to keep in mind.
 *            
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 *  Pseudocoding:
 *  
 *  "Create a Tile":
 *      ScriptableObject? Prefab with script? idk. "TileObject"
 *  
 *  "Change its Mesh":
 *  
 *          6 Possible Combinations. 1 neighbor, 2 neighbor (L shape or | shape variations), 3 neighbor, or 4 neighbor.
 *          Possibly drop prefab itself and rotate it where necessary.
 *          
 *          
 *          
 *          
 *  Combination Rules:
 *          Create int "count" , and store adjacentNeighbors.count into that variable. Maybe debug.Log this number. Should logically never exceed 4.
 *          
 *          Use switch(count) to determine mesh.
 *              case 0: 0Neighbor mesh
 *              case 1: 1Neighbor mesh
 *              case 3: 3Neighbor mesh
 *              case 2: check if forms 90 degree angle with neighbors. If so, use elbow mesh. else, use straight mesh. (? operator)
 *              
 *          
 *  Rotate TileObject accordingly:
 *      
 *              
 * * * */

public class TileObject : MonoBehaviour
{
    Dictionary<Vector3, TileObject> neighbors;
}

public enum TileNeighbors
{ 
    NoNeighbors, OneNeighbors, TwoNeighborsElbow, TwoNeighborsCave, ThreeNeighbors, FourNeighbors
}

