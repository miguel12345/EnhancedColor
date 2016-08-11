using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(EnhancedColorCurves))]
[CanEditMultipleObjects]
public class EnhancedColorCurvesEditor : Editor 
{
	SerializedProperty RedChannelCurve;
	SerializedProperty GreenChannelCurve;
	SerializedProperty BlueChannelCurve;
	SerializedProperty BakedTexture;
	SerializedProperty ShowBeforeAndAfterEffect;
	SerializedProperty UseBakedTexture;

	void OnEnable()
	{
		RedChannelCurve = serializedObject.FindProperty("RedChannelCurve");
		GreenChannelCurve = serializedObject.FindProperty("GreenChannelCurve");
		BlueChannelCurve = serializedObject.FindProperty("BlueChannelCurve");
		BakedTexture = serializedObject.FindProperty("BakedLUTexture");
		ShowBeforeAndAfterEffect = serializedObject.FindProperty("ShowBeforeAndAfterEffect");
		UseBakedTexture = serializedObject.FindProperty("UseBakedTexture");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.PropertyField(UseBakedTexture);

		if (UseBakedTexture.boolValue) {
			EditorGUILayout.PropertyField (BakedTexture);
		} else {
			EditorGUILayout.PropertyField(RedChannelCurve);
			EditorGUILayout.PropertyField(GreenChannelCurve);
			EditorGUILayout.PropertyField(BlueChannelCurve);
		}
		EditorGUILayout.PropertyField(ShowBeforeAndAfterEffect);
		serializedObject.ApplyModifiedProperties();

		if (!UseBakedTexture.boolValue) {
			if (GUILayout.Button("Save LUTexture"))
			{
				EnhancedColorCurves curves = (EnhancedColorCurves) serializedObject.targetObject;
				curves.SaveLUTexture ();
			}
		}
	}
}