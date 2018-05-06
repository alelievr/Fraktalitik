using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MasterServer : NetworkManager
{
	[Space]
	public ClusterGroupConfig	config;

	List< NetworkConnection >	nonClusterClients = new List< NetworkConnection >();
	List< NetworkConnection >	monitoringClients = new List< NetworkConnection >();

	void Start ()
	{
		base.maxConnections = 400;

		NetworkServer.RegisterHandler(NetMessageType.UpdateStatus, ClientUpdateStatusCallback);

		StartServer();
	}

	string		GetIpv4FromConnection(NetworkConnection connection)
	{
		return connection.address.Substring(connection.address.LastIndexOf(':') + 1);
	}

	public override void OnServerConnect(NetworkConnection conn)
	{
		NetworkServer.SetClientReady(conn);

		string ipv4 = GetIpv4FromConnection(conn);

		if (!Cluster.iMacInfosByIp.ContainsKey(ipv4))
		{
			Debug.Log("Connected client " + ipv4 + " is not in the cluster !");
			nonClusterClients.Add(conn);

			return ;
		}

		var iMac = Cluster.iMacInfosByIp[ipv4];

		iMac.connection = conn;
		iMac.status = ClientStatus.Available;

		Cluster.UpdateImacByConnectionDictionary();
		ClusterGUI.instance.UpdateImacStatus(iMac);

		var group = config.iMacGroups.Find(i => i.ip == ipv4);

		if (group == null)
		{
			Debug.LogError("Can't find group for iMac: " + ipv4);
			return ;
		}

		iMac.groupIndex = group.groupIndex;

		var groupMessage = new GroupMessage(config.clusterGroups[group.groupIndex]);
		NetworkServer.SendToClient(conn.connectionId, NetMessageType.Group, groupMessage);
	}

	public override void OnServerDisconnect(NetworkConnection conn)
	{
		IMacInfo	iMac;

		Cluster.iMacInfosByConnection.TryGetValue(conn, out iMac);

		if (iMac != null)
		{
			iMac.connection = null;
			iMac.status = ClientStatus.Disconnected;

			Cluster.UpdateImacByConnectionDictionary();
			ClusterGUI.instance.UpdateImacStatus(iMac);

			return ;
		}

		nonClusterClients.Remove(conn);
		monitoringClients.Remove(conn);
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
		iMac.status = status;

		ClusterGUI.instance.UpdateImacStatus(iMac);
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
}
