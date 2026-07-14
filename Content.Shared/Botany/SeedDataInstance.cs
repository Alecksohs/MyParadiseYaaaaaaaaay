using Content.Shared.Chemistry.Reagent;
using Robust.Shared.Prototypes;

namespace Content.Shared.Botany;

public readonly struct SeedDataInstance(SeedPrototype prototype, SeedData? modifiedData)
{
    public readonly SeedPrototype Prototype = prototype;
    public readonly SeedData? ModifiedData = modifiedData;

    public PlantGenome Stats =>
        ModifiedData?.PlantStats ?? Prototype.PlantStats;

    public IReadOnlyList<ProtoId<PlantTraitPrototype>> Traits =>
        ModifiedData?.Traits ?? Prototype.Traits;

    public IReadOnlyList<ProtoId<ReagentPrototype>> Reagents =>
        ModifiedData?.Reagents ?? Prototype.Reagents;

    public string DisplayName =>
        ModifiedData?.DisplayName ?? Prototype.DisplayName;
    public bool Seedless => ModifiedData?.Seedless ?? Prototype.Seedless;
}
