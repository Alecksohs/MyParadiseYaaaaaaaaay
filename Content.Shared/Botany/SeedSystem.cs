using Content.Shared.Chemistry.Components;
using Content.Shared.Chemistry.Reagent;
using Content.Shared.FixedPoint;
using Robust.Shared.Prototypes;
using Robust.Shared.Utility;

namespace Content.Shared.Botany;

public sealed class SeedSystem : EntitySystem
{
    [Dependency] private IPrototypeManager _proto = default!;

    /// <summary>
    /// By taking the Seed Data, we make reagants from it.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="min">Minimum amount of chemicals. Usually 1.</param>
    /// <param name="potencyMultiplier">Maximum Chemicals from 100 Potency, so it's max / 100. 20 units maximum is 0.2f.</param>
    /// <returns></returns>
    public Solution BuiltReagantFromSeed(Seed seed, float min = 1f, float potencyMultiplier = 0.2f)
    {
        var seedData = GetSeedData(seed);
        var reagantInjectableSolution = new Solution();

        foreach (var reagant in seedData.Reagents)
        {
            if (!_proto.TryIndex(reagant, out var reagentPrototype))
            {
                continue;
            }

            var amount = FixedPoint2.Max(min, seedData.Stats.Potency * potencyMultiplier);

            reagantInjectableSolution.AddReagent(reagentPrototype, new ReagentId(reagentPrototype.ID, null), amount);
        }

        return reagantInjectableSolution;
    }

    public SeedDataInstance GetSeedData(Seed seed)
    {
        if (!_proto.TryIndex(seed.SeedId, out SeedPrototype? prototype))
        {
            throw new InvalidOperationException(
                $"Seed {seed.SeedId} does not exist.");
        }

        return new SeedDataInstance(prototype, seed.SeedData);
    }
}
