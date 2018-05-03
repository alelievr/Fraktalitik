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
	};

	void Start ()
	{
		/*int		width = Cluster.clusterIPMap.GetLength(0);
		int		hieght = Cluster.clusterIPMap.GetLength(1);
		
		foreach (var ipPart in Cluster.clusterIPMap)
		{
			var go = GameObject.Instantiate(clusterSeatPrefab, transform);
			var imacGUI = go.GetComponent< ClusterIMacGUI >();

			imacGUI.SetText(ipPart);

			clusterImacs.Add(imacGUI);
		}*/
		
	}
	
	void Update ()
	{
		
	}
}
