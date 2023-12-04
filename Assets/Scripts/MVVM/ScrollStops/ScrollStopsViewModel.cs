using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollStopsViewModel
{
    public int stopsCount;


    public ScrollStopsViewModel(ScrollStopsModel model)
    {
        stopsCount = model.number;
    }




}
