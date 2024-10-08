using Content.Client.Power.Components;
using Content.Shared.Power.Components;

namespace Content.Client.Power.EntitySystems;

public static class StaticPowerSystem
{
    // Using this makes the call shorter.
    // ReSharper disable once UnusedParameter.Global
    public static bool IsPowered(this EntitySystem system, EntityUid uid, IEntityManager entManager)
    {
        UnifiedPowerReceiverComponent? unifiedPowerReceiver = null;
        if (entManager.TryGetComponent(uid, out unifiedPowerReceiver) && unifiedPowerReceiver.Powered)
            return true;

        ApcPowerReceiverComponent? apcPowerReceiver = null;
        if (entManager.TryGetComponent(uid, out apcPowerReceiver) && apcPowerReceiver.Powered)
            return true;

        return false;
    }
}
