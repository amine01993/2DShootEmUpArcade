
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(EnemySpawnProbs))]
public class EnemySpawnProbs_PropertyDrawer : PropertyDrawer
{
	public override VisualElement CreatePropertyGUI(SerializedProperty property)
	{
		var index = property.propertyPath[^2];

		var popup = new UnityEngine.UIElements.PopupWindow
		{
			text = $"Position {index}"
		};
		var flexContainer = new VisualElement();
		flexContainer.style.flexDirection = FlexDirection.Row;

		var probs = property.FindPropertyRelative("probs");

		for (var i = 0; i < probs.arraySize; ++i)
		{
			var prop = new PropertyField(probs.GetArrayElementAtIndex(i), "");
			if (i != 0)
			{
				prop.style.marginLeft = new Length(10, LengthUnit.Pixel);
			}

			prop.style.flexGrow = 1;
			flexContainer.Add(prop);
		}
		popup.Add(flexContainer);

		return popup;
	}
}
