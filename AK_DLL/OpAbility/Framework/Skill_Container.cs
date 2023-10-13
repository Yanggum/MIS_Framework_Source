﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using AKA_Ability;

namespace AK_DLL
{
    public class AK_AbilityTracker : AKAbility_Tracker
    {
        public OperatorDocument doc;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_References.Look(ref doc, "doc");
        }
    }
}
