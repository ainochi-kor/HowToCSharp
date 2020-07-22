using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MDHHandler2
{
    class MDHHandler : Form  
    {
        public static void Main()
        {
            MDHHandler MyForm = new MDHHandler();
            MyForm.Paint += new PaintEventHandler(MyPaint);
            Application.Run(MyForm);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawString("OnPaint 메서드 호출", Font, Brushes.Black, 10, 10);
        }

        static void MyPaint(Object sender, PaintEventArgs e)
        {
            e.Graphics.DrawEllipse(Pens.Blue, 10, 10, 200, 200);
        }
    }
}
