using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace TooManyAreas
{
    public static class DropdownAreaAllowedGUI
    {
        private static IEnumerable<Widgets.DropdownMenuElement<Area>> Button_GenerateMenu(Pawn pawn)
        {
            yield return new Widgets.DropdownMenuElement<Area>
            {
                option = new FloatMenuOption(AreaUtility.AreaAllowedLabel_Area(null), () => pawn.playerSettings.AreaRestriction = null),
                payload = null
            };
            foreach (Area area in Find.CurrentMap.areaManager.AllAreas)
            {
                Area a = area;
                if (area != pawn.playerSettings.AreaRestriction && area.AssignableAsAllowed())
                {
                    yield return new Widgets.DropdownMenuElement<Area>
                    {
                        option = new FloatMenuOption(a.Label, () => pawn.playerSettings.AreaRestriction = a, mouseoverGuiAction: (rect) => a.MarkForDraw()),
                        payload = a
                    };
                }
            }
        }

        public static bool DrawBox( Rect rect, Pawn pawn)
        {
            if (pawn.playerSettings == null) return true;

            bool flag = ((pawn?.playerSettings.AreaRestriction != null) && (Find.CurrentMap.areaManager.AllAreas.Contains(pawn.playerSettings.AreaRestriction)));
            Texture2D fillTex = !flag ? BaseContent.GreyTex : pawn.playerSettings.AreaRestriction.ColorTexture;
            string label = !flag ? AreaUtility.AreaAllowedLabel_Area(null) : pawn.playerSettings.AreaRestriction.Label;
            Rect rectRow = rect.ContractedBy(2f);
            Widgets.Dropdown(rectRow, pawn, p => flag ? p.playerSettings.AreaRestriction : null, Button_GenerateMenu, label, fillTex, dragLabel: label, paintable: true);

            if (Mouse.IsOver(rectRow))
            {
                GUI.DrawTexture(rect, BaseContent.WhiteTex);
                if (flag) pawn.playerSettings.AreaRestriction.MarkForDraw();
            }
            GUI.DrawTexture(rectRow, fillTex);
            Text.Font = GameFont.Small;
            Text.Anchor = TextAnchor.LowerCenter;
            Text.WordWrap = false;
            Widgets.Label(rectRow, label);
            Text.WordWrap = true;
            Text.Anchor = TextAnchor.UpperLeft;
            
            return false;
        }
    }
}