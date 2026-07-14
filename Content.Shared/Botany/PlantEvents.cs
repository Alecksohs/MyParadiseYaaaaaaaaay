using Robust.Shared.Map;

namespace Content.Shared.Botany;

[ByRefEvent]
public record struct PlantOnHitEvent(EntityUid HitEntity, EntityUid? Attacker, Seed Seed);

[ByRefEvent]
public record struct PlantOnThrownEvent(MapCoordinates TargetLocation, EntityUid? User);
