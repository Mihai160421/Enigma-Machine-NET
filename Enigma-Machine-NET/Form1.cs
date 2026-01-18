using Microsoft.VisualBasic.Devices;

namespace Enigma_Machine_NET
{
    public partial class Form1 : Form
    {
        private bool PlugboardMatching = false;
        private Color PlugboardCurrentMatchingColor = Color.White;
        private List<Button> ButtonsMatched = new List<Button>();

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


        private void Plugboard_Click(object? sender, EventArgs e)
        {
            Button btn = (Button)sender!;
            btn.Enabled = false;
            if (PlugboardMatching == false)
            {
                PlugboardMatching = true;
                /* Start Matching */
                // Generate Random color

                Random rnd = new Random();
                PlugboardCurrentMatchingColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                btn.BackColor = PlugboardCurrentMatchingColor;
            }
            else
            {
                // Apply matching color
                btn.BackColor = PlugboardCurrentMatchingColor;
                PlugboardMatching = false;
            }

            ButtonsMatched.Append(btn);
        }

        private void PlugboardClear_Click(object? sender, EventArgs e)
        {
            /* NOT WORKING */
            foreach (Button b in ButtonsMatched)
            {
                b.BackColor = Color.Gray;
            }

            ButtonsMatched.Clear();
        }
    }
}
