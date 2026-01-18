namespace Enigma_Machine_NET
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            InitializeKeyboard();
        }

        private void InitializeKeyboard()
        {
            // Keyboard Layout
            string[] rows = {   
                "QWERTZUIOP", 
                "ASDFGHJKL", 
                "PYXCVBNM" 
            };

            for (int r = 0; r < rows.Length; r++)
            {
                for (int i = 0; i < rows[r].Length; i++)
                {
                    const int _bsize = 200;
                    const int _padding = 5;

                    Button btn = new Button();

                    btn.Text = rows[r][i].ToString();
                    btn.Width = _bsize;
                    btn.Height = _bsize;
                    
                    btn.Left = 50 + (i * _bsize+ _padding) + (r * 20);
                    btn.Top = 100 + (r * _bsize+ _padding);

                    btn.Click += TastaApasata_Click;

                    this.Controls.Add(btn);
                }
            }
        }

        private void TastaApasata_Click(object? sender, EventArgs e)
        {
            Button btn = (Button)sender!;
            char litera = btn.Text[0];
        }
    }
}
