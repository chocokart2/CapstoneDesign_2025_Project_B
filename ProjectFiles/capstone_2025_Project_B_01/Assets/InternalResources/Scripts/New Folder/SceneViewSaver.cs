using UnityEditor;
using UnityEngine;

//public class SceneViewSaver : EditorWindow
//{
//    private static Vector3 savedPosition;
//    private static Quaternion savedRotation;

//    [MenuItem("Tools/Scene View/Save View")]
//    private static void SaveSceneView()
//    {
//        var sceneView = SceneView.lastActiveSceneView;
//        if (sceneView != null)
//        {
//            savedPosition = sceneView.camera.transform.position;
//            savedRotation = sceneView.camera.transform.rotation;
//            Debug.Log("Scene 뷰 위치 저장 완료");
//        }
//        else
//        {
//            Debug.LogWarning("활성화된 Scene 뷰가 없습니다.");
//        }
//    }

//    [MenuItem("Tools/Scene View/Load View")]
//    private static void LoadSceneView()
//    {
//        var sceneView = SceneView.lastActiveSceneView;
//        if (sceneView != null)
//        {
//            sceneView.LookAt(savedPosition, savedRotation);
//            Debug.Log("Scene 뷰 위치 복원 완료");
//        }
//        else
//        {
//            Debug.LogWarning("활성화된 Scene 뷰가 없습니다.");
//        }
//    }
//}
