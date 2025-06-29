using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coursework.Utilities
{
    public static class UIHelper
    {
        public static Label CreateLabel(string text, int x, int y, int? sizeX = null, int? sizeY = null, int fontSize = 9, bool visable = true)
        {
            var lbl = new Label
            {
                Text = text,
                Location = new Point(x, y),
                Font = new Font("Segoe UI", fontSize, FontStyle.Bold),
                Visible = visable,
                ForeColor = Color.WhiteSmoke,
            };
            if (sizeX != null && sizeY != null) lbl.Size = new Size(sizeX.Value, sizeY.Value);
            else lbl.AutoSize = true;
            return lbl;
        }

        public static TextBox CreateTextbox(string text, int x, int y, int? sizeX = null, int? sizeY = null, int fontSize = 9)
        {
            var txtBox =  new TextBox
            {
                Text = text,
                Location = new Point(x, y),
                AutoSize = true,
                Font = new Font("Segoe UI", fontSize)
            };

            if (sizeX != null && sizeY != null) txtBox.Size = new Size(sizeX.Value, sizeY.Value);
            else txtBox.AutoSize = true;
            return txtBox;
        }

        public static Panel CreateColorPanel(int x, int y)
        {
            return new Panel
            {
                Location = new Point(x, y),
                Size = new Size(20, 20),
                BackColor = Color.Transparent,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false,
                Cursor = Cursors.Hand
            };
        }

        public static Panel CreateColorPanelFromColors(Point location, Color initialColor, EventHandler onClick)
        {
            var panel = new Panel
            {
                Location = location,
                Size = new Size(20, 20),
                BackColor = initialColor,
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand
            };
            panel.Click += onClick;
            return panel;
        }
    }
}
