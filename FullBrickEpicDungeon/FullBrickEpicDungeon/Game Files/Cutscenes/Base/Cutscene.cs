﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//staat weinig in, maar is voor mogelijke uitbreiding met bewegende sprites aangemaakt
class Cutscene : GameObjectList
{ 
    public Cutscene(string path)
    {
        SpriteGameObject cutscene = new SpriteGameObject(path, 0, "", 0, false);
        Add(cutscene);
    }

}

