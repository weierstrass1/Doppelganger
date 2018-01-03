using UnityEditor;

[CustomEditor(typeof(MobileObject),true)]
public class mobileObjectEditor : Editor
{

    public override void OnInspectorGUI()
    {
        MobileObject mo = (MobileObject)target;
        base.OnInspectorGUI();

        EditorGUILayout.LabelField("Physics");

        EditorGUI.indentLevel++;

        mo.Solid = EditorGUILayout.Toggle("Interact With Layers", mo.Solid);
        EditorGUILayout.Toggle("Blocked Below", mo.BlockedFromBelow);
        EditorGUILayout.Toggle("Blocked Above", mo.BlockedFromAbove);
        EditorGUILayout.Toggle("Blocked Right", mo.BlockedFromRight);
        EditorGUILayout.Toggle("Blocked Left", mo.BlockedFromLeft);

        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Terrain Angle: " + mo.TerrainAngle);

        EditorGUILayout.Separator();

        mo.XSpeed = EditorGUILayout.FloatField("X Speed", mo.XSpeed);
        mo.YSpeed = EditorGUILayout.FloatField("Y Speed", mo.YSpeed);

        EditorGUILayout.Separator();

        mo.XAccel = EditorGUILayout.FloatField("X Acceleration", mo.XAccel);
        mo.YAccel = EditorGUILayout.FloatField("Y Acceleration", mo.YAccel);

        EditorGUILayout.Separator();

        mo.MinXSpeed = EditorGUILayout.FloatField("Min X Speed", mo.MinXSpeed);
        mo.MinYSpeed = EditorGUILayout.FloatField("Min Y Speed", mo.MinYSpeed);

        EditorGUILayout.Separator();

        mo.MaxXSpeed = EditorGUILayout.FloatField("Max X Speed", mo.MaxXSpeed);
        mo.MaxYSpeed = EditorGUILayout.FloatField("Max Y Speed", mo.MaxYSpeed);

        EditorGUILayout.Separator();

        mo.XGravity = EditorGUILayout.FloatField("X Gravity", mo.XGravity);
        mo.YGravity = EditorGUILayout.FloatField("Y Gravity", mo.YGravity);

        EditorGUILayout.Separator();

        mo.XFriction = EditorGUILayout.FloatField("X Friction", mo.XFriction);
        mo.YFriction = EditorGUILayout.FloatField("Y Friction", mo.YFriction);

        EditorGUILayout.Separator();

        mo.Mass = EditorGUILayout.IntField("Mass", mo.Mass);

        EditorGUI.indentLevel--;

        EditorUtility.SetDirty(target);
    }
}