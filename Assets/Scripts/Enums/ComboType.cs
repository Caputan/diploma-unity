using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;

namespace Diploma.Enums
{
    public sealed class ComboType
    {
        public FactoryType type { get; }
        public TypesOfGearBoxes typesOfGearBoxes { get;  set;}
        
        public Dictionary<FactoryType, TypesOfGearBoxes> ComboTypesOfGearboxes;
    }
}