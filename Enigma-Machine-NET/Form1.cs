using System.Diagnostics;
using Microsoft.VisualBasic.Devices;

namespace Enigma_Machine_NET
{
    public partial class Form1 : Form
    {
        private bool PlugboardMatching = false;
        private Color PlugboardCurrentMatchingColor = Color.White;
        private char CurrentMatchingCharacter = '\0';
        private List<Button> PlugBoardSelectedButtons = new List<Button>();

        private Plugboard plugboard = new Plugboard();

        Rotor rotorRight = new Rotor("BDFHJLCPRTXVZNYEIWGAKMUSQO"); // Rotor III
        Rotor rotorMiddle = new Rotor("AJDKSIRUXBLHWTMCQGZNPYFVOE"); // Rotor II
        Rotor rotorLeft = new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ"); // Rotor I

        string reflectorB = "YRUHQSLDPXNGOKMIEBFZCWVJAT";

        public Form1()
        {
            InitializeComponent();

            lblRotor1.MouseWheel += LabelRightRotor_MouseWheel;
            lblRotor1.MouseEnter += (s, e) => lblRotor1.Focus();

            lblRotor2.MouseWheel += LabelMiddleRotor_MouseWheel;
            lblRotor2.MouseEnter += (s, e) => lblRotor2.Focus();

            lblRotor3.MouseWheel += LabelLeftRotor_MouseWheel;
            lblRotor3.MouseEnter += (s, e) => lblRotor3.Focus();
        }

        private void TastaApasata_Click(object? sender, EventArgs e)
        {
            Button btn = (Button)sender!;
            char litera = btn.Text[0];

            this.keyboard_textbox.Text += litera;

            litera = plugboard.Process(litera);
            PlugboardResult_textbox.Text += litera;


            litera = Encrypt(litera);

            litera = plugboard.Process(litera);

            this.encrypted_text.Text += litera;

        }
        private void Plugboard_Click(object? sender, EventArgs e)
        {
            Button btn = (Button)sender!;
            btn.Enabled = false;
            if (PlugboardMatching == false)
            {
                PlugboardMatching = true;

                // Get character
                CurrentMatchingCharacter = btn.Text[0];

                // Generate Random color
                Random rnd = new Random();
                PlugboardCurrentMatchingColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));

                btn.BackColor = PlugboardCurrentMatchingColor;
            }
            else
            {
                PlugboardMatching = false;


                // Apply matching color
                btn.BackColor = PlugboardCurrentMatchingColor;
                // Add the connection
                plugboard.AddConnection(CurrentMatchingCharacter, btn.Text[0]);

            }
            PlugBoardSelectedButtons.Add(btn);

        }

        private void PlugboardClear_Click(object sender, EventArgs e)
        {
            foreach (Button btn in PlugBoardSelectedButtons)
            {
                btn.Enabled = true;
                btn.BackColor = Color.White;
            }
            PlugBoardSelectedButtons.Clear();
            PlugboardMatching = false;
            plugboard.ClearAll();
        }

        public char Encrypt(char c)
        {
            int semnal = c - 'A';

            if (rotorRight.Step())
            {
                if (rotorMiddle.Step())
                {
                    rotorLeft.Step();
                }
            }

            lblRotor1.Text = (rotorRight.Position + 1).ToString("D2");
            lblRotor2.Text = (rotorMiddle.Position + 1).ToString("D2");
            lblRotor3.Text = (rotorLeft.Position + 1).ToString("D2");


            semnal = rotorRight.Forward(semnal);
            semnal = rotorMiddle.Forward(semnal);
            semnal = rotorLeft.Forward(semnal);

            semnal = reflectorB[semnal] - 'A';

            semnal = rotorLeft.Backward(semnal);
            semnal = rotorMiddle.Backward(semnal);
            semnal = rotorRight.Backward(semnal);

            return (char)('A' + semnal);
        }

        private void LabelRightRotor_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                rotorRight.Step();
            }
            else
            {
                int pos = (rotorRight.Position - 1 + 26) % 26;
                rotorRight.SetPosition(pos);
            }

            lblRotor1.Text = (rotorRight.Position + 1).ToString("D2");
        }

        private void LabelMiddleRotor_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                rotorMiddle.Step();
            }
            else
            {
                int pos = (rotorMiddle.Position - 1 + 26) % 26;
                rotorMiddle.SetPosition(pos);
            }

            lblRotor2.Text = (rotorMiddle.Position + 1).ToString("D2");
        }

        private void LabelLeftRotor_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                rotorLeft.Step();
            }
            else
            {
                int pos = (rotorLeft.Position - 1 + 26) % 26;
                rotorLeft.SetPosition(pos);
            }

            lblRotor3.Text = (rotorLeft.Position + 1).ToString("D2");
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            rotorLeft.SetPosition(0);
            rotorMiddle.SetPosition(0);
            rotorRight.SetPosition(0);
            lblRotor1.Text = "01";
            lblRotor2.Text = "01";
            lblRotor3.Text = "01";

            keyboard_textbox.Text = "";
            encrypted_text.Text = "";
            PlugboardResult_textbox.Text = "";

            PlugboardClear_Click(sender, e);
        }
    }
}
