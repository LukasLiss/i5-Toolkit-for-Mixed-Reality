using Microsoft.Azure.SpatialAnchors.Unity;
using Microsoft.MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[CustomEditor(typeof(AnchorModuleScript))]
public class AnchorModuleScriptEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var AncModScript = target as AnchorModuleScript;

        //EditorGUILayout.BeginHorizontal();
        //GUILayout.Label("Azure Spatial Anchor Connection Settings");
        //AncModScript.UseOwnAnchorManager = GUILayout.Toggle(AncModScript.UseOwnAnchorManager, "Do you want to use a custom ASA Manager?");
        //EditorGUILayout.EndHorizontal();

        if (AncModScript.UseOwnAnchorManager)
        {
            EditorGUILayout.BeginHorizontal();
            AncModScript.ASAManager = EditorGUILayout.ObjectField(AncModScript.ASAManager, typeof(SpatialAnchorManager), true) as SpatialAnchorManager;
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Account Id:");
            AncModScript.ASAAccountId = EditorGUILayout.TextField(AncModScript.ASAAccountId);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Account Key:");
            AncModScript.ASAAccountKey = EditorGUILayout.TextField(AncModScript.ASAAccountKey);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Account Domain:");
            AncModScript.ASAAccountDomain = EditorGUILayout.TextField(AncModScript.ASAAccountDomain);
            EditorGUILayout.EndHorizontal();
        }

        ARAnchorManager arFoundationAnchorManager = GameObject.FindObjectOfType<ARAnchorManager>();
        if(arFoundationAnchorManager == null)
        {
            GUIStyle textStyle = EditorStyles.label;
            textStyle.wordWrap = true;

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("It seems like there is no AR Foundation Anchor Manager in the Scene. In the case that this scene is loaded additionally to a scene with an AR Anchor Manger that is no problem. But if not than you should use the following button to set up the scene correctly.", textStyle);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if(GUILayout.Button("Setup Scene for ASA"))
            {
                //Try to find Mixed Reality Toolkit
                MixedRealityToolkit mrtkInScene = FindObjectOfType<MixedRealityToolkit>();
                if(mrtkInScene != null)
                {
                    mrtkInScene.gameObject.AddComponent<ARAnchorManager>();
                    Debug.Log("AR Anchor Manager added to the Mixed Reality Toolkit object in the scene.");
                }
                else
                {
                    AncModScript.gameObject.AddComponent<ARAnchorManager>();
                    Debug.LogWarning("We were not able to locate the Mixed Reality Toolkit in the Scene. Therefore we added the AR Anchor Manager directly on the object that has the ASA Helper component.");
                }

            }
            EditorGUILayout.EndHorizontal();

        }

    }
}
