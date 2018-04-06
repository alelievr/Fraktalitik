using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class AlwayRefreshScene
{
	static bool	refresh = true;

	[MenuItem("Edit/Always refresh scene view", false, 10)]
	public static void AlwayRefreshSceneMenu()
	{
		refresh = !refresh;
		
		Debug.Log((refresh) ? "Refresh enabled" : "Refresh disabled");
	}

	static AlwayRefreshScene()
	{
		EditorApplication.update += Update;
	}

	static void Update()
	{
		if (refresh)
			SceneView.RepaintAll();
	}
}
