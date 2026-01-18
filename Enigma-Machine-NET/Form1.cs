using Microsoft.VisualBasic.Devices;

namespace Enigma_Machine_NET
{
    public partial class Form1 : Form
    {
        private bool PlugboardMatching = false;
        private Color PlugboardCurrentMatchingColor = Color.White;
        private char CurrentMatchingCharacter = '\0';
        private Plugboard plugboard = new Plugboard();


        public Form1()
        {
            InitializeComponent();
        }

        private void TastaApasata_Click(object? sender, EventArgs e)
        {
            Button btn = (Button)sender!;
            char litera = btn.Text[0];

            this.keyboard_textbox.Text += litera ;

            litera = plugboard.Process(litera);
            PlugboardResult_textbox.Text += litera ;

        }


        private void Plugboard_Click(object? sender, EventArgs e)
        {
            Button btn = (Button)sender!;
            btn.Enabled = false;
            if (PlugboardMatching == false)
            {
                PlugboardMatching = true;
                /* Start Matching */

                CurrentMatchingCharacter = btn.Text[0];
                
                // Generate Random color
                Random rnd = new Random();
                PlugboardCurrentMatchingColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                btn.BackColor = PlugboardCurrentMatchingColor;
            }
            else
            {
                // Apply matching color
                btn.BackColor = PlugboardCurrentMatchingColor;

                plugboard.AddConnection(CurrentMatchingCharacter, btn.Text[0]);

                PlugboardMatching = false;
            }
        }

        private void PlugboardClear_Click(object? sender, EventArgs e)
        {
            plugboard.ClearAll();
        }
    }
}
