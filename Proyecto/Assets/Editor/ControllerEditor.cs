using UnityEditor;

[CustomEditor(typeof(CharacterController),true)]
public class ControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CharacterController controller = (CharacterController)target;
        controller.State = EditorGUILayout.IntField("State", controller.State);
        base.OnInspectorGUI();
        EditorUtility.SetDirty(target);
    }
}
