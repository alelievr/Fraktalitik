using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class Client : NetworkManager
{
	public ClientStatus		status;

	void Start ()
	{
		base.connectionConfig.ConnectTimeout = 0;
	}

	private void OnDisconnectedFromServer(NetworkDisconnection info)
	{
		Debug.Log("Disconnected !");
	}

	public override void OnClientConnect(NetworkConnection conn)
	{
		client.Send(NetMessageType.UpdateStatus, new UpdateStatusMessage(ClientStatus.WaitingForGroup));
		
		Debug.Log("CONNECTED TO: " + conn.address + " | " + conn.connectionId);
	}
	
	void Update()
	{
		if (client == null)
		{
			Debug.Log("Starting new client connection");
			StartClient();
		}
	}
}
