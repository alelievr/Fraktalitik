using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(ClusterGroupConfig))]
public class ClusterGroupConfigEditor : Editor
{
	ClusterGroupConfig		config;

	ReorderableList			groupConfigList;

	[MenuItem("Assets/Create/Cluster Config")]
	public static void CreateClusterConfig()
	{
		var cc = ScriptableObject.CreateInstance< ClusterGroupConfig >();
		Object obj = Selection.activeObject;
		string path = "";

		if (obj == null)
			path = "Assets";
		else
			path = AssetDatabase.GetAssetPath(obj.GetInstanceID());

		ProjectWindowUtil.CreateAsset(cc, path + "/Cluster Config.asset");
	}

	private void OnEnable()
	{
		config = target as ClusterGroupConfig;

		InitClusterGroupList();

		if (config.iMacGroups.Count == 0)
		{
			foreach (var ip in Cluster.GetIps())
			{
				config.iMacGroups.Add(new ImacGroupId(ip, 0));
			}
		}
	}

	void InitClusterGroupList()
	{
		groupConfigList = new ReorderableList(config.clusterGroups, typeof(ClusterGroupConfig));

		groupConfigList.drawHeaderCallback = (r) => {
			EditorGUI.LabelField(r, "Cluster groups");
		};

		groupConfigList.elementHeight = (EditorGUIUtility.singleLineHeight + 2) * 4;

		groupConfigList.drawElementCallback = (rect, index, active, selected) => {
			var group = config.clusterGroups[index];

			rect.height = EditorGUIUtility.singleLineHeight;

			group.name = EditorGUI.TextField(rect, group.name);
			rect.y += EditorGUIUtility.singleLineHeight + 2;
			group.color = EditorGUI.ColorField(rect, "group color", group.color);
			rect.y += EditorGUIUtility.singleLineHeight + 2;
			group.sceneName = EditorGUI.TextField(rect, "Scene name", group.sceneName);
			rect.y += EditorGUIUtility.singleLineHeight + 2;
			group.syncDelay = EditorGUI.FloatField(rect, "Sync delay", group.syncDelay);
		};
	}

	public override void OnInspectorGUI()
	{
		EditorGUI.BeginChangeCheck();

		groupConfigList.DoLayoutList();

		float	clusterHeight = 300;

		float	iMacCellHeight = clusterHeight / Cluster.rowCount;
		float	iMacCellWidth = 20;

		Rect r = EditorGUILayout.GetControlRect(false, clusterHeight, GUILayout.ExpandWidth(true));

		GUIStyle style = new GUIStyle(EditorStyles.textField);
		style.fontSize = 22;

		foreach (var iMacGroup in config.iMacGroups)
		{
			var iMacInfo = Cluster.iMacInfosByIp[iMacGroup.ip];
			Vector3 iMacPos = iMacInfo.worldPosition;
			Rect iMacRect = r;
			Color c = Color.grey;

			iMacPos.z += (iMacInfo.faceEntrance) ? .2f : -.2f;

			iMacRect.position += new Vector2(iMacPos.x * 10, iMacPos.z * 25);
			iMacRect.size = new Vector2(iMacCellWidth, iMacCellHeight);

			if (iMacGroup.groupIndex < config.clusterGroups.Count)
				c = config.clusterGroups[iMacGroup.groupIndex].color;

			style.normal.textColor = c;

			iMacGroup.groupIndex = EditorGUI.IntField(iMacRect, iMacGroup.groupIndex, style);
		}

		if (EditorGUI.EndChangeCheck())
			EditorUtility.SetDirty(config);
	}
}
