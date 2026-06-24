using System.Windows.Forms;

namespace management_kos.UI
{
    internal static class ComboBoxSearchHelper
    {
        public static void EnableSearch(ComboBox comboBox)
        {
            comboBox.DropDownStyle = ComboBoxStyle.DropDown;
            comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
        }
    }
}
