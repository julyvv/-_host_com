using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        
        int[] bauds = { 2400, 4800, 9600, 19200, 38400, 57600, 115200 };

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load_1(object sender,EventArgs e)
        {
           
            string[] Coms = SerialPort.GetPortNames();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            foreach (string comStr in Coms)
            {
                comboBox1.Items.Add(comStr);
            }
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
            foreach(int n_baud in bauds)
            {
                comboBox2.Items.Add(n_baud + "");
            }
            comboBox2.Text = "115200";
          
        }
     
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
      
            if (!serialPort1.IsOpen)
            {
                serialPort1.PortName = comboBox1.Text;
                serialPort1.BaudRate = int.Parse(comboBox2.Text);
                try
                {
                    serialPort1.Open();
                    button1.Text = "点击关串口";
                    textBox1.AppendText("start\r\n");
                    comboBox1.Enabled = false;
                    comboBox2.Enabled = false;
                }
                catch
                {
                    textBox1.AppendText("串口打开失败！！\r\n");
                }
            }
            else
            {
                serialPort1.Close();
                button1.Text = "点击开串口";
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            DateTime current_time = System.DateTime.Now;     //获取当前时间
            if (radioButton2.Checked)
            {
                string str = serialPort1.ReadExisting();
             
                
                this.Invoke((EventHandler)(delegate
                {
                    textBox1.AppendText(current_time.ToString("HH:mm:ss") + "  ");
                    textBox1.AppendText(str+"\r\n");
                }
                   )
                );
            }
            if(radioButton1.Checked)
            {
                int num = serialPort1.BytesToRead;
                byte[] data=new byte[num];
                string returnStr = "";
                serialPort1.Read(data, 0, num);
                for (int i = 0; i < num; i++)
                {
                  returnStr += data[i].ToString("X2");
                } 
                this.Invoke((EventHandler)(delegate
                {
                    textBox1.AppendText(current_time.ToString("HH:mm:ss") + "  ");
                    textBox1.AppendText(returnStr+"\r\n");

                }
                   )
                );
            }
        }
       
        private void label3_Click(object sender, EventArgs e)
        {
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
           if(serialPort1.IsOpen)
            {
                if(textBox2.Text.Trim()!="")
                {
                    serialPort1.Write(textBox2.Text.Trim());
                }
                else
                {
                    textBox1.AppendText("发送框无数据\r\n");
                }
            }
           else
            {
                textBox1.AppendText("串口未打开\r\n");
            }
           
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
