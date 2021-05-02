using UnityEngine;
using UnityEditor;
// using DG.Tweening;
using System.Collections.Generic;
using System.IO;
using nl.ypmits.gametools.editor;


namespace nl.ypmits.gametools.tools
{
    [CustomEditor(typeof(Visualizer)), CanEditMultipleObjects]
    public class VisualizerEditor : Editor
    {
        // private int indent;
        
        // // private bool togglePos3D = true;
        // private bool togglePosUI;
        // private bool showPositionShow;
        // private bool showPositionHide;

        // private bool toggleRotation;
        // private bool showRotationShow;
        // private bool showRotationHide;

        // private bool toggleScale;
        // private bool showScaleShow;
        // private bool showScaleHide;

        // private bool toggleTransparency;
        // private bool showTransparencyShow;
        // private bool showTransparencyHide;

        // private bool toggleEtc;

        // private SerializedProperty selectedPositionType;
        // private SerializedProperty useCurrentAsShowAmount;
        // private SerializedProperty autoShow;
        // private SerializedProperty autoHide;
        // private SerializedProperty debugOutput;
        // private SerializedProperty disableWhenHiding;

        // Visualizer t;


        // void OnEnable() {
        //     selectedPositionType = serializedObject.FindProperty("selectedPositionType");
        //     useCurrentAsShowAmount = serializedObject.FindProperty("useCurrentAsShowAmount");
        //     autoShow = serializedObject.FindProperty("autoShow");
        //     autoHide = serializedObject.FindProperty("autoHide");
        //     debugOutput = serializedObject.FindProperty("debugOutput");
        //     disableWhenHiding = serializedObject.FindProperty("disableWhenHiding");
        // }


        // public override void OnInspectorGUI()
        // {
        //     t = (Visualizer)target;
        //     indent = EditorGUI.indentLevel;
        //     //
        //     //   ______               __ __   __              
        //     //  |   __ \.-----.-----.|__|  |_|__|.-----.-----.
        //     //  |    __/|  _  |__ --||  |   _|  ||  _  |     |
        //     //  |___|   |_____|_____||__|____|__||_____|__|__|
        //     //
        //     // Font: http://patorjk.com/software/taag/#p=display&c=c%2B%2B&f=Chunky
        //     //
        //     GUIStyle st = new GUIStyle(EditorStyles.miniButton);
        //     EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        //         selectedPositionType.intValue = GUILayout.SelectionGrid(selectedPositionType.intValue,new string[3]{"None", "Position (3D)", "Position (2D)"},3,"toggle");
        //         if(selectedPositionType.intValue == 1 || selectedPositionType.intValue == 2) {
        //             GUILayout.BeginHorizontal();
        //         }
        //         if (selectedPositionType.intValue == 1)
        //         {
        //             t.showPosition = EditorGUILayout.Vector3Field("Show Position", t.showPosition);
        //             if (GUILayout.Button("Cur", st)) t.showPosition = t.transform.localPosition;
        //             if (GUILayout.Button("Set", st)) t.transform.localPosition = t.showPosition;
        //             GUILayout.EndHorizontal();
        //             GUILayout.BeginHorizontal();
        //             t.hidePosition = EditorGUILayout.Vector3Field("Hide Position", t.hidePosition);
        //             if (GUILayout.Button("Cur", st)) t.hidePosition = t.transform.localPosition;
        //             if (GUILayout.Button("Set", st)) t.transform.localPosition = t.hidePosition;
        //         }
        //         else if( selectedPositionType.intValue == 2 )
        //         {
        //             t.showPosition2D = EditorGUILayout.Vector3Field("Show PositionUI", t.showPosition2D);
        //             if (GUILayout.Button("Cur", st)) t.showPosition2D = t.rectTransform.anchoredPosition3D;
        //             if (GUILayout.Button("Set", st)) t.rectTransform.anchoredPosition3D = t.showPosition2D;
        //             GUILayout.EndHorizontal();
        //             GUILayout.BeginHorizontal();
        //             t.hidePosition2D = EditorGUILayout.Vector3Field("Hide PositionUI", t.hidePosition2D);
        //             if (GUILayout.Button("Cur", st)) t.hidePosition2D = t.rectTransform.anchoredPosition3D;
        //             if (GUILayout.Button("Set", st)) t.rectTransform.anchoredPosition3D = t.hidePosition2D;
        //         }
        //         if(selectedPositionType.intValue == 1 || selectedPositionType.intValue == 2) {
        //             GUILayout.EndHorizontal();
        //             EditorGUI.indentLevel = indent;
        //             BuildShowHideParameters($"Show Vars", $"dur:{t.showPositionDuration} del:{t.showPositionDelay} e:{t.showPositionEase}", 1, ref showPositionShow, "Show-duration", ref t.showPositionDuration, "Show-delay", ref t.showPositionDelay, "Show-ease", ref t.showPositionEase, "showPositionCompleteAction");
        //             BuildShowHideParameters("Hide Vars", $"dur:{t.hidePositionDuration} del:{t.hidePositionDelay} e:{t.hidePositionEase}", 1, ref showPositionHide, "Hide-duration", ref t.hidePositionDuration, "Hide-delay", ref t.hidePositionDelay, "Hide-ease", ref t.hidePositionEase, "hidePositionCompleteAction");
        //         }
        //         t.usePosition = (selectedPositionType.intValue == 0) ? false : true;
        //     EditorGUILayout.EndVertical();


