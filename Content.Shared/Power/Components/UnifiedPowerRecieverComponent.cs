using Robust.Shared.GameStates;

namespace Content.Shared.Power.Components;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class UnifiedPowerReceiverComponent : Component
{
    /// <summary>
    /// Is it powered?
    /// </summary>
    [DataField, AutoNetworkedField]
    public bool Powered;

    /// <summary>
    /// Where is it powered from?F
    /// </summary>
    [DataField, AutoNetworkedField]
    public string PowerSource = "None";

    public bool ApcPowerProviderPowered = false;
    public float ApcPowerProviderRecievedPower = 0.0f;

    public bool CablePowerConsumerPowered = false;
    public float CablePowerConsumerRecievedPower = 0.0f;
}
