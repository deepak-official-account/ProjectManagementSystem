public class ResponseDto
{
    public bool IsSuccess { get; set; } = false;
    public string Message { get; set; }= string.Empty;
    public object Data { get; set; }

    public ResponseDto()
    {
    }


    public ResponseDto(bool success, string message, object data)
    {
        IsSuccess = success;
        Message = message;
        Data = data;
    }
}