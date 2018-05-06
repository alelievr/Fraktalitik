using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClientStatus
{
	Unknown,			//Unknown
	Disconnected,		//Disconnected
	Available,			//Connected by no information
	ConnectedToGroup,	//Connected and scene loaded successfully
	WaitingForGroup,	//Connected and waiting to receive it's group informations
	GroupServer,		//Connected and group server (controlling other iMac of the group)
	Monitoring,			//Monitor of the server
}
