using Diploma.Enums;

namespace Diploma.Interfaces
{
    public interface IAbstractFactory
    {
        void Create(FactoryType factoryType);
    }
}