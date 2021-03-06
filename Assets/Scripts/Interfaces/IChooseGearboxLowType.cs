using System;
using Diploma.Enums;

namespace Interfaces
{
    public interface IChooseGearboxLowType
    {
        void ChoosedLowType(TypesOfGearBoxes typesOfGearBoxes);
        event Action<TypesOfGearBoxes> ChooseTypeOf;
    }
}