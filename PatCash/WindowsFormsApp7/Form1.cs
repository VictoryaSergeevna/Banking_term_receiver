using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp7
{
    public partial class Form1 : Form
    {
        Penny penny = new Penny();
        Client client = new Client();
        Handler fivePenny = new FivePennyHandler();
        Handler tenPenny = new TenPennyHandler();
        Handler twentyFivePenny = new TwentyFivePennyHandler();
        Handler fiftyPenny = new FiftyPennyHandler();
        Handler oneGrnPenny = new OneGrnPennyHandler();
        ImageList list = null;
        ToolBar toolBar = null;
        public Form1()
        {
            InitializeComponent();
            fivePenny.Successor = tenPenny;
            tenPenny.Successor = twentyFivePenny;
            twentyFivePenny.Successor = fiftyPenny;
            fiftyPenny.Successor = oneGrnPenny;
            list = new ImageList();
            list.ImageSize = new Size(50, 50);
            list.Images.Add(new Bitmap("5.bmp"));
            list.Images.Add(new Bitmap("10.bmp"));
            list.Images.Add(new Bitmap("25.bmp"));
            list.Images.Add(new Bitmap("50.bmp"));
            list.Images.Add(new Bitmap("1.bmp"));
            toolBar = new ToolBar();
            toolBar.Appearance = ToolBarAppearance.Flat;
            toolBar.BorderStyle = BorderStyle.Fixed3D;
            toolBar.ImageList = list;
            ToolBarButton toolBarButton1 = new ToolBarButton();
            toolBarButton1.Tag = "5";
            ToolBarButton toolBarButton2 = new ToolBarButton();
            toolBarButton2.Tag = "10";
            ToolBarButton toolBarButton3 = new ToolBarButton();
            toolBarButton3.Tag = "25";
            ToolBarButton toolBarButton4 = new ToolBarButton();
            toolBarButton4.Tag = "50";
            ToolBarButton toolBarButton5 = new ToolBarButton();
            toolBarButton5.Tag = "1";      
            

            toolBarButton1.ImageIndex = 0;
            toolBarButton2.ImageIndex = 1;
            toolBarButton3.ImageIndex = 2;
            toolBarButton4.ImageIndex = 3;
            toolBarButton5.ImageIndex = 4;
           

            toolBar.Buttons.Add(toolBarButton1);
            toolBar.Buttons.Add(toolBarButton2);
            toolBar.Buttons.Add(toolBarButton3);
            toolBar.Buttons.Add(toolBarButton4);
            toolBar.Buttons.Add(toolBarButton5);          
            this.Controls.Add(toolBar);
            toolBar.ButtonClick += ToolBar_ButtonClick;

        }

        private void ToolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            switch (e.Button.Tag)
            {
                case "5":
                    MessageBox.Show("Диаметр: 24 / Вес: 4", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "10":
                    MessageBox.Show("Диаметр: 16 / Вес: 2", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "25":
                    MessageBox.Show("Диаметр: 21 / Вес: 3", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "50":
                    MessageBox.Show("Диаметр: 23 / Вес: 4", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "1":
                    MessageBox.Show("Диаметр: 26 / Вес: 7", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                default:
                    break;
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            penny.Weight = Convert.ToInt32(textBoxDiametr.Text);
            penny.Diameter = Convert.ToInt32(textBoxWeight.Text);
            fivePenny.HandleRequest(penny, client);
            if (client.Sum >= 100)
            {
                textBoxSum.Text = (client.Sum / 100).ToString() + " грн." + (client.Sum % 100).ToString() + " коп";
            }
            else
            {
                textBoxSum.Text = client.Sum.ToString() + " коп";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            textBoxDiametr.Clear();
            textBoxWeight.Clear();
        }

       


        public class Penny
        {
            public int Weight { get; set; }
            public int Diameter { get; set; }
        }

        abstract class Handler
        {
            public Handler Successor { get; set; }
            public abstract void HandleRequest(Penny penny, Client client);
        }

        class FivePennyHandler : Handler
        {
            public override void HandleRequest(Penny penny, Client client)
            {
                if (penny.Weight == 24 && penny.Diameter == 4)
                {
                    client.Sum += 5;
                }
                else if (Successor != null)
                {
                    Successor.HandleRequest(penny, client);
                }
            }
        }
        class TenPennyHandler : Handler
        {
            public override void HandleRequest(Penny penny, Client client)
            {
                if (penny.Weight == 16 && penny.Diameter == 2)
                {
                    client.Sum += 10;
                }
                else if (Successor != null)
                {
                    Successor.HandleRequest(penny, client);
                }
            }
        }
        class TwentyFivePennyHandler : Handler
        {
            public override void HandleRequest(Penny penny, Client client)
            {
                if (penny.Weight == 21 && penny.Diameter == 3)
                {
                    client.Sum += 25;
                }
                else if (Successor != null)
                {
                    Successor.HandleRequest(penny, client);
                }
            }
        }
        class FiftyPennyHandler : Handler
        {
            public override void HandleRequest(Penny penny, Client client)
            {
                if (penny.Weight == 23 && penny.Diameter == 4)
                {
                    client.Sum += 50;
                }
                else if (Successor != null)
                {
                    Successor.HandleRequest(penny, client);
                }
            }
        }
        class OneGrnPennyHandler : Handler
        {
            public override void HandleRequest(Penny penny, Client client)
            {
                if (penny.Weight == 26 && penny.Diameter == 7)
                {
                    client.Sum += 100;
                }
                else if (Successor != null)
                {
                    Successor.HandleRequest(penny, client);
                }
                else
                {
                    MessageBox.Show("Введите правильный вес и диаметр монеты!", "Монеты", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                }
            }
        }
        class Client
        {
            public int Sum { get; set; }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            textBoxDiametr.Clear();
            textBoxWeight.Clear();
            textBoxSum.Clear();
        }
    }
}

