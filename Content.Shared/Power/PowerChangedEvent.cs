namespace Content.Shared.Power;

/// <summary>
/// Raised whenever an ApcPowerReceiver becomes powered / unpowered.
/// Does nothing on the client.
/// </summary>
[ByRefEvent]
public readonly record struct PowerChangedEvent(bool Powered, float ReceivingPower);

/// <summary>
/// Raised whenever an UnifiedPowerReceiver becomes powered / unpowered.
/// Does nothing on the client.
/// </summary>
[ByRefEvent]
public readonly record struct UnifiedPowerChangedEvent(bool Powered, float ReceivingPower);
