using CustomRespawnWaves;
using Mirror;
using Utils.Networking;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Respawning.Objectives;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public abstract class CustomObjectiveBase<T> : FactionObjectiveBase, IFootprintObjective where T : ObjectiveFootprintBase
{
    public ObjectiveFootprintBase ObjectiveFootprint { get; set; }

    public abstract int FakeObjectiveIndex { get; }

    public override void ServerWriteRpc(NetworkWriter writer)
    {
        base.ServerWriteRpc(writer);
        ObjectiveFootprint.ServerWriteRpc(writer);
    }

    public override void ClientReadRpc(NetworkReader reader)
    {
        base.ClientReadRpc(reader);
        ObjectiveFootprint = ClientCreateFootprint();
        ObjectiveFootprint.ClientReadRpc(reader);
    }
    protected abstract T ClientCreateFootprint();
    public new void ServerSendUpdate()
    {
        if (FakeObjectiveIndex > CustomWaves.OriginalObjectives.Count)
            return;
        var fake = CustomWaves.OriginalObjectives[FakeObjectiveIndex];
        ParseFakeObject(fake);
        new ObjectiveCompletionMessage(fake).SendToHubsConditionally((hub) => !hub.IsHost);
    }

    public virtual void ParseFakeObject(FactionObjectiveBase factionObjective)
    {

    }
}