        //     //
        //     //   ______         __          __   __              
        //     //  |   __ \.-----.|  |_.---.-.|  |_|__|.-----.-----.
        //     //  |      <|  _  ||   _|  _  ||   _|  ||  _  |     |
        //     //  |___|__||_____||____|___._||____|__||_____|__|__|
        //     //
        //     // Font: http://patorjk.com/software/taag/#p=display&c=c%2B%2B&f=Chunky
        //     //
        //     EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        //     EditorGUI.indentLevel = indent;
        //     if (EditorUtils.ClickableHeader("Rotation", "", ref t.useRotation, EditorStyles.toggle))
        //     {
        //         GUILayout.BeginHorizontal();
        //         t.showRotation = EditorGUILayout.Vector3Field("Show-rotation", t.showRotation);
        //         if (GUILayout.Button("Current", GUILayout.Width(70))) t.showRotation = t.transform.localRotation.eulerAngles;
        //         GUILayout.EndHorizontal();
        //         GUILayout.BeginHorizontal();
        //         t.hideRotation = EditorGUILayout.Vector3Field("Hide-rotation", t.hideRotation);
        //         if (GUILayout.Button("Current", GUILayout.Width(70))) t.hideRotation = t.transform.localRotation.eulerAngles;
        //         GUILayout.EndHorizontal();

        //         // Show/Hide stuff:
        //         BuildShowHideParameters("Show Vars", $"dur:{t.showRotationDuration} del:{t.showRotationDelay} e:{t.showRotationEase}", 1, ref showRotationShow, "Show-duration", ref t.showRotationDuration, "Show-delay", ref t.showRotationDelay, "Show-ease", ref t.showRotationEase, "showRotationCompleteAction");
        //         BuildShowHideParameters("Hide Vars", $"dur:{t.hideRotationDuration} del:{t.hideRotationDelay} e:{t.hideRotationEase}", 1, ref showRotationHide, "Hide-duration", ref t.hideRotationDuration, "Hide-delay", ref t.hideRotationDelay, "Hide-ease", ref t.hideRotationEase, "hideRotationCompleteAction");
        //     }
        //     EditorGUILayout.EndVertical();

        //     //
        //     //   _______              __        
        //     //  |     __|.----.---.-.|  |.-----.
        //     //  |__     ||  __|  _  ||  ||  -__|
        //     //  |_______||____|___._||__||_____|
        //     //
        //     // Font: http://patorjk.com/software/taag/#p=display&c=c%2B%2B&f=Chunky
        //     //
        //     EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        //     if (EditorUtils.ClickableHeader("Scale", "", ref t.useScale, EditorStyles.toggle))
        //     {
        //         GUILayout.BeginHorizontal();
        //         t.showScale = EditorGUILayout.Vector3Field("Show-scale", t.showScale);
        //         if (GUILayout.Button("Current", GUILayout.Width(70))) t.showScale = t.transform.localScale;
        //         GUILayout.EndHorizontal();
        //         GUILayout.BeginHorizontal();
        //         t.hideScale = EditorGUILayout.Vector3Field("Hide-scale", t.hideScale);
        //         if (GUILayout.Button("Current", GUILayout.Width(70))) t.hideScale = t.transform.localScale;
        //         GUILayout.EndHorizontal();

