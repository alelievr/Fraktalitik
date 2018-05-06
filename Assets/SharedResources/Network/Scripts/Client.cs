using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.SceneManagement;
using System.Net;
using System.Net.Sockets;

public class Client : NetworkManager
{
	[Space]
	public ClientStatus		status;

	[Space]
	public AudioListener	audioListener;

	void Start ()
	{
		base.connectionConfig.ConnectTimeout = 0;
		
		//Get iMac world position:

		var iMac = Cluster.iMacInfosByIp[GetLocalIPAddress()];

		audioListener.transform.position = iMac.worldPosition;
	}

	public static string GetLocalIPAddress()
	{
		var host = Dns.GetHostEntry(Dns.GetHostName());
		foreach (var ip in host.AddressList)
		{
			if (ip.AddressFamily == AddressFamily.InterNetwork)
			{
				return ip.ToString();
			}
		}

		Debug.LogError("No local ip !");

		return null;
	}

	private void OnDisconnectedFromServer(NetworkDisconnection info)
	{
		Debug.Log("Disconnected !");
	}

	public override void OnClientConnect(NetworkConnection conn)
	{
		client.Send(NetMessageType.UpdateStatus, new UpdateStatusMessage(ClientStatus.WaitingForGroup));
		
		Debug.Log("CONNECTED TO: " + conn.address + " | " + conn.connectionId);

		client.RegisterHandler(NetMessageType.Group, GroupMessageCallback);
	}
	
	void Update()
	{
		if (client == null)
		{
			Debug.Log("Starting new client connection");
			StartClient();
		}
	}

	void GroupMessageCallback(NetworkMessage message)
	{
		var groupInfo = message.ReadMessage< GroupMessage >().groupInfo;

		Debug.Log("Client changed to group: " + groupInfo.name + " | " + groupInfo.sceneName);
		
		SceneManager.LoadScene(groupInfo.sceneName);

		client.Send(NetMessageType.UpdateStatus, new UpdateStatusMessage(ClientStatus.ConnectedToGroup));
	}
}
