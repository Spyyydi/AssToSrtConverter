using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ass_to_Srt_roles_allocator
{
    public partial class ShowLongMessage : Form
    {
        public ShowLongMessage(string message)
        {
            InitializeComponent();
            richTxtMessage.Text = message;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            bool success = false;
            bool isTried = false;
            int retries = 5;

            while (!success && retries > 0)
            {
                try
                {
                    Clipboard.SetText(richTxtMessage.Text);
                    success = true;
                }
                catch (System.Runtime.InteropServices.ExternalException)
                {
                    retries--;
                    isTried = true;
                    Thread.Sleep(100);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show($"Argument error: {ex.Message}");
                    isTried = false;
                    break; // Text is too long
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}");
                    isTried = false;
                    break;
                }
            }

            if (!success)
            {
                if (isTried)
                    MessageBox.Show($"Failed to copy report to clipboard after {retries} attempts");
            }
            else
            {
                MessageBox.Show("Report copied to clipboard");
            }
        }
    }
}
