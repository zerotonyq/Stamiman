﻿using System;
using System.Collections.Generic;
using Gameplay.Magic;
using Gameplay.Magic.Pickupables.Base;
using Gameplay.Services.LevelActivity.Config;
using Signals.Activities;
using Signals.Activities.Base;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Random = UnityEngine.Random;

namespace Gameplay.Services.Boss.Config
{
    [CreateAssetMenu(menuName = "CreateConfig/" + nameof(BossDifficultyConfig), fileName = nameof(BossDifficultyConfig))]
    public class BossDifficultyConfig : ActivityConfig
    {
        public List<DifficultySection> sections = new();
        
        public override ActivitySignal ConstructSignal() => new BossActivitySignal();

        [Serializable]
        public struct DifficultySection
        {
            public List<MagicPickupable> allowedPickupables;

            public List<BossConfig> bossesConfigs;

            public void GenerateBossConfigs()
            {
                foreach (var bossConfig in bossesConfigs)
                {
                    if(!bossConfig.autoGenerated)
                        continue;
                    
                    bossConfig.Generate(allowedPickupables);
                }
            }
        }

        [Serializable]
        public struct BossConfig
        {
            public AssetReferenceGameObject bossReference;
            public List<AutomationMagicComponentBinder.AbilityInterval> abilityIntervals;

            [Space, Header("AUTO GENERATION")] 
            public bool autoGenerated;
            public int requiredCount;
            public float minInterval, maxInterval;

            public void Generate(List<MagicPickupable> allowed)
            {
                for (var i = 0; i < requiredCount; i++)
                {
                    abilityIntervals.Add(new AutomationMagicComponentBinder.AbilityInterval()
                    {
                        beforeInterval = Random.Range(minInterval, maxInterval),
                        pickupablePrefab = allowed[Random.Range(0, allowed.Count)]
                    });
                }
            }
        }

    }
}