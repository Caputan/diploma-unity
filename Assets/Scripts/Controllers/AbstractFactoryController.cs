using Diploma.Enums;
using Diploma.Interfaces;

namespace Diploma.Controllers
{
    public class AbstractFactoryController: IInitialization,IExecute,ICleanData
    {
        private readonly IAbstractView _abstractView;

        public AbstractFactoryController(IAbstractView abstractView)
        {
            _abstractView = abstractView;
        }

        public void Initialization()
        {
            _abstractView.NextStage += DoneNextStage;
        }

        private void DoneNextStage(FactoryType factoryType)
        {
            throw new System.NotImplementedException();
        }

        public void Execute(float deltaTime)
        {
            throw new System.NotImplementedException();
        }

        public void CleanData()
        {
            throw new System.NotImplementedException();
        }
    }
}