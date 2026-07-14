using Content.Server.Botany.Components;
using Content.Shared.Botany.Components;
using Content.Shared.EntityEffects;
using Content.Shared.Examine;
using Content.Shared.FixedPoint;

namespace Content.Server.Botany.Systems;

public sealed partial class BotanySystem
{
    [Dependency] private readonly SharedEntityEffectsSystem _entityEffects = default!;

    public void ProduceGrown(EntityUid uid, ProduceComponent produce)
    {
        if (!TryGetSeed(produce, out var seed))
            return;

        foreach (var mutation in seed.Mutations)
        {
            if (mutation.AppliesToProduce)
                _entityEffects.TryApplyEffect(uid, mutation.Effect);
        }

        if (!_solutionContainerSystem.EnsureSolution(uid,
                produce.SolutionName,
                out var solutionContainer,
                FixedPoint2.Zero))
            return;

        solutionContainer.RemoveAllSolution();
        foreach (var (chem, quantity) in seed.Chemicals)
        {
            var amount = quantity.Min;
            if (quantity.PotencyDivisor > 0 && seed.Potency > 0)
                amount += seed.Potency / quantity.PotencyDivisor;
            amount = FixedPoint2.Clamp(amount, quantity.Min, quantity.Max);
            solutionContainer.MaxVolume += amount;
            solutionContainer.AddReagent(chem, amount);
        }
    }


}
