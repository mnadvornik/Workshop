﻿namespace Workshop
{
    using System.Linq;
    using System.Text;

    using UnityEngine;

    public class WorkshopGui
    {
        public static void ProgressBar(float progress)
        {
            GUILayout.Box("", GUILayout.Width(750), GUILayout.Height(50));
            var boxRect = GUILayoutUtility.GetLastRect();
            if (progress >= 1)
            {
                var color = GUI.color;
                GUI.color = new Color(0, 1, 0, 1);
                GUI.Box(new Rect(boxRect.xMin, boxRect.yMin, boxRect.width * progress / 100, boxRect.height), "");
                GUI.color = color;
            }
            GUI.Label(boxRect, " " + progress.ToString("0.0") + " / 100", WorkshopStyles.Center());
        }

        public static void ItemThumbnail(WorkshopItem item)
        {
            GUILayout.BeginVertical();
            GUILayout.Box("", GUILayout.Width(50), GUILayout.Height(50));
            var textureRect = GUILayoutUtility.GetLastRect();
            GUI.DrawTexture(textureRect, item.Icon.texture, ScaleMode.ScaleToFit);
            GUILayout.EndVertical();
        }

        public static void ItemDescription(WorkshopItem item)
        {
            GUILayout.BeginVertical();
            var text = new StringBuilder();
            text.AppendLine(item.Part.title);
            var totalRatio = item.Part.partPrefab.GetComponent<OseModuleRecipe>().TotalRatio;
            foreach (var demand in item.Part.partPrefab.GetComponent<OseModuleRecipe>().Demand)
            {
                var density = PartResourceLibrary.Instance.GetDefinition(demand.ResourceName).density;
                var requiredResources = item.Part.partPrefab.mass * (demand.Ratio / totalRatio) / density;
                text.AppendLine(" " + requiredResources + " " + demand.ResourceName);
            }
            GUILayout.Box(text.ToString(), WorkshopStyles.Databox(), GUILayout.Width(250), GUILayout.Height(50));
            GUILayout.EndVertical();
        }

        public static bool FilterButton(FilterBase filter, Rect position)
        {
            var texture = GameDatabase.Instance.databaseTexture.Single(t => t.name == filter.TexturePath).texture;
            return GUI.Button(position, texture, WorkshopStyles.Button());
        }
    }
}
