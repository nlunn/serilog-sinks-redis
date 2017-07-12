namespace Serilog.Sinks.Redis
{
  public interface IFormatterConfiguration
  {
    bool WriteRenderedMessage { get; set; }
  }
}