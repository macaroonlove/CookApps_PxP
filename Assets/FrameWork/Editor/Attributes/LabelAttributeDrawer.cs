using UnityEditor;
using UnityEngine;

namespace FrameWork.Editor
{
    [CustomPropertyDrawer(typeof(LabelAttribute))]
    public class LabelAttributeDrawer : PropertyDrawer
    {
#if UNITY_EDITOR
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            LabelAttribute labelAttribute = (LabelAttribute)attribute;

            // �ν����Ϳ� ǥ��� �̸� �ٲٱ�
            label.text = labelAttribute.label;

            // ������Ƽ �׸���
            EditorGUI.PropertyField(position, property, label, true);
        }
#endif
    }
}