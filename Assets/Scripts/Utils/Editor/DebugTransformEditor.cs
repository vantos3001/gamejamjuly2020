using Game.MovingObjects;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace GameGarden.FarmFrenzy.Utils.Editor {
    [CustomEditor(typeof(DebugTransform))]
    public class DebugTransformEditor : UnityEditor.Editor 
    {
        public Vector3 CachedPosition;
        public bool CachePosition;
        
        public override void  OnInspectorGUI() {
            base.OnInspectorGUI();
            var debugTransform = (target as DebugTransform);
            var transform  = debugTransform.transform;
            
            EditorGUILayout.BeginVertical ();
            
            if (GUILayout.Button("left")) {
                MovingObject.GetDirectionByName(MovingObject.Directions.Left, out var delta);
                transform.position += new Vector3(delta.x, delta.y, 0);
                MovingObject.SnapPosition(transform);
                SetDirty();
            }
            
            if (GUILayout.Button("right")) {
                MovingObject.GetDirectionByName(MovingObject.Directions.Right, out var delta);
                transform.position += new Vector3(delta.x, delta.y, 0);
                MovingObject.SnapPosition(transform);
                SetDirty();
            }
            
            if (GUILayout.Button("forward")) {
                MovingObject.GetDirectionByName(MovingObject.Directions.Forward, out var delta);
                transform.position += new Vector3(delta.x, delta.y, 0);
                MovingObject.SnapPosition(transform);
                SetDirty();
            }
            
            if (GUILayout.Button("backward")) {
                MovingObject.GetDirectionByName(MovingObject.Directions.Backward, out var delta);
                transform.position += new Vector3(delta.x, delta.y, 0);
                MovingObject.SnapPosition(transform);
                SetDirty();
            }

            EditorGUILayout.EndVertical();;
        }

        private static void SetDirty() {
            var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage != null)
            {
                EditorSceneManager.MarkSceneDirty(prefabStage.scene);
            }
        }
    }
}
