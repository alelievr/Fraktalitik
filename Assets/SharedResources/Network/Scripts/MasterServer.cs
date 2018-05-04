using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MasterServer : NetworkManager
{
	void Start ()
	{
		base.maxConnections = 400;

		StartServer();
	}

	public override void OnServerConnect(NetworkConnection conn)
	{
		NetworkServer.SetClientReady(conn);

		var ipv4 = conn.address.Substring(conn.address.LastIndexOf(':') + 1);

		Debug.Log("ipv4: " + ipv4);

		if (!Cluster.iMacInfos.ContainsKey(ipv4))
		{
			Debug.Log("nope");
			foreach (var iMac2 in Cluster.iMacInfos)
				Debug.Log("iMacIp: " + iMac2.Key);
			return ;
		}
		
		Debug.Log("here !");

		var iMac = Cluster.iMacInfos[ipv4];

		iMac.connection = conn;
		iMac.status = ClientStatus.ConnectedToGroup;

		ClusterGUI.instance.UpdateImacStatus(iMac);
	}

	void Update ()
	{
	}
}
