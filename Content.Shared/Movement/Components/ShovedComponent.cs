using Robust.Shared.GameStates;

namespace Content.Shared.Movement.Components;

/// <summary>
/// Marker component given to those who have been shoved.
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class ShovedComponent : Component
{
    /// <summary>
    /// The duration to stun the owner on collide with environment.
    /// </summary>
    [DataField, AutoNetworkedField]
    public TimeSpan KnockdownDuration;

    /// <summary>
    /// The duration of the shove collision event. Be short.
    /// </summary>
    [DataField, AutoNetworkedField]
    public TimeSpan ShoveWindow;

    /// <summary>
    /// The player who shoved the owner of the component.
    /// </summary>
    [DataField, AutoNetworkedField]
    public EntityUid Shover;
}