        //         // Show/Hide stuff:
                
        //         BuildShowHideParameters("Show Vars", $"dur:{t.showScaleDuration} del:{t.showScaleDelay} e:{t.showScaleEase}", 1, ref showScaleShow, "Show-duration", ref t.showScaleDuration, "Show-delay", ref t.showScaleDelay, "Show-ease", ref t.showScaleEase, "showScaleCompleteAction");
        //         BuildShowHideParameters("Hide Vars", $"dur:{t.hideScaleDuration} del:{t.hideScaleDelay} e:{t.hideScaleEase}", 1, ref showScaleHide, "Hide-duration", ref t.hideScaleDuration, "Hide-delay", ref t.hideScaleDelay, "Hide-ease", ref t.hideScaleEase, "hideScaleCompleteAction");
        //     }
        //     EditorGUILayout.EndVertical();

        //     //
        //     //   _______                                                                 
        //     //  |_     _|.----.---.-.-----.-----.-----.---.-.----.-----.-----.----.--.--.
        //     //    |   |  |   _|  _  |     |__ --|  _  |  _  |   _|  -__|     |  __|  |  |
        //     //    |___|  |__| |___._|__|__|_____|   __|___._|__| |_____|__|__|____|___  |
        //     //                                  |__|                              |_____|
        //     //
        //     // Font: http://patorjk.com/software/taag/#p=display&c=c%2B%2B&f=Chunky
        //     //
        //     EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        //     if (EditorUtils.ClickableHeader("Transparency (Renderer)", "", ref t.useTransparency, EditorStyles.toggle))
        //     {
        //         // EditorGUI.indentLevel = indent + 1;
        //         t.showFadeAmount = EditorGUILayout.FloatField("show-fadeAmount", t.showFadeAmount);
        //         t.hideFadeAmount = EditorGUILayout.FloatField("Hide-fadeAmount", t.hideFadeAmount);

        //         // Show/Hide stuff:
        //         BuildShowHideParameters("Show Vars", $"dur:{t.showTransparencyDuration} del:{t.showTransparencyDelay} e:{t.showTransparencyEase}", 1, ref showTransparencyShow, "Show-duration", ref t.showTransparencyDuration, "Show-delay", ref t.showTransparencyDelay, "Show-ease", ref t.showTransparencyEase, "showTransparencyCompleteAction");
        //         BuildShowHideParameters("Hide Vars", $"dur:{t.hideTransparencyDuration} del:{t.hideTransparencyDelay} e:{t.hideTransparencyEase}", 1, ref showTransparencyHide, "Hide-duration", ref t.hideTransparencyDuration, "Hide-delay", ref t.hideTransparencyDelay, "Hide-ease", ref t.hideTransparencyEase, "hideTransparencyCompleteAction");
        //     }
        //     EditorGUILayout.EndVertical();

        //     EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        //     EditorGUI.indentLevel = indent;
        //     EditorGUILayout.PropertyField(useCurrentAsShowAmount);
        //     EditorGUILayout.PropertyField(autoShow);
        //     EditorGUILayout.PropertyField(autoHide);
        //     EditorGUILayout.PropertyField(disableWhenHiding);
        //     EditorGUILayout.PropertyField(debugOutput);
        //     EditorGUI.indentLevel = indent;
        //     EditorGUILayout.EndVertical();

        //     serializedObject.ApplyModifiedProperties();
        // }


        // public void BuildShowHideParameters(string header, string extraInfo, int indent, ref bool toggleBool, string durationString, ref float duration, string delayString, ref float delay, string easeString, ref Ease ease, string completeAction)
        // {
        //     int oldIndent = EditorGUI.indentLevel;
        //     EditorGUI.indentLevel = oldIndent + indent;
        //     if (EditorUtils.ClickableHeader(header, extraInfo, ref toggleBool, EditorStyles.foldout))
        //     {
        //         EditorGUI.indentLevel = oldIndent + indent + 1;
        //         duration = EditorGUILayout.FloatField(durationString, duration);
        //         delay = EditorGUILayout.FloatField(delayString, delay);
        //         ease = (Ease)EditorGUILayout.EnumPopup(easeString, ease);
        //         EditorGUILayout.PropertyField(serializedObject.FindProperty(completeAction));
        //     }
        //     EditorGUI.indentLevel = oldIndent;
        // }
    }
}