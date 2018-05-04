using UnityEngine;
using UnityEngine.Networking;

public class IMacInfo
{
    public string				name;
	public string				ip;
	public ClientStatus			status;
	public NetworkConnection	connection;

	public int					row;
	public int					seat;

	public Vector3				worldPosition;

	public bool					faceEntrance;
}