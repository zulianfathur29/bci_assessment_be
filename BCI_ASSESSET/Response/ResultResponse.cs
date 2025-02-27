﻿namespace BCI_ASSESSET.Response
{
    public class ResultResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ResultResponse(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}
