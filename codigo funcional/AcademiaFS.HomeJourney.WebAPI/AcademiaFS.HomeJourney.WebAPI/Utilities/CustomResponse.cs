namespace AcademiaFS.HomeJourney.WebAPI.Utilities
{
    public class CustomResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
