namespace GameReviewApi.Domain.Response
{
    public class AuthResponse<T>
    {
        public T? Result { get; set; }
        public string? Status { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }
    }

}
