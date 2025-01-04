﻿using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Gameplay.Camera.Config
{
    [CreateAssetMenu(menuName = "CreateConfig/" + nameof(CameraConfig), fileName = nameof(CameraConfig))]
    public class CameraConfig : Utils.Initialize.Config
    {
        public AssetReferenceGameObject cameraReference;
        public override Type InitializableType { get; } = typeof(CameraProvider);
    }
}