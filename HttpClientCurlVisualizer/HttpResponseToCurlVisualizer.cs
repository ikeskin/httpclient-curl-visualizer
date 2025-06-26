using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: System.Diagnostics.DebuggerVisualizer(
    typeof(HttpClientToCurlVisualizer.HttpResponseToCurlVisualizer),
    typeof(HttpClientToCurlVisualizer.HttpResponseToCurlVisualizerObjectSource),
    Target = typeof(HttpResponseMessage),
    Description = "HTTP Response to Curl")]

namespace HttpClientToCurlVisualizer
{
    public class HttpResponseToCurlVisualizer : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            var curlCommand = (string)objectProvider.GetObject();
            
            var form = new CurlVisualizerForm(curlCommand);
            windowService.ShowDialog(form);
        }
    }

    public class HttpResponseToCurlVisualizerObjectSource : VisualizerObjectSource
    {
        public override void GetData(object target, System.IO.Stream outgoingData)
        {
            var response = (HttpResponseMessage)target;
            var curlCommand = ConvertToCurl(response);
            
            var writer = new System.IO.StreamWriter(outgoingData);
            writer.Write(curlCommand);
            writer.Flush();
        }

        private string ConvertToCurl(HttpResponseMessage response)
        {
            try
            {
                var request = response.RequestMessage;
                if (request == null)
                {
                    return "# Request information not available in response";
                }

                var sb = new StringBuilder();
                sb.Append("curl");

                // HTTP Method
                if (request.Method != HttpMethod.Get)
                {
                    sb.Append($" -X {request.Method.Method}");
                }

                // URL
                sb.Append($" '{request.RequestUri}'");

                // Headers
                if (request.Headers != null)
                {
                    foreach (var header in request.Headers)
                    {
                        foreach (var value in header.Value)
                        {
                            sb.Append($" -H '{header.Key}: {value}'");
                        }
                    }
                }

                // Content headers and body
                if (request.Content != null)
                {
                    // Content headers
                    foreach (var header in request.Content.Headers)
                    {
                        foreach (var value in header.Value)
                        {
                            sb.Append($" -H '{header.Key}: {value}'");
                        }
                    }

                    // Content body
                    try
                    {
                        var content = request.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(content))
                        {
                            // Escape single quotes in content
                            content = content.Replace("'", "'\"'\"'");
                            sb.Append($" -d '{content}'");
                        }
                    }
                    catch
                    {
                        sb.Append(" # Content body could not be read");
                    }
                }

                return sb.ToString();
            }
            catch (Exception ex)
            {
                return $"# Error generating curl command: {ex.Message}";
            }
        }
    }
}

// CurlVisualizerForm.cs
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