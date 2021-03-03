using Diploma.Enums;
using Diploma.Interfaces;

namespace Diploma.Controllers
{
    public sealed class AbstractFactoryController: IInitialization,ICleanData
    {
        private readonly IAbstractView _abstractView;
        private readonly IAbstractFactory _abstractFactory;

        public AbstractFactoryController(IAbstractView abstractView, IAbstractFactory abstractFactory)
        {
            _abstractView = abstractView;
            _abstractFactory = abstractFactory;
        }

        public void Initialization()
        {
            _abstractView.NextStage += DoneNextStage;
        }

        private void DoneNextStage(FactoryType factoryType)
        {
            _abstractFactory.Create(factoryType);
        }

        public void CleanData()
        {
            _abstractView.NextStage -= DoneNextStage;
        }
    }
}