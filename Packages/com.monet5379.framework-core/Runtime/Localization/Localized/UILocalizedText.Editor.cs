using TMPro;

namespace Monet5379
{
    public partial class UILocalizedText
    {
        public override void AutoGetComponents()
        {
            base.AutoGetComponents();
            EnsureTextMeshProAssigned();
        }

        private void EnsureTextMeshProAssigned()
        {
            if (_textMeshPro == null)
            {
                _textMeshPro = GetComponent<TextMeshProUGUI>();
            }
        }

        private void OnValidate()
        {
            string hierarchyPath = this.GetHierarchyPath();
            Font.ValidateEnumStrings(hierarchyPath);
            Typography.ValidateEnumStrings(hierarchyPath);
        }

        public override void AutoSetting()
        {
            base.AutoSetting();

            Content.AutoSettingStringKey();
            Font.AutoSetting();
            Typography.AutoSetting();
        }
    }
}
