using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using ZLinq;

namespace PizzaMaker
{
    public class MenuItemPropertyDrawer : OdinValueDrawer<MenuItem>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            Rect rect = EditorGUILayout.GetControlRect();
            if (label != null)
                rect = EditorGUI.PrefixLabel(rect, label);
            var pizzaNameEnum = MenuItem.All.AsValueEnumerable();
            var allPizzaMenuName = pizzaNameEnum.Select(pm => pm.name);
            var selectedIndex = 0;
            if (!string.IsNullOrEmpty(ValueEntry.SmartValue.name))
            {
               selectedIndex = allPizzaMenuName.ToList().IndexOf(ValueEntry.SmartValue.name);
            }

            if (selectedIndex == -1)
                selectedIndex = 0;
            
            selectedIndex = EditorGUI.Popup(rect, selectedIndex, allPizzaMenuName.ToArray());
            ValueEntry.SmartValue = MenuItem.All[selectedIndex];
        }
    }
}