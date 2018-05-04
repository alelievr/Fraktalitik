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

	public override void OnClientConnect(NetworkConnection conn)
	{
		Debug.Log("Client connected !");
	}

	void Update ()
	{
	}
}
