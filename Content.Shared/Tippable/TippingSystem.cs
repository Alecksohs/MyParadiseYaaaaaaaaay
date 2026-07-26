using System.Numerics;
using Content.Shared.Construction.Components;
using Content.Shared.Construction.EntitySystems;
using Content.Shared.Damage;
using Content.Shared.Damage.Components;
using Content.Shared.Damage.Systems;
using Content.Shared.IdentityManagement;
using Content.Shared.Interaction;
using Content.Shared.Popups;
using Content.Shared.UserInterface;
using Content.Shared.Verbs;
using Robust.Shared.Audio;
using Robust.Shared.Audio.Systems;

namespace Content.Shared.Tippable;

public sealed class TippingSystem : EntitySystem
{
    [Dependency] private readonly SharedTransformSystem _transformSystem = default!;
    [Dependency] private readonly EntityLookupSystem _lookup = default!;
    [Dependency] private readonly DamageableSystem _damageableSystem = default!;
    [Dependency] private readonly SharedAudioSystem Audio = default!;
    [Dependency] private readonly SharedPopupSystem PopupSystem = default!;



    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<TippableComponent, ShovedIntoEvent>(OnShovedInto);
        SubscribeLocalEvent<TippableComponent, GetVerbsEvent<AlternativeVerb>>(AddVerb);
    }

    private void AddVerb(EntityUid uid, TippableComponent component, ref GetVerbsEvent<AlternativeVerb> args)
    {
        if (!component.Tipped)
            return;

        args.Verbs.Add(new AlternativeVerb
        {
            Text = Loc.GetString("tippable-system-right-machine"),
            Act = () => OnAltClick(uid, component)
        });
    }

    private void OnAltClick(EntityUid uid, TippableComponent component)
    {
        component.Tipped = false;
        if (!TryComp(uid, out TransformComponent? transformComponent))
        {
            return;
        }

        // Assuming 0f is upright. Could be wrong. Refactor if so.
        _transformSystem.SetLocalRotation(uid, new Angle(float.DegreesToRadians(0f)));
    }


    private void OnShovedInto(EntityUid uid, TippableComponent component, ref ShovedIntoEvent args)
    {
        if(!component.Tipped)
            TipOverOnto(uid, args.Target, component);
    }

    private void TipOverOnto(EntityUid machine, EntityUid victim, TippableComponent? component)
    {
        if (!TryComp(machine, out TransformComponent? transformComponent))
        {
            return;
        }

        Audio.PlayPredicted(new SoundPathSpecifier("/Audio/Effects/metalhit.ogg"), machine, victim);

        var dir = (_transformSystem.GetWorldPosition(victim) - _transformSystem.GetWorldPosition(machine)).Normalized();
        _transformSystem.SetWorldPositionRotation(machine,
            _transformSystem.GetWorldPosition(machine) + dir,
            90f,
            transformComponent);

        _transformSystem.SetLocalRotation(machine, new Angle(float.DegreesToRadians(90f)));

        var hit = _lookup.GetEntitiesInRange(machine, 0.25f, LookupFlags.Dynamic);

        var damage = new DamageSpecifier();
        damage.DamageDict.Add("Blunt", 100);

        foreach (var entity in hit)
        {
            if (!HasComp<DamageableComponent>(entity))
                continue;

            _damageableSystem.TryChangeDamage(entity, damage, origin: machine);
        }

        var msgRest  = Loc.GetString("tippable-system-crushed-player", ("performerName", Identity.Entity(machine, EntityManager)), ("targetName", Identity.Entity(machine, EntityManager)));
        PopupSystem.PopupCoordinates(msgRest, Transform(victim).Coordinates, PopupType.MediumCaution);
        var msgUser  = Loc.GetString("tippable-system-crushed-player-to-user", ("targetName", Identity.Entity(machine, EntityManager)));
        PopupSystem.PopupEntity(msgUser, victim, PopupType.LargeCaution);
        component?.Tipped = true;
    }
}
