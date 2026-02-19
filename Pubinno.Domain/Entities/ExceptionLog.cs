namespace Pubinno.Domain.Entities
{
    public class ExceptionLog : BaseEntity
    {
        public string Path { get; set; } = null!;
        public string Method { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string? StackTrace { get; set; }
        public int StatusCode { get; set; }
    }
}
