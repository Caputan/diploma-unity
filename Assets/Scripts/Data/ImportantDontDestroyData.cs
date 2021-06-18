﻿using Controllers;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Create Important Data", fileName = "DataConfig", order = 0)]
    public sealed class ImportantDontDestroyData: ScriptableObject
    {
        public int lessonID;

        public int activatedUserID;

        public float mouseSensitivity;

        public int[] assembleOrder;

        public LoadingSceneController LoadingSceneController;
        //дополнить по мере надобности
    }
}