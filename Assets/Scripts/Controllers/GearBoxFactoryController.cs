using Diploma.Constructor;
using Diploma.Enums;
using Diploma.Interfaces;
using Interfaces;

namespace Controllers
{
    public sealed class GearBoxFactoryController: IInitialization,ICleanData
    {
        private readonly GearBoxFactory _gearBoxFactory;
        private readonly GearBoxFactoryView _gearBoxFactoryView;
        private readonly DataBaseController _dataBaseController;

        public GearBoxFactoryController(GearBoxFactory gearBoxFactory,GearBoxFactoryView gearBoxFactoryView,
            DataBaseController dataBaseController)
        {
            _gearBoxFactory = gearBoxFactory;
            _gearBoxFactoryView = gearBoxFactoryView;
            _dataBaseController = dataBaseController;
        }
        
        public void Initialization()
        {
            _gearBoxFactoryView.ChooseTypeOf += GearBoxFactoryViewOnChooseTypeOf;
            _gearBoxFactoryView.NextStage += GearBoxFactoryViewOnNextStage;
        }

        private void GearBoxFactoryViewOnNextStage(LoadingParts loadingParts)
        {
            //_dataBaseController.SetTable();
        }

        private void GearBoxFactoryViewOnChooseTypeOf(TypesOfGearBoxes obj)
        {
            var info = _gearBoxFactory.ChooseAType(obj);
            _gearBoxFactoryView.LoadNextUi(info);
        }

        public void CleanData()
        {
            _gearBoxFactoryView.ChooseTypeOf -= GearBoxFactoryViewOnChooseTypeOf;
            _gearBoxFactoryView.NextStage -= GearBoxFactoryViewOnNextStage;
        }
    }
}