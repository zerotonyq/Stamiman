﻿using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Gameplay.Services.Camera.Config
{
    [CreateAssetMenu(menuName = "CreateConfig/" + nameof(CameraConfig), fileName = nameof(CameraConfig))]
    public class CameraConfig : ScriptableObject
    {
        public AssetReferenceGameObject cameraReference;
    }
}