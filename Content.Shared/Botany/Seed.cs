using Content.Shared.Chemistry.Components;
using Content.Shared.Chemistry.Reagent;
using Content.Shared.FixedPoint;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype;

namespace Content.Shared.Botany;

[RegisterComponent]
public partial class Seed : Component
{
    /// <summary>
    /// Do not use the original seed data if we have a modified copy.
    /// </summary>
    [DataField]
    public SeedData? SeedData;

    /// <summary>
    /// Original Seed Type, for when Seeddata is not assigned.
    /// </summary>
    [DataField("seedId", customTypeSerializer: typeof(PrototypeIdSerializer<SeedPrototype>))]
    public string SeedId;

    public Seed(string seedId)
    {
        SeedId = seedId;
    }
}

[Prototype]
public sealed partial class SeedPrototype : IPrototype
{
    [IdDataField]
    public string ID { get; private set; } = default!;

    [DataField]
    public bool Seedless = false;

    [DataField]
    public string DisplayName = "";

    [DataField]
    public PlantGenome PlantStats = new();

    [DataField]
    public List<ProtoId<PlantTraitPrototype>> Traits = [];

    [DataField]
    public List<ProtoId<ReagentPrototype>> Reagents = [];

    [DataField]
    public List<ProtoId<SeedPrototype>> MutationPrototypes = [];

    [DataField("packetPrototype", customTypeSerializer: typeof(PrototypeIdSerializer<EntityPrototype>))]
    public string PacketPrototype = "SeedBase";
}
