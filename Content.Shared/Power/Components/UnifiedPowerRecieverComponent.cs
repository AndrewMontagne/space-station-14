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
    /// Where is it powered from?
    /// </summary>
    [DataField, AutoNetworkedField]
    public string PowerSource = "None";

    /// <summary>
    /// Should this consume APC power?
    /// </summary>
    [DataField, AutoNetworkedField]
    public bool ApcPowerProviderEnabled = true;
    public bool ApcPowerProviderPowered = false;
    public float ApcPowerProviderRecievedPower = 0.0f;

    /// <summary>
    /// Should this consume Cable power?
    /// </summary>
    [DataField, AutoNetworkedField]
    public bool CablePowerProviderEnabled = true;
    public bool CablePowerConsumerPowered = false;
    public float CablePowerConsumerRecievedPower = 0.0f;


    /// <summary>
    /// Should this consume battery power?
    /// </summary>
    [DataField, AutoNetworkedField]
    public bool BatteryPowerProviderEnabled = true;
    public bool BatteryPowerConsumerPowered = false;
    public float BatteryPowerRecievedPower = 0.0f;
}
