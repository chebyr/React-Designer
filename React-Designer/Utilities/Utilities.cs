using System.Windows.Forms;

namespace ReactDesigner
{
    public static class Utilities
    {
        public static void ShowExceptionMessage(string message)
        {
            MessageBox.Show(
                message, "ReactDesigner Exception:",
                MessageBoxButtons.OK, MessageBoxIcon.Error
                );
        }
    }
}
