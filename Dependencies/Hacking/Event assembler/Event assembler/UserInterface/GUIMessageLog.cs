using System.Windows.Forms;
using Nintenlord.Event_Assembler.Core.IO.Logs;

namespace Nintenlord.Event_Assembler.UserInterface
{
    class GUIMessageLog : MessageLog
    {
        public override void PrintAll()
        {
            int longestLine;
            string message = this.GetText(out longestLine);
            using (TextShower shower = new TextShower(message))
            {
                shower.Text = "";
                shower.Width = 7 * longestLine;
                shower.ShowDialog();
            }
        }
    }
}
