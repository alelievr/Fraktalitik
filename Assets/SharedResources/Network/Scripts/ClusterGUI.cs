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

	void Start ()
	{
		foreach (var iMac in Cluster.GetImacInfos())
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
		
		iMacs[ip].SetBorderColor(clientStatusColors[status]);
	}
	
	void Update ()
	{
		
	}
}
