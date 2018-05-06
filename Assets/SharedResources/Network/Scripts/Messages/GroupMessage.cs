using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GroupMessage : MessageBase
{
	public ClusterGroupInfo	groupInfo;

	public GroupMessage() {}

    public GroupMessage(ClusterGroupInfo groupInfo)
    {
        this.groupInfo = groupInfo;
    }

    public override void Deserialize(NetworkReader reader)
    {
		groupInfo = new ClusterGroupInfo();

		groupInfo.syncDelay = reader.ReadSingle();
		groupInfo.sceneName = reader.ReadString();
		groupInfo.name = reader.ReadString();
    }

    public override void Serialize(NetworkWriter writer)
    {
		writer.Write(groupInfo.syncDelay);
		writer.Write(groupInfo.sceneName);
		writer.Write(groupInfo.name);
    }
}
