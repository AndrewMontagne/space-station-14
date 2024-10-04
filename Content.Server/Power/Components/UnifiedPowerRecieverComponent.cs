namespace Content.Server.Power.Components;

[RegisterComponent]
public sealed partial class UnifiedPowerReceiverComponent : Component
{
    /// <summary>
    /// Is it powered?
    /// </summary>
    [ViewVariables]
    public bool Powered;

    /// <summary>
    /// Where is it powered from?
    /// </summary>
    [ViewVariables]
    public string PowerSource = "None";
}

/// <summary>
/// Raised whenever an UnifiedPowerReceiver becomes powered / unpowered.
/// Does nothing on the client.
/// </summary>
[ByRefEvent]
public readonly record struct UnifiedPowerChangedEvent(bool Powered, float ReceivingPower);
