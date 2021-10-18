using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class TileMapUtility
{
    /* * *
     * Following 2 "methods" (not C# method, literal definition of method) are ideas for how to use CODE to create the 3D tile.
     * 
     * Prefab seems to be the way to go.
     * * */
    /////////////
    // New object method
    //
    // Material tempMat = AssetDatabase.LoadAssetAtPath("Assets/Graphics/Materials/GrassblockMaterial.mat", typeof(Material)) as Material;
    // Mesh tempMesh = AssetDatabase.LoadAssetAtPath("Assets/Graphics/Meshes/0N.asset", typeof(Mesh)) as Mesh;
    //
    // GameObject obj = ObjectFactory.CreateGameObject("3D Tile");
    // var mf = obj.AddComponent<MeshFilter>();
    // var mr = obj.AddComponent<MeshRenderer>();
    //
    // mf.mesh = tempMesh;
    // mr.material = tempMat;
    //
    /////////////
    // Prefab method
    // Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/3D Tiles/TileObject.prefab", typeof(GameObject));
    //
    // GameObject clone = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;

    /* * * * * * *
     * 
     * ABSTRACT: This class handles the messy algorithm stuff as to not messify the TileMapEditor.
     * 
     * * */


    private static TileData _data;
    private static Dictionary<Vector3Int, GameObject> _tileObjects = new Dictionary<Vector3Int, GameObject>();

    public static void TestSpawnBlock(Vector3Int spawnCoords, TileData data)
    {
        if (data == null) _data = LoadDefaultData();
        else _data = data;


        Vector3 realCoords = GetRealCoords(spawnCoords);
        GameObject obj = ObjectFactory.CreateGameObject("3D Tile");
        MeshFilter mf = obj.AddComponent<MeshFilter>();
        MeshRenderer mr = obj.AddComponent<MeshRenderer>();
        obj.AddComponent<TileObject>();
        _tileObjects.Add(spawnCoords, obj);

        mf.mesh = _data.meshes[0];
        mr.material = _data.material;
        obj.transform.position = realCoords;
    } // end method TestSpawnBlock()

    // Adjust coordinates so the integer values given by user can accurately place blocks in the scene.
    public static Vector3 GetRealCoords(Vector3 rawCoords)
    {
        rawCoords.x += 0.5f;
        rawCoords.y += 0.5f;
        rawCoords.z += 0.5f;

        return rawCoords;
    } // end method rawCoordsToRealCoords

    public static TileData LoadDefaultData()
    {
        TileData data = AssetDatabase.LoadAssetAtPath("Assets/Editor Tools/3D Tilemap Editor/Scripts/TileData.cs", typeof(TileData)) as TileData;

        if (data == null) data = CreateDefaultData();

        return data;
    } // end method loadDefaultData

    private static void UpdateTileBlock()
    {

    } // end method UpdateTileBlock()

    
    internal static void DeleteAllBlocks()
    {
        foreach (var entry in _tileObjects)
        {
            var value = entry.Value;

            Debug.Log("Sending " + value.name + " to Jesus. Goodbye!");

            GameObject.DestroyImmediate(value);
        }

        _tileObjects.Clear();
    } // end method DeleteAllBlocks()

    private static TileData CreateDefaultData()
    {
        AssetDatabase.CreateAsset(TileData.CreateInstance("TileData"), "Assets/Editor Tools/3D Tilemap Editor/Data/Default TileData.asset");
        TileData data = AssetDatabase.LoadAssetAtPath("Assets/Editor Tools/3D Tilemap Editor/Data/Default TileData.asset", typeof(TileData)) as TileData;

        data.meshes[(int)TileNeighbors.NoNeighbors] = AssetDatabase.LoadAssetAtPath("Assets/Graphics/Meshes/0N.asset", typeof(Mesh)) as Mesh;
        data.meshes[(int)TileNeighbors.OneNeighbors] = AssetDatabase.LoadAssetAtPath("Assets/Graphics/Meshes/1N.asset", typeof(Mesh)) as Mesh;
        data.meshes[(int)TileNeighbors.TwoNeighborsElbow] = AssetDatabase.LoadAssetAtPath("Assets/Graphics/Meshes/2NElbow.asset", typeof(Mesh)) as Mesh;
        data.meshes[(int)TileNeighbors.TwoNeighborsCave] = AssetDatabase.LoadAssetAtPath("Assets/Graphics/Meshes/2NCave.asset", typeof(Mesh)) as Mesh;
        data.meshes[(int)TileNeighbors.ThreeNeighbors] = AssetDatabase.LoadAssetAtPath("Assets/Graphics/Meshes/3N.asset", typeof(Mesh)) as Mesh;
        data.meshes[(int)TileNeighbors.FourNeighbors] = AssetDatabase.LoadAssetAtPath("Assets/Graphics/Meshes/4N.asset", typeof(Mesh)) as Mesh;

        data.material = AssetDatabase.LoadAssetAtPath("Assets/Graphics/Materials/Block_GrassTexture.mat", typeof(Material)) as Material;

        return data;
    } // end method createDefaultData()

    #region test space

    public static void DebugScreenMouseoverLog()
    {
        Event e = Event.current;

        if (e != null)
        {
            Vector2 mousePos = e.mousePosition;
            Vector3 mouseoverPoint = ScreenToLocal(SceneView.currentDrawingSceneView.camera.transform, mousePos);

            Debug.Log(mouseoverPoint);
        }
        else Debug.Log("Oh no.");
    } // end method DebugScreenMouseover()

    public static Vector3 ScreenToLocal(Transform transform, Vector2 screenPosition)
    {
        return ScreenToLocal(transform, screenPosition, new Plane(transform.up * -1f, transform.position));
    } // end method ScreenToLocal(Transform, Vector2)

    public static Vector3 ScreenToLocal(Transform transform, Vector2 screenPosition, Plane plane)
    {
        Ray ray = HandleUtility.GUIPointToWorldRay(screenPosition);
        Debug.DrawLine(ray.origin, ray.direction);
        float result;
        plane.Raycast(ray, out result);
        Vector3 world = ray.GetPoint(result);
        return transform.InverseTransformPoint(world);
    } // end method ScreenToLocal(Transform, Vector2, Plane)



    /* Reference
    public static Vector3 ScreenToLocal(Transform transform, Vector2 screenPosition)
    {
        return ScreenToLocal(transform, screenPosition, new Plane(transform.forward * -1f, transform.position));
    }

    public static Vector3 ScreenToLocal(Transform transform, Vector2 screenPosition, Plane plane)
    {
        Ray ray;
        if (Camera.current.orthographic)
        {
            Vector2 screen = EditorGUIUtility.PointsToPixels(GUIClip.Unclip(screenPosition));
            screen.y = Screen.height - screen.y;
            Vector3 cameraWorldPoint = Camera.current.ScreenToWorldPoint(screen);
            ray = new Ray(cameraWorldPoint, Camera.current.transform.forward);
        }
        else
        {
            ray = HandleUtility.GUIPointToWorldRay(screenPosition);
        }

        float result;
        plane.Raycast(ray, out result);
        Vector3 world = ray.GetPoint(result);
        return transform.InverseTransformPoint(world);
    }
    */
    #endregion
} // end class TileMapUtility
