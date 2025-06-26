using System;
using System.Drawing;
using System.Windows.Forms;


namespace HttpClientToCurlVisualizer
{
    public partial class CurlVisualizerForm : Form
    {
        private TextBox textBox;
        private Button copyButton;
        private Button closeButton;

        public CurlVisualizerForm(string curlCommand)
        {
            InitializeComponent();
            textBox.Text = curlCommand;
            textBox.SelectAll();
        }

        private void InitializeComponent()
        {
            this.textBox = new TextBox();
            this.copyButton = new Button();
            this.closeButton = new Button();
            this.SuspendLayout();

            // Form
            this.ClientSize = new Size(800, 400);
            this.Text = "HTTP Request as Curl Command";
            this.StartPosition = FormStartPosition.CenterParent;
            this.MinimizeBox = false;
            this.MaximizeBox = true;

            // TextBox
            this.textBox.Location = new Point(12, 12);
            this.textBox.Size = new Size(776, 340);
            this.textBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.textBox.Multiline = true;
            this.textBox.ScrollBars = ScrollBars.Both;
            this.textBox.ReadOnly = true;
            this.textBox.Font = new Font("Consolas", 9F, FontStyle.Regular);
            this.textBox.WordWrap = false;

            // Copy Button
            this.copyButton.Location = new Point(632, 365);
            this.copyButton.Size = new Size(75, 23);
            this.copyButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.copyButton.Text = "Copy";
            this.copyButton.UseVisualStyleBackColor = true;
            this.copyButton.Click += CopyButton_Click;

            // Close Button
            this.closeButton.Location = new Point(713, 365);
            this.closeButton.Size = new Size(75, 23);
            this.closeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.DialogResult = DialogResult.OK;

            // Add controls to form
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.copyButton);
            this.Controls.Add(this.closeButton);

            this.ResumeLayout(false);
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(textBox.Text);
                MessageBox.Show("Curl command copied to clipboard!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to copy to clipboard: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}