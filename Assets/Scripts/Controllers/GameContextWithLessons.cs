using System.Collections.Generic;
using ListOfLessons;

namespace Controllers
{
    public sealed class GameContextWithLessons
    {
        public Dictionary<int, ListOfLessonsView> _lessonsViews;

        public void AddLessonsView(int id,ListOfLessonsView lessonsView)
        {
            _lessonsViews.Add(id,lessonsView);
        }
    }
}