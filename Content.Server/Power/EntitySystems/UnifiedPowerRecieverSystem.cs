using Content.Server.Power.NodeGroups;
using Content.Server.Power.Pow3r;
using Content.Server.Power.EntitySystems;
using Content.Server.Power.Components;
using Content.Shared.Power.Components;
using Content.Shared.Power;

namespace Content.Server.Power.EntitySystems
{
    /// <summary>
    ///     System for recieving power from multiple sources, such as:
    ///     - <see cref="ApcPowerReceiverComponent"/>
    ///     - <see cref="PowerConsumerComponent"/>
    ///     - Installed Batteries
    /// </summary>
    public sealed partial class UnifiedPowerReceiverSystem : SharedUnifiedPowerReceiverSystem
    {
        public override void Initialize()
        {
            base.Initialize();

            SubscribeLocalEvent<UnifiedPowerReceiverComponent, PowerConsumerReceivedChanged>(OnCablePowerChanged);
            SubscribeLocalEvent<UnifiedPowerReceiverComponent, PowerChangedEvent>(OnApcPowerChanged);
        }

        public PowerConsumerComponent? CablePowerConsumer = null;
        private bool CablePowerConsumerPowered = false;
        private float CablePowerConsumerRecievedPower = 0.0f;
        private void OnCablePowerChanged(EntityUid uid, UnifiedPowerReceiverComponent component, ref PowerConsumerReceivedChanged args)
        {
            if (args.ReceivedPower < args.DrawRate)
            {
                this.CablePowerConsumerPowered = false;
            }
            else
            {
                this.CablePowerConsumerPowered = true;
            }
            CablePowerConsumerRecievedPower = args.ReceivedPower;
            this.ReconsiderPowered(uid, component);
        }

        public ApcPowerProviderComponent? ApcPowerProvider = null;
        private bool ApcPowerProviderPowered = false;
        private float ApcPowerProviderRecievedPower = 0.0f;
        private void OnApcPowerChanged(EntityUid uid, UnifiedPowerReceiverComponent component, ref PowerChangedEvent args)
        {
            if (!args.Powered)
            {
                this.ApcPowerProviderPowered = false;
            }
            else
            {
                this.ApcPowerProviderPowered = true;
            }
            this.ApcPowerProviderRecievedPower = args.ReceivingPower;
            this.ReconsiderPowered(uid, component);
        }

        private void ReconsiderPowered(EntityUid uid, UnifiedPowerReceiverComponent component)
        {
            bool NextPowered = false;
            float NextRecievedPower = 0.0f;

            if (this.CablePowerConsumerPowered) {
                NextPowered = true;
                NextRecievedPower = CablePowerConsumerRecievedPower;
                component.PowerSource = "Cable";
            }
            if (this.ApcPowerProviderPowered) {
                NextPowered = true;
                NextRecievedPower = ApcPowerProviderRecievedPower;
                component.PowerSource = "APC";
            }

            component.Powered = NextPowered;
            if (!NextPowered) {
                component.PowerSource = "None";
            }

            var Event = new UnifiedPowerChangedEvent(this.Powered, NextRecievedPower);
            RaiseLocalEvent(uid, ref Event);
        }
    }
}
