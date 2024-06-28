namespace RestApi.DTOs
{
    public class LogDto
    {
        public int Idlog { get; set; }

        public DateTime Timestamp { get; set; }

        public int Level { get; set; }

        public string Message { get; set; } = null!;
    }
}
