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
    // The 'power' of a plant. Generally effects the amount of reagent in a plant, also used in other ways.
    [DataField]
    public float Potency = 1;

    // Amount of growns created per harvest. If is -1, the plant/shroom/weed is never meant to be harvested.
    [DataField]
    public int Yield;

    // How long before the plant begins to take damage from age.
    [DataField]
    public float Lifespan;

    // Changes the amount of time needed for a plant to become harvestable.
    [DataField]
    public float Production;

    // Amount of health the plant has.
    [DataField]
    public float Endurance;

    // Used to determine which sprite to switch to when growing.
    [DataField]
    public float Maturation = 6;

    // Amount of growth sprites the plant has.
    [DataField]
    public float GrowthStages = 6;

    // How rare the plant is. Used for giving points to cargo when shipping off to Centcom.
    [DataField]
    public int Rarity = 0;

    // Percentage chance per tray update to grow weeds
    [DataField]
    public float WeedChance = 5f;

    // If weed chance passes, this many weeds sprout during growth
    [DataField]
    public float WeedRate = 1f;
}
