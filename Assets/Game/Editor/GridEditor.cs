using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(DrawGrid))]
public class GridEditor : Editor
{
    DrawGrid grid;

    public void OnEnable()
    {
        grid = (DrawGrid)target;
        SceneView.onSceneGUIDelegate = GridUpdate;
    }

    void GridUpdate(SceneView sceneview)
    {
        Event e = Event.current;

        if (e.isKey && e.character == 'a')
        {
            Ray r = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y ));//+ Camera.current.pixelHeight));
            Vector3 mousePos = r.origin;
            if (Selection.activeObject)
            {
                GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(Selection.activeObject);
                obj.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
            }
        }
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(" Grid Width ");
        grid.width = EditorGUILayout.FloatField(grid.width, GUILayout.Width(50));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(" Grid Height ");
        grid.height = EditorGUILayout.FloatField(grid.height, GUILayout.Width(50));
        GUILayout.EndHorizontal();
    }
}
