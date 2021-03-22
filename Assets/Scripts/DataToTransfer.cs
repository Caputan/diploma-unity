using System.Collections;
using System.Collections.Generic;
using Diploma.Controllers;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "DataToTransfer")]
public class DataToTransfer : ScriptableObject
{
    public GameContextWithLessons GameContextWithLessons;
    public GameContextWithLogic GameContextWithLogic;
    public GameContextWithUI GameContextWithUI;
    public GameContextWithViews GameContextWithViews;
}
