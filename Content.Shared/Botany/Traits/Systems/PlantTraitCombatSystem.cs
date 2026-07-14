using Content.Shared.Body.Components;
using Content.Shared.Body.Systems;
using Content.Shared.Botany.Traits.Components;
using Content.Shared.Chemistry.Components;
using Content.Shared.Chemistry.Reagent;
using Content.Shared.FixedPoint;
using Robust.Shared.Prototypes;

namespace Content.Shared.Botany.Traits.Systems;

public sealed class PlantTraitCombatSystem : EntitySystem
{
    [Dependency] private readonly IPrototypeManager _proto = default!;
    [Dependency] private readonly SharedBloodstreamSystem _bloodstreamSystem = default!;
    [Dependency] private readonly SeedSystem _seedSystem = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<PlantTraitStingingComponent, PlantOnHitEvent>(OnHitHypodermicPrickles);
    }

    private void OnHitHypodermicPrickles(EntityUid uid, PlantTraitStingingComponent component, PlantOnHitEvent args)
    {
        var hitEnt = args.HitEntity;

        var solution = _seedSystem.BuiltReagantFromSeed(args.Seed ,1f, 0.2f);

        _bloodstreamSystem.TryAddToBloodstream(hitEnt, solution);
    }
}
