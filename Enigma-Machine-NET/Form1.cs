using Microsoft.VisualBasic.Devices;

namespace Enigma_Machine_NET
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void TastaApasata_Click(object? sender, EventArgs e)
        {
            Button btn = (Button)sender!;
            char litera = btn.Text[0];

            this.keyboard_textbox.Text += litera ;
        }
    }
}
