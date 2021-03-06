using UnityEngine;
using UnityEngine.Networking;

public class IMacInfo
{
    public string				name;
	public string				ip;

	public int					row;
	public int					seat;

	public Vector3				worldPosition;

	public bool					faceEntrance;

	public int					groupIndex;
	public ClientStatus			status;
	public NetworkConnection	connection;
}