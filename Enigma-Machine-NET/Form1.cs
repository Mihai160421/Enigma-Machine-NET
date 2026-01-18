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

        Rotor rotorRight    = new Rotor("BDFHJLCPRTXVZNYEIWGAKMUSQO", 'V'); // Rotor III
        Rotor rotorMiddle   = new Rotor("AJDKSIRUXBLHWTMCQGZNPYFVOE", 'E'); // Rotor II
        Rotor rotorLeft     = new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", 'Q'); // Rotor I
        
        string reflectorB = "YRUHQSLDPXNGOKMIEBFZCWVJAT";

        public Form1()
        {
            InitializeComponent();
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

        private void button53_Click(object sender, EventArgs e)
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

            // 1. ROTIREA (Mecanismul de tip odometru)
            // Rotorul din dreapta se învârte la fiecare tastă.
            // Dacă trece de notch, îl învârte pe cel din mijloc, etc.
            if (rotorRight.Step())
            {
                if (rotorMiddle.Step())
                {
                    rotorLeft.Step();
                }
            }

            // 2. DRUMUL DUS
            semnal = rotorRight.Forward(semnal);
            semnal = rotorMiddle.Forward(semnal);
            semnal = rotorLeft.Forward(semnal);

            // 3. REFLECTORUL
            semnal = reflectorB[semnal] - 'A';

            // 4. DRUMUL ÎNTORS
            semnal = rotorLeft.Backward(semnal);
            semnal = rotorMiddle.Backward(semnal);
            semnal = rotorRight.Backward(semnal);

            return (char)('A' + semnal);
        }

    }
}
