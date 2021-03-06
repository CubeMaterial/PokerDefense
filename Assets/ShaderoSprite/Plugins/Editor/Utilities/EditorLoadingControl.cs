#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
#if UNITY_5_3_OR_NEWER || UNITY_5_3
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
#endif
using System;

namespace _ShaderoShaderEditorFramework.Utilities
{
	[InitializeOnLoad]
	public static class EditorLoadingControl 
	{
#if UNITY_5_3_OR_NEWER || UNITY_5_3
		private static Scene loadedScene;
#else
		private static string loadedScene;
#endif

		private static bool serializationTest = false;
		private static bool playmodeSwitchToEdit = false;
		private static bool toggleLateEnteredPlaymode = false;

		public static Action beforeEnteringPlayMode;
		public static Action justEnteredPlayMode;
		public static Action lateEnteredPlayMode;
		public static Action beforeLeavingPlayMode;
		public static Action justLeftPlayMode;
		public static Action justOpenedNewScene;

		static EditorLoadingControl () 
		{
#if !UNITY_2017_2_OR_NEWER
            EditorApplication.playmodeStateChanged -= PlaymodeStateChanged;
            EditorApplication.playmodeStateChanged += PlaymodeStateChanged;
#else
            EditorApplication.playModeStateChanged -= PlaymodeStateChanged;
			EditorApplication.playModeStateChanged += PlaymodeStateChanged;
#endif
            EditorApplication.update -= Update;
            EditorApplication.update += Update;
#if !UNITY_2018_1_OR_NEWER
            EditorApplication.hierarchyWindowChanged -= OnHierarchyChange;
            EditorApplication.hierarchyWindowChanged += OnHierarchyChange;
#else
            EditorApplication.hierarchyChanged -= OnHierarchyChange;
			EditorApplication.hierarchyChanged += OnHierarchyChange;

#endif
        }

        private static void OnHierarchyChange () 
		{ 
#if UNITY_5_3_OR_NEWER || UNITY_5_3
			Scene currentScene = EditorSceneManager.GetActiveScene ();
#else
			string currentScene = Application.loadedLevelName;
#endif
			if (loadedScene != currentScene)
			{
				if (justOpenedNewScene != null)
					justOpenedNewScene.Invoke ();
				loadedScene = currentScene;
			}
		}

		private static void Update () 
		{
			if (toggleLateEnteredPlaymode)
			{
				toggleLateEnteredPlaymode = false;
				if (lateEnteredPlayMode != null)
					lateEnteredPlayMode.Invoke ();
			}
			serializationTest = true;
		}
#if (UNITY_5_6 || UNITY_2017_1)
        private static void PlaymodeStateChanged ()
#else
       private static void PlaymodeStateChanged (PlayModeStateChange state)
#endif
        {
            if (!Application.isPlaying)
			{ 
				if (playmodeSwitchToEdit)
				{if (justLeftPlayMode != null)
						justLeftPlayMode.Invoke ();
					playmodeSwitchToEdit = false;
				}
				else 
				{ if (beforeEnteringPlayMode != null)
						beforeEnteringPlayMode.Invoke ();
				}
			}
			else
			{
				if (serializationTest) 
				{ if (beforeLeavingPlayMode != null)
						beforeLeavingPlayMode.Invoke ();
					playmodeSwitchToEdit = true;
				}
				else
				{ 	if (justEnteredPlayMode != null)
						justEnteredPlayMode.Invoke ();
					toggleLateEnteredPlaymode = true;
				}

			}
		}
	}
}
#endif