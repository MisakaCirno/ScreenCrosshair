using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace ScreenCrosshair
{
    public partial class MainForm : Form
    {
        /*
         * 窗口属性中，有一个TransparencyKey，可以设置某种颜色在窗口上是透明的
         * 本程序就以此为思路进行制作
         */

        CrosshairForm f_CrosshairForm = new CrosshairForm(); //实例化准星窗口

        Point ScreenCenterPos = new Point(0, 0);
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            CrosshairInitialization();
        }

        private void CrosshairInitialization() //准星初始化
        {
            f_CrosshairForm.StartPosition = FormStartPosition.CenterScreen;
            f_CrosshairForm.BackgroundImage = Properties.Resources.Red;
            f_CrosshairForm.Show();
            Point t_NewLocation = new Point(f_CrosshairForm.Location.X, 0);
            f_CrosshairForm.Location = t_NewLocation;

            ScreenCenterPos.X = f_CrosshairForm.Location.X+(f_CrosshairForm.Width/2);
            ScreenCenterPos.Y = 0;
            textBoxXPos.Text = f_CrosshairForm.Location.X.ToString();
            textBoxYPos.Text = f_CrosshairForm.Location.Y.ToString();
        }

        private void CrosshairChangeColor() //准星修改颜色
        {
            if (radioButton1.Checked == true)
            {
                int t_ColorChoice = comboBox1.SelectedIndex;
                switch (t_ColorChoice)
                {
                    case 0:
                        f_CrosshairForm.BackgroundImage = Properties.Resources.Red;
                        break;
                    case 1:
                        f_CrosshairForm.BackgroundImage = Properties.Resources.Green;
                        break;
                    case 2:
                        f_CrosshairForm.BackgroundImage = Properties.Resources.Blue;
                        break;
                    default:
                        f_CrosshairForm.BackgroundImage = Properties.Resources.Red;
                        break;
                }
                f_CrosshairForm.Show();
            }
            else
            {
                f_CrosshairForm.Hide();
            }
            
        }

        private void CrosshairChangePos() //准星修改坐标
        {
            Point t_NewLocation = new Point
                    (
                    Convert.ToUInt16(textBoxXPos.Text)
                    ,
                    Convert.ToUInt16(textBoxYPos.Text)
                    );
            f_CrosshairForm.Location = t_NewLocation;
        }

        private void OnlyInputNumber(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 0x20) e.KeyChar = (char)0;  //禁止空格键

            if (e.KeyChar == (char)46) e.KeyChar = (char)0;

            if ((e.KeyChar == 0x2D) && (((TextBox)sender).Text.Length == 0)) return;   //处理负数

            if (e.KeyChar > 0x20)
            {
                try { double.Parse(((TextBox)sender).Text + e.KeyChar.ToString()); }

                catch { e.KeyChar = (char)0; }
            }
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            CrosshairChangeColor();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            f_CrosshairForm.Height = Convert.ToUInt16(textBoxHeight.Text);
            f_CrosshairForm.Width = Convert.ToUInt16(textBoxWidth.Text);
            ScreenCenterPos.X = (Convert.ToUInt16(SystemParameters.PrimaryScreenWidth) / 2) - (f_CrosshairForm.Width / 2);
            f_CrosshairForm.Location = ScreenCenterPos;
            textBoxXPos.Text = f_CrosshairForm.Location.X.ToString();
            CrosshairChangeColor();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            f_CrosshairForm.Height = 600;
            f_CrosshairForm.Width = 180;
            CrosshairChangeColor();
            textBoxHeight.Text = "600";
            textBoxWidth.Text = "180";
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            CrosshairChangePos();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            //double screenWidth = ; // 屏幕整体宽度
            //double screenHeight = SystemParameters.PrimaryScreenHeight;
            ScreenCenterPos.X = (Convert.ToUInt16(SystemParameters.PrimaryScreenWidth) / 2) - (f_CrosshairForm.Width / 2);
            ScreenCenterPos.Y = f_CrosshairForm.Location.Y;
            f_CrosshairForm.Location = ScreenCenterPos;
            textBoxXPos.Text = f_CrosshairForm.Location.X.ToString();
            textBoxYPos.Text = f_CrosshairForm.Location.Y.ToString();
        }
    }
}
