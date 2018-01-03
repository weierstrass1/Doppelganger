using UnityEditor;

[CustomEditor(typeof(charComponent))]
public class charComponentEditor : Editor
{

    public override void OnInspectorGUI()
    {
        charComponent cc = (charComponent)target;
        
        cc.FlipX = EditorGUILayout.Toggle("Flip X", cc.FlipX);
        cc.FlipY = EditorGUILayout.Toggle("Flip Y", cc.FlipY);

        base.OnInspectorGUI();
        EditorUtility.SetDirty(target);
    }
}
