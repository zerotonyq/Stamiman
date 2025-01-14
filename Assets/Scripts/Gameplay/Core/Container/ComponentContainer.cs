﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Gameplay.Core.Base;
using R3;
using UnityEngine.AddressableAssets;

namespace Gameplay.Core.Container
{
    public class ComponentContainer : MonoComponentContainer
    {
        public override List<MonoComponent> Components { get; } = new();
        
        private DisposableBag _disposableBag;

        public override Task Initialize()
        {
            foreach (var monoComponent in GetComponents<MonoComponent>())
            {
                monoComponent.Initialize();

                Components.Add(monoComponent);
            }

            foreach (var inputBinder in GetComponents<Binder>())
            {
                inputBinder.Bind();
            }

            return Task.CompletedTask;
        }

        public void Destroy() => Addressables.ReleaseInstance(gameObject);

        private void OnDestroy() => _disposableBag.Dispose();
    }
}