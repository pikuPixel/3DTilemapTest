using UnityEngine;
using UnityEditor;


/* * * * * * * *
 * 
 * Using this as reference: https://youtu.be/kynWOuv9FLE
 * Thank you, Allen Devs!
 * 
 * * * * */
[CustomEditor(typeof(TileMap))]
public class TileMapInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

    }

    private void OnSceneGUI()
    {
        //TileMapUtility.DebugScreenMouseoverLog();
    }
}
