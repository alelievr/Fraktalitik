using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MasterServer : NetworkManager
{

	List< NetworkConnection >	nonClusterClients = new List< NetworkConnection >();
	List< NetworkConnection >	monitoringClients = new List< NetworkConnection >();

	void Start ()
	{
		base.maxConnections = 400;

		NetworkServer.RegisterHandler(NetMessageType.UpdateStatus, ClientUpdateStatusCallback);

		StartServer();
	}

	public override void OnServerConnect(NetworkConnection conn)
	{
		NetworkServer.SetClientReady(conn);

		var ipv4 = conn.address.Substring(conn.address.LastIndexOf(':') + 1);

		if (!Cluster.iMacInfosByIp.ContainsKey(ipv4))
		{
			Debug.Log("Connected client " + ipv4 + " is not in the cluster !");

			return ;
		}

		var iMac = Cluster.iMacInfosByIp[ipv4];

		iMac.connection = conn;
		iMac.status = ClientStatus.ConnectedToGroup;

		Cluster.UpdateImacByConnectionDictionary();
		ClusterGUI.instance.UpdateImacStatus(iMac);
	}

	public void ClientUpdateStatusCallback(NetworkMessage message)
	{
		IMacInfo		iMac;
		ClientStatus	status = message.ReadMessage< UpdateStatusMessage >().status;

		Cluster.iMacInfosByConnection.TryGetValue(message.conn, out iMac);

		//if the message came from non-cluster client
		if (iMac == null)
		{
			HandleNonClusterClientMessage(message, status);
			return ;
		}

		//Cluster client message:
		Cluster.iMacInfosByConnection[message.conn].status = status;
	}

	void HandleNonClusterClientMessage(NetworkMessage message, ClientStatus status)
	{
		if (!nonClusterClients.Contains(message.conn))
		{
			Debug.LogError("Unknow client message !" + message);
			return ;
		}
		
		switch (status)
		{
			case ClientStatus.Monitoring:
				nonClusterClients.Remove(message.conn);
				monitoringClients.Add(message.conn);

				Debug.Log("Adding a new monitoring client: " + message.conn.address);

				break ;
			default:
				Debug.LogError("Unknow message type: " + message.msgType);
				break ;
		}
	}

	void Update ()
	{
	}
}
