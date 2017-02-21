namespace SerilogToElkExample
{
  public interface IFormatterConfiguration
  {
    bool WriteRenderedMessage { get; set; }
  }
}