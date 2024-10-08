using Content.Server.Power.NodeGroups;
using Content.Server.Power.Pow3r;
using Content.Server.Power.EntitySystems;
using Content.Server.Power.Components;
using Content.Shared.Power.Components;
using Content.Shared.Power;

using Robust.Server.GameObjects;

namespace Content.Server.Power.EntitySystems
{
    /// <summary>
    ///     System for recieving power from multiple sources, such as:
    ///     - <see cref="ApcPowerReceiverComponent"/>
    ///     - <see cref="PowerConsumerComponent"/>
    ///     - Installed Batteries
    /// </summary>
    public sealed partial class UnifiedPowerReceiverSystem : EntitySystem
    {
        [Dependency] private readonly AppearanceSystem _appearance = default!;

        public override void Initialize()
        {
            base.Initialize();

            SubscribeLocalEvent<UnifiedPowerReceiverComponent, PowerConsumerReceivedChanged>(OnCablePowerChanged);
            SubscribeLocalEvent<UnifiedPowerReceiverComponent, PowerChangedEvent>(OnApcPowerChanged);
        }

        private void OnCablePowerChanged(EntityUid uid, UnifiedPowerReceiverComponent component, ref PowerConsumerReceivedChanged args)
        {
            if (args.ReceivedPower < args.DrawRate)
            {
                component.CablePowerConsumerPowered = false;
            }
            else
            {
                component.CablePowerConsumerPowered = true;
            }
            component.CablePowerConsumerRecievedPower = args.ReceivedPower;
            this.ReconsiderPowered(uid, component);
        }

        private void OnApcPowerChanged(EntityUid uid, UnifiedPowerReceiverComponent component, ref PowerChangedEvent args)
        {
            if (!args.Powered)
            {
                component.ApcPowerProviderPowered = false;
            }
            else
            {
                component.ApcPowerProviderPowered = true;
            }
            component.ApcPowerProviderRecievedPower = args.ReceivingPower;
            this.ReconsiderPowered(uid, component);
        }

        private void ReconsiderPowered(EntityUid uid, UnifiedPowerReceiverComponent component)
        {
            bool NextPowered = false;
            float NextRecievedPower = 0.0f;

            if (component.CablePowerConsumerPowered && component.CablePowerConsumerRecievedPower > 0.0f) {
                NextPowered = true;
                NextRecievedPower = component.CablePowerConsumerRecievedPower;
                component.PowerSource = "Cable";
            }
            if (component.ApcPowerProviderPowered && component.ApcPowerProviderRecievedPower > 0.0f) {
                NextPowered = true;
                NextRecievedPower = component.ApcPowerProviderRecievedPower;
                component.PowerSource = "APC";
            }

            bool PowerChanged = component.Powered != NextPowered;

            component.Powered = NextPowered;
            if (!NextPowered) {
                component.PowerSource = "None";
            }

            var Event = new UnifiedPowerChangedEvent(component.Powered, NextRecievedPower);
            RaiseLocalEvent(uid, ref Event);

            if (PowerChanged) {
                var appearanceQuery = GetEntityQuery<AppearanceComponent>();
                if (appearanceQuery.TryComp(uid, out var appearance))
                    _appearance.SetData(uid, PowerDeviceVisuals.Powered, NextPowered, appearance);
            }

            Dirty(uid, component);
        }
    }
}
