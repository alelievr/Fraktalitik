using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class UpdateStatusMessage : MessageBase
{
    public ClientStatus status;

	public UpdateStatusMessage() {}

    public UpdateStatusMessage(ClientStatus status)
    {
        this.status = status;
    }

    public override void Deserialize(NetworkReader reader)
    {
        status = (ClientStatus)reader.ReadPackedUInt32();
    }

    public override void Serialize(NetworkWriter writer)
    {
        writer.WritePackedUInt32((uint)status);
    }
}
