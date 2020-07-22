using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;


namespace MDHHandler
{
    class MDHHandler : Form
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MDHHandler MyForm = new MDHHandler();
            MyForm.Paint += new PaintEventHandler(MyPaint);
            Application.Run(new Form1());
        }

        static void MyPaint(Object sender, PaintEventArgs e)
        {
            e.Graphics.DrawEllipse(Pens.Blue, 10, 10, 200, 200);
        }
    }
}
