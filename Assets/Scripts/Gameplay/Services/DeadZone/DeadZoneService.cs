﻿using Cysharp.Threading.Tasks;
using Gameplay.Services.Base;
using Gameplay.Services.DeadZone.Config;
using Signals;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.Activate;
using Zenject;

namespace Gameplay.Services.DeadZone
{
    public class DeadZoneService : GameService, IInitializable
    {
        [Inject] private DeadZoneConfig _config;

        private DeadZoneComponent _deadZoneComponent;
        
        public override void Initialize()
        {
            _signalBus.Subscribe<TreeLevelChangedSignal>(ModeDeadZone);
            base.Initialize();
        }

        private void ModeDeadZone(TreeLevelChangedSignal signal)
        {
            _deadZoneComponent.transform.position = signal.LevelPosition - Vector3.up * 5;
        }

        public override async void Boot()
        {
            _deadZoneComponent = (await Addressables.InstantiateAsync(_config.deadZoneReference))
                .GetComponent<DeadZoneComponent>();

            _deadZoneComponent.Initialize(_config.deadZoneDimension);
            
            _deadZoneComponent.ColliderDetected += ProcessDetectedCollider;
            
            base.Boot();
        }

        private void ProcessDetectedCollider(Collider coll)
        {
            if (!coll.TryGetComponent(out IActivateable activateable))
                return;
            
            activateable.Deactivate();
        }
    }
}