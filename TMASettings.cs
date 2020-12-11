using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
using UnityEngine;
using Verse.Sound;

namespace TooManyAreas
{
    public class TMASettings : ModSettings
    {

        public int transitionValue = 10;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref transitionValue, "transitionValue");
            base.ExposeData();
        }
    }

    public class TMAMod : Mod
    {
        readonly TMASettings settings;

        public TMAMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<TMASettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);

            string buffer = null;
            Listing_Standard customList = new Listing_Standard();
            customList.Begin(inRect);
            customList.TextFieldNumericLabeled("Minimum number of areas to enable alternate view: ", ref settings.transitionValue, ref buffer);
            customList.End();
        }

        public override string SettingsCategory()
        {
            return "TMA: Too Many Areas";
        }
    }
}