using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PortalScript))]
[CanEditMultipleObjects]
public class PortalScriptEditor : Editor {
    
    SerializedProperty m_TargetProp;
    SerializedProperty m_TargetListProp;

    void OnEnable() {
        m_TargetProp = serializedObject.FindProperty("target");
        m_TargetListProp = serializedObject.FindProperty("targetList");
    }

    public override void OnInspectorGUI() {
        //base.OnInspectorGUI();

        PortalScript portal = (PortalScript)target;

        portal.association = (PortalAssociation)EditorGUILayout.EnumPopup("Association", portal.association);

        // displaye target
        if (portal.association == PortalAssociation.random) {
            // display nothing
        } else if (portal.association == PortalAssociation.onoToOne) {
            // display target
            EditorGUILayout.PropertyField(m_TargetProp, new GUIContent("Target"));
        } else if (portal.association == PortalAssociation.oneToMany) {
            // display list of target
            EditorGUILayout.PropertyField(m_TargetListProp, new GUIContent("Target list"), true);

        }

        serializedObject.ApplyModifiedProperties();
    }
}
