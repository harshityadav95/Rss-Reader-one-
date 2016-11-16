using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Rss_Reader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        String[,] rssData = null;
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private String[,] getRssData(String channel)
        {
            System.Net.WebRequest myrequest = System.Net.WebRequest.Create(channel);
            System.Net.WebResponse myresponse = myrequest.GetResponse();


            System.IO.Stream rssStream = myresponse.GetResponseStream();
            System.Xml.XmlDocument rssDoc = new System.Xml.XmlDocument();

            rssDoc.Load(rssStream);
            System.Xml.XmlNodeList rssItems = rssDoc.SelectNodes("rss/channel/items");
            String[,] tempRssData = new String[100, 3];

            for (int i = 0; i < rssItems.Count; i++)
            {
                System.Xml.XmlNode rssNode;

                rssNode = rssItems.Item(i).SelectSingleNode("title");
                if (rssNode != null)
                {
                    tempRssData[i, 0] = rssNode.InnerText;

                }
                else
                {
                    tempRssData[i, 0] = "";

                }
                rssNode = rssItems.Item(i).SelectSingleNode("descryption");
                if (rssNode != null)
                {
                    tempRssData[i, 1] = rssNode.InnerText;

                }
                else
                {
                    tempRssData[i, 1] = "";

                }
                rssNode = rssItems.Item(i).SelectSingleNode("link");
                if (rssNode != null)
                {
                    tempRssData[i, 2] = rssNode.InnerText;
                }
                else
                {
                    tempRssData[i, 2] = "";

                }
            }
            return tempRssData;


        }


        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            rssData=getRssData(textBox1.Text);
            for (int i = 0; i < rssData.GetLength(0); i++)
            {
                if (rssData[i, 0] != null)
                {
                    comboBox1.Items.Add(rssData[i, 0]);
                }
                comboBox1.SelectedIndex = 0;


            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rssData[comboBox1.SelectedIndex, 1] != null)
            {
                textBox2.Text = rssData[comboBox1.SelectedIndex, 1];

            }
            if (rssData[comboBox1.SelectedIndex, 2] != null)
            {
                linkLabel1.Text = "Go to" + rssData[comboBox1.SelectedIndex, 0];
            }   
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (rssData[comboBox1.SelectedIndex, 2] != null)
                System.Diagnostics.Process.Start(rssData[comboBox1.SelectedIndex,2]);

        }

        //http://go.microsoft.com/fwlink/?linkid=85889&clcid=409
    }
}

