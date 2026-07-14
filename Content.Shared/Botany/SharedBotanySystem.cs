using System.Diagnostics.CodeAnalysis;
using Content.Shared.Botany.Components;
using Content.Shared.Chemistry.Components;
using Content.Shared.Chemistry.Reagent;
using Content.Shared.Examine;
using Content.Shared.FixedPoint;
using Robust.Shared.Map;
using Robust.Shared.Prototypes;

namespace Content.Shared.Botany;

public class SharedBotanySystem : EntitySystem
{
    [Dependency] private IPrototypeManager _proto = default!;


    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<Seed, ExaminedEvent>(OnExamined);
        SubscribeLocalEvent<Components.ProduceComponent, ExaminedEvent>(OnProduceExamined);
    }

    public bool TryGetSeedData(Seed seed, [NotNullWhen(true)] out SeedDataInstance? data)
    {
        if (!_proto.TryIndex(seed.SeedId, out SeedPrototype? prototype))
        {
            data = null;
            return false;
        }

        data = new SeedDataInstance(prototype, seed.SeedData);
        return true;
    }

    public bool TryGetSeedData(ProduceComponent comp, [NotNullWhen(true)] out SeedDataInstance? data)
    {
        if (comp.Seed == null)
        {
            data = null;
            return false;
        }

        return TryGetSeedData(comp.Seed, out data);
    }

    private void OnExamined(EntityUid uid, Seed component, ExaminedEvent args)
    {
        if (!args.IsInDetailsRange)
            return;

        if (!TryGetSeedData(component, out var seed))
            return;

        using (args.PushGroup(nameof(SeedDataInstance), 1))
        {
            var name = Loc.GetString(seed.Value.DisplayName);

            args.PushMarkup(
                Loc.GetString(
                    "seed-component-description",
                    ("seedName", name)));

            args.PushMarkup(
                Loc.GetString(
                    "seed-component-plant-yield-text",
                    ("seedYield", seed.Value.Stats.Yield)));

            args.PushMarkup(
                Loc.GetString(
                    "seed-component-plant-potency-text",
                    ("seedPotency", seed.Value.Stats.Potency)));
        }
    }

    public void OnProduceExamined(EntityUid uid, Components.ProduceComponent comp, ExaminedEvent args)
    {
        if (comp.Seed == null)
            return;

                // This used to do something but it shouldn't be doing anything currently, DNAScanners.
    }
}
