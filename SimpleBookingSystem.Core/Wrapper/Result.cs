﻿namespace SimpleBookingSystem.Core.Wrapper
{
    public class Result : IResult
    {
        public Result()
        {
        }

        public List<string> Messages { get; set; } = new List<string>();

        public bool Succeeded { get; set; }

        public static Result Fail() => new() { Succeeded = false };

        public static Result Fail(string message) => new() { Succeeded = false, Messages = new List<string> { message } };

        public static Result Fail(List<string> messages) => new() { Succeeded = false, Messages = messages };

        public static Task<Result> FailAsync() => Task.FromResult(Fail());

        public static Task<Result> FailAsync(string message) => Task.FromResult(Fail(message));

        public static Task<Result> FailAsync(List<string> messages) => Task.FromResult(Fail(messages));

        public static Result Success() => new() { Succeeded = true };

        public static Result Success(string message) => new() { Succeeded = true, Messages = new List<string> { message } };

        public static Task<Result> SuccessAsync() => Task.FromResult(Success());

        public static Task<Result> SuccessAsync(string message) => Task.FromResult(Success(message));
    }

    public class Result<T> : Result, IResult<T>
    {
        public Result()
        {
        }

        public T? Data { get; set; }

        public new static Result<T> Fail() => new() { Succeeded = false };

        public new static Result<T> Fail(string message) => new Result<T> { Succeeded = false, Messages = new List<string> { message } };

        public new static Result<T> Fail(List<string> messages) => new Result<T> { Succeeded = false, Messages = messages };

        public new static Task<Result<T>> FailAsync() => Task.FromResult(Fail());

        public new static Task<Result<T>> FailAsync(string message) => Task.FromResult(Fail(message));

        public new static Task<Result<T>> FailAsync(List<string> messages) => Task.FromResult(Fail(messages));

        public new static Result<T> Success() => new Result<T> { Succeeded = true };

        public new static Result<T> Success(string message) => new() { Succeeded = true, Messages = new List<string> { message } };

        public static Result<T> Success(T data) => new() { Succeeded = true, Data = data };

        public static Result<T> Success(T data, string message) => new() { Succeeded = true, Data = data, Messages = new List<string> { message } };

        public static Result<T> Success(T data, List<string> messages) => new() { Succeeded = true, Data = data, Messages = messages };

        public new static Task<Result<T>> SuccessAsync() => Task.FromResult(Success());

        public new static Task<Result<T>> SuccessAsync(string message) => Task.FromResult(Success(message));

        public static Task<Result<T>> SuccessAsync(T data) => Task.FromResult(Success(data));

        public static Task<Result<T>> SuccessAsync(T data, string message) => Task.FromResult(Success(data, message));
    }
}
