using Diploma.Interfaces;

namespace Diploma.Controllers
{
    public sealed class PracticeSceneController: IInitialization
    {
        // данный класс должен подписаться на нужные ему события
        // для завершения сессии или прочих операций
        // список операций:
        // 1. завершение практики и проставление в бд маркера завершенности
        // 2. перезапуск сцены
        // ...

        public PracticeSceneController()
        {
            
        }
        
        public void Initialization()
        {
            throw new System.NotImplementedException();
        }
    }
}