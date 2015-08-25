using System.Windows.Forms;
using System.Drawing;

namespace Tetris
{
    class Dialog : Form
    {
        private Label textLabel;
        private TextBox textBox;
        private Button confirmationButton;

        public Dialog()
        {
            Width = 500;
            Height = 150;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterScreen;

            Font = new Font("Franklin Gothic", 15);

            textLabel = new Label() { Left = 50, Top = 15, Height = 25, Width = 400 };
            textBox = new TextBox() { Left = 50, Top = 50, Height = 25, Width = 400 };
            confirmationButton = new Button() { DialogResult = DialogResult.OK };
            confirmationButton.Click += (sender, e) => { Close(); };
            Controls.Add(textBox);
            Controls.Add(textLabel);
            AcceptButton = confirmationButton;
        }
        public string Prompt(string text, string caption)
        {
            Text = caption;
            textLabel.Text = text;
            ShowDialog();
            return textBox.Text;
        }
    }
}
