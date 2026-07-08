using Robust.Shared.GameStates;

namespace Content.Shared.Tippable;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
[Access(typeof(TippingSystem))]
public sealed partial class TippableComponent : Component
{
    /// <summary>
    /// The damage dealt to entities on tip over.
    /// </summary>
    [AutoNetworkedField, DataField]
    public float TipDamage;

    /// <summary>
    /// The damage dealt to entities on tip over.
    /// </summary>
    [AutoNetworkedField]
    public bool Tipped;
}
