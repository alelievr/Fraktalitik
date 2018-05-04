using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterGUI : MonoBehaviour
{
	public GameObject		clusterSeatPrefab;

	List< ClusterIMacGUI >	clusterImacs = new List< ClusterIMacGUI >();

	Dictionary< ClientStatus, Color >	clientStatusColors = new Dictionary< ClientStatus, Color >()
	{
		{ClientStatus.Unknown, Colors.gray70},
		{ClientStatus.Disconnected, Colors.red2},
		{ClientStatus.WaitingForGroup, Colors.orange},
		{ClientStatus.ConnectedToGroup, Colors.green2},
		{ClientStatus.GroupServer, Colors.purple1},
	};

	Dictionary< string, ClusterIMacGUI >	iMacs = new Dictionary< string, ClusterIMacGUI >();

	public static ClusterGUI		instance;

	private void Awake()
	{
		instance = this;
	}

	void Start ()
	{
		foreach (var iMac in Cluster.iMacInfos.Values)
		{
			var go = GameObject.Instantiate(clusterSeatPrefab, transform);
			var imacGUI = go.GetComponent< ClusterIMacGUI >();

			imacGUI.SetText(iMac.name);

			go.transform.position = iMac.worldPosition;

			clusterImacs.Add(imacGUI);
			iMacs[iMac.ip] = imacGUI;
		}

		StaticBatchingUtility.Combine(gameObject);
	}

	public void UpdateImacStatus(string ip, ClientStatus status)
	{
		if (!iMacs.ContainsKey(ip))
			return ;

		Debug.Log("Update imac: " + status);
		
		iMacs[ip].SetBorderColor(clientStatusColors[status]);
	}
	
	public void UpdateImacStatus(IMacInfo iMac)
	{
		UpdateImacStatus(iMac.ip, iMac.status);
	}
	
	void Update ()
	{
		
	}
}
