using Unity.Netcode.Components;
using UnityEngine;

public class ClientNetworkTransform : NetworkTransform
{
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        CanCommitToTransform = IsOwner;
    }
    public override void OnUpdate()
    {
        CanCommitToTransform = IsOwner;
        base.OnUpdate();

    }
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }
}
