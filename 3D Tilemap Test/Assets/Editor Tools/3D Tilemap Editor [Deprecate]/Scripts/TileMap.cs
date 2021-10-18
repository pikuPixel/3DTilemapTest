using System.Collections.Generic;
using UnityEngine;

/* * * * * * * * * *
 * 
 *  Abstract: A Layer's purpose should basically be to hold TileObjects. It should know every TileObject that is within it, as well as where the layer itself is on the Y axis.
 *  
 *  ///////////////////////////
 *  After a lot of banging my head against the wall pretending I know how to code, I've decided
 *  TileMap will essentially be the center of everything.
 *  
 *  A GameObject "TileMap" will have children objects, the "Layers." 
 *  
 * * * */


public class TileMap : MonoBehaviour
{        
    public Dictionary<int, TileObject> _layer; // int being the Layer's position in its parent TileMap.
}