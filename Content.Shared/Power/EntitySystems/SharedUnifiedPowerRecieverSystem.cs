using Robust.Shared.GameStates;

namespace Content.Shared.Power.Components;

[NetworkedComponent]
public abstract partial class SharedUnifiedPowerReceiverSystem : EntitySystem
{
    [ViewVariables]
    public bool Powered;
}
