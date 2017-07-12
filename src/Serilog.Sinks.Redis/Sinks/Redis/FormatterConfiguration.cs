
namespace Serilog.Sinks.Redis
{
  public class FormatterConfiguration : IFormatterConfiguration
  {
    public bool WriteRenderedMessage { get; set; }

    public FormatterConfiguration()
    {
      WriteRenderedMessage = true;
    }
  }
}