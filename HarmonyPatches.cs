using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace TooManyAreas
{
    [StaticConstructorOnStartup]
    static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            new Harmony("TooManyAreas").PatchAll();
        }
    }

    [HarmonyPatch(typeof(AreaAllowedGUI), nameof(AreaAllowedGUI.DoAllowedAreaSelectors))]
    static class DoAllowedAreaSelectorsPatch
    {
        static bool Prefix(Rect rect, Pawn p)
        {
            if ((Find.CurrentMap == null) || (p.playerSettings == null)) return true;
            List<Area> allAreas = Find.CurrentMap.areaManager.AllAreas;
            if ((allAreas.Count(area => area.AssignableAsAllowed()) + 1) < LoadedModManager.GetMod<TMAMod>().GetSettings<TMASettings>().transitionValue) return true;

            return DropdownAreaAllowedGUI.DrawBox(rect, p);
        }
        
        static Exception Finalizer(Exception __exception)
        {
            if (__exception  is NullReferenceException) return null;
            return __exception;
        }
    }
}