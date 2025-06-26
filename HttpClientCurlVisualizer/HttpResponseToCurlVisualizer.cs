using System;
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

