using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


/* * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * 
 * Abstract: Editor tool for the 3D tiles.
 * 
 * 
 * * * * * * */


#region TileMap Editor Window GUI

public class TilemapEditorWindow : EditorWindow
{
    // Fields //
    public int brushSize = 1;
    public Vector3Int selectionCoords = new Vector3Int(0, 0, 0);
    public TileMap activeTileMap;    
    public TileData data;


    // Methods // 
    [MenuItem("Window/3D Tilemap Editor")]
    public static void ShowWindow()
    {
        GetWindow<TilemapEditorWindow>("3D Tilemap Editor");
    } // end method ShowWindow()

    void OnGUI()
    {
        RenderTilemapEditorWindow();        
    } // end method OnGUI()
    

    void RenderTilemapEditorWindow()
    {
        GUILayout.BeginArea(new Rect(10, 10, 200, 200));
        // Data
        GUILayout.BeginHorizontal();
        GUILayout.Label("Data:", EditorStyles.boldLabel);        
        data = EditorGUILayout.ObjectField(data, typeof(TileData), false) as TileData;
        GUILayout.EndHorizontal();

        // Brush
        GUILayout.BeginHorizontal();
        GUILayout.Label("Active TileMap:", EditorStyles.boldLabel);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        
        brushSize = (int)GUILayout.HorizontalSlider(brushSize, 1, 4);
        GUILayout.Label(brushSize.ToString());
        GUILayout.EndHorizontal();

        // TEST AREA
        SpawnBlockButtonModule();


        GUILayout.EndArea();
    } // end method RenderLayout

    public void SpawnBlockButtonModule()
    {                
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();
        GUILayout.Label("X");
        selectionCoords.x = int.Parse(EditorGUILayout.TextField(selectionCoords.x.ToString()));
        GUILayout.EndVertical();

        GUILayout.BeginVertical();
        GUILayout.Label("Y");
        selectionCoords.y = int.Parse(EditorGUILayout.TextField(selectionCoords.y.ToString()));
        GUILayout.EndVertical();

        GUILayout.BeginVertical();
        GUILayout.Label("Z");
        selectionCoords.z = int.Parse(EditorGUILayout.TextField(selectionCoords.z.ToString()));
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Spawn Block"))
        {
            AssignDefaultTileData();
            TileMapUtility.TestSpawnBlock(selectionCoords, data);
        } // end if
        if(GUILayout.Button("Delete All Blocks"))
        {
            TileMapUtility.DeleteAllBlocks();
        } // end if
    } // end method SpawnBlockButtonModule()

    public void AssignDefaultTileData()
    {
        if (data == null) data = TileMapUtility.LoadDefaultData();
    } // end method AssignDefaultTileData()

    #endregion
} // end class TileMapEditorWindow

