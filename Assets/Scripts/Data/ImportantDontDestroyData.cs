using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Create Important Data", fileName = "DataConfig", order = 0)]
    public sealed class ImportantDontDestroyData: ScriptableObject
    {
        public int lessonID;

        public int activatedUserID;

        public float mouseSensitivity;
        
        //дополнить по мере надобности
    }
}