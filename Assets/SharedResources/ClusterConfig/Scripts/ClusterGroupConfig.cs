using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class ImacGroupId
{
	public string	ip;
	public int		groupIndex;

	public ImacGroupId(string ip, int index)
	{
		this.ip = ip;
		this.groupIndex = index;
	}
}

public class ClusterGroupConfig : ScriptableObject
{
	[SerializeField]
	public List< ClusterGroupInfo >	clusterGroups = new List< ClusterGroupInfo >();

	[SerializeField]
	public List< ImacGroupId >		iMacGroups = new List< ImacGroupId >();

	private void OnEnable()
	{
		Debug.Log("Imac coutn: " + iMacGroups.Count);
	}
}
