using System;
using System.Collections.Generic;
using Controllers;
using Diploma.Interfaces;

namespace Interfaces
{
    public interface IShowInfoFromChoosing
    {
       event Action<int> ChooseAnotherLession;
    }
}