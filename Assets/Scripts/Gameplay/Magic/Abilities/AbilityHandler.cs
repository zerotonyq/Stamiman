﻿using System.Collections.Generic;
using System.Linq;
using Gameplay.Core.Base;
using Gameplay.Core.Container;
using Gameplay.Magic.Abilities.Base;
using UnityEngine;

namespace Gameplay.Magic.Abilities
{
    [RequireComponent(typeof(ComponentContainer))]
    public class AbilityHandler : MonoComponent
    {
        [SerializeField] private List<MagicAbility> _handledAbilities = new();

        public override void Initialize()
        {
        }


        public void AttachAbility(MagicAbility magicAbility, List<MagicAbility> antagonistPrefabs)
        {
            foreach (var handledAbility in _handledAbilities)
            {
                if (antagonistPrefabs.Any( a=> a.GetType() == handledAbility.GetType()))
                {
                    magicAbility.Deactivate();
                    handledAbility.Deactivate();
                    return;
                }

                if (handledAbility.GetType() == magicAbility.GetType())
                {
                    magicAbility.Deactivate();
                    handledAbility.ApplyEffects(GetComponent<ComponentContainer>());
                    return;
                }
            }

            _handledAbilities.Add(magicAbility);

            magicAbility.transform.SetParent(transform);

            magicAbility.Deactivated += DetachAbility;

            magicAbility.ApplyEffects(GetComponent<ComponentContainer>());
        }

        private void DetachAbility(GameObject obj)
        {
            var ability = obj.GetComponent<MagicAbility>();
            
            if (!_handledAbilities.Contains(ability))
            {
                Debug.LogError("there is no such ability to detach " + ability.name);
                return;
            }

            ability.transform.SetParent(null);
            ability.Deactivated -= DetachAbility;

            _handledAbilities.Remove(ability);
        }
    }
}