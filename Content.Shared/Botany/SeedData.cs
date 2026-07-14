using Content.Shared.Chemistry.Components;
using Content.Shared.Chemistry.Reagent;
using Content.Shared.FixedPoint;
using Robust.Shared.Prototypes;

namespace Content.Shared.Botany;

[DataDefinition]
public sealed partial class SeedData
{
    [DataField]
    public string DisplayName = "";

    [DataField]
    public PlantGenome PlantStats = new();

    [DataField]
    public List<ProtoId<PlantTraitPrototype>> Traits = [];

    [DataField]
    public List<ProtoId<ReagentPrototype>> Reagents = [];

    /// <summary>
    /// Does not spawn seeds.
    /// </summary>
    [DataField]
    public bool Seedless;

    /// <summary>
    /// Potential non-trait based mutations such as Tomatos -> Bluespace Tomatos. Behavior from SS13.
    /// </summary>
    [DataField]
    public List<ProtoId<SeedPrototype>> MutationPrototypes = [];

    public SeedData()
    {
    }

    public SeedData(SeedPrototype prototype)
    {
        DisplayName = prototype.DisplayName;
        PlantStats = prototype.PlantStats;
        Traits = new(prototype.Traits);
        Reagents = new(prototype.Reagents);
        MutationPrototypes = new(prototype.MutationPrototypes);
    }


}

[DataDefinition]
public sealed partial class PlantGenome
{
    [DataField]
    public float Potency = 1;

    [DataField]
    public int Yield;

    [DataField]
    public float Lifespan;

    [DataField]
    public float Production;

    [DataField]
    public float Endurance;
}
