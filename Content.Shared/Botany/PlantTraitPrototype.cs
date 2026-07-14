using Robust.Shared.Prototypes;

namespace Content.Shared.Botany;

[Prototype]
public sealed partial class PlantTraitPrototype : IPrototype
{
    [IdDataField]
    public string ID { get; private set; } = default!;

    public ComponentRegistry TraitComponents { get; private set; } = default!;
}
