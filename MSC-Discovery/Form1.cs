using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zeroconf;

namespace MSC_Discovery
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Scan Started");
            label3.Text = "Scanning...";
            String[] searchableProtocols = ["_msp1._tcp.local.", "_msp2._tcp.local."];

            IReadOnlyList<IZeroconfHost> results = await
                    ZeroconfResolver.ResolveAsync(searchableProtocols);
            MercuryController[] controllers = new MercuryController[results.Count];
            //MessageBox.Show("Found " + results.Count.ToString() + " controllers");

            Dictionary<String, MercuryController> controllerList = new Dictionary<string, MercuryController>();
            List<ListViewItem> listViewItems = new List<ListViewItem>();

            foreach (IZeroconfHost result in results)
            {
                MercuryController tmpController = new MercuryController(result);
                if (controllerList.ContainsKey(tmpController.macAddress)) {
                    break;
                }
                controllerList.Add(tmpController.macAddress, tmpController);
                ListViewItem tmpItem = new ListViewItem(new string[]
                {
                    tmpController.hostName,
                    tmpController.ipAddress,
                    tmpController.macAddress,
                    tmpController.serialNumber,
                    tmpController.firmwareVersion,
                    tmpController.boardType
                });

                listViewItems.Add(tmpItem);
                //MessageBox.Show(tmpController.hostName);
            }

            listView1.Items.Clear();
            listView1.Items.AddRange(listViewItems.ToArray());

            label3.Text = "Scan Complete";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            label3.Text = "Cleared, Please Start";
            listView1.Items.Clear();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://rexfault.net");

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://rexfault.net");
        }

        private void linkLabel1_LinkClicked(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Process.Start("http://rexfault.net");
        }
    }
}
