using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ListViewData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        struct City
        {
            public string name;
            public int area;
            public int popu;
            public City(string name, int area, int popu)
            {
                this.name = name;
                this.area = area;
                this.popu = popu;
            }
        }

        City[] arCity = 
        {
            new City("서울", 605,1026),
            new City("부산", 758,381),
            new City("용인", 591,583),
            new City("춘천", 1116,25),
            new City("홍천", 1817,7)
        };

        

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach(City C in arCity)
            {
                listView1.Items.Add(new ListViewItem(new string[] {C.name,
                    C.area.ToString(), C.popu.ToString() }));
            }

            listView2.View = View.Details;

            ColumnHeader header1, header2;
            header1 = new ColumnHeader();
            header2 = new ColumnHeader();

            header1.Text = "File name";
            header1.TextAlign = HorizontalAlignment.Left;
            header1.Width = 70;

            header2.TextAlign = HorizontalAlignment.Left;
            header2.Text = "Location";
            header2.Width = 200;

            listView2.Columns.Add(header1);
            listView2.Columns.Add(header2);

            /*
            foreach(City C in arCity)
            {
                listView2.Items.Add(new ListViewItem(new string[] {C.name,
                    C.area.ToString(), C.popu.ToString() }));
            }

            listView2.Columns.Add("Item Column", -2, HorizontalAlignment.Left);
            */

            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count==0)
            {
                Text = "선택된 항목이 없습니다.";
            }
            else
            {
                MessageBox.Show(listView1.SelectedItems[0].Text);
            }
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            MessageBox.Show(listView1.SelectedItems[0].Text);
        }


    }
}
