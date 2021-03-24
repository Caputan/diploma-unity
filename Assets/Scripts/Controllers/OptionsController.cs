using Diploma.Controllers;
using Diploma.Interfaces;
using UnityEngine.Audio;

namespace Controllers
{
    public class OptionsController: IInitialization
    {
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly AudioMixer _audioMixer;

        public OptionsController(GameContextWithViews gameContextWithViews, AudioMixer audioMixer)
        {
            _gameContextWithViews = gameContextWithViews;
            _audioMixer = audioMixer;
        }
        
        public void Initialization()
        {
            // положение слайдера и проценты
        }
    }
}