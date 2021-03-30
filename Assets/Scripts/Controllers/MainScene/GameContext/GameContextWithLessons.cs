using System.Collections.Generic;
using ListOfLessons;

namespace Diploma.Controllers
{
    public sealed class GameContextWithLessons
    {
        public Dictionary<int, ListOfLessonsView> _lessonsViews;

        public GameContextWithLessons()
        {
            _lessonsViews = new Dictionary<int, ListOfLessonsView>();
        }
        
        public void AddLessonsView(int id,ListOfLessonsView lessonsView)
        {
            _lessonsViews.Add(id,lessonsView);
        }
    }
}