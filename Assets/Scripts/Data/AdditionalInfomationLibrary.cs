using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    [CreateAssetMenu(menuName = "Create Library massive", fileName = "Library", order = 1)]
    public class AdditionalInfomationLibrary: ScriptableObject
    { 
        public List<LibraryObjcet> libraryObjcets;
    }
    
}