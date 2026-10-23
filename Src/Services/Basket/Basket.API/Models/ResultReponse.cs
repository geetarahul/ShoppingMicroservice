using System.Net;

namespace Basket.API.Models;

public record ResultResponse<T>(T Data, HttpStatusCode StatusCode, string? ErrorMessage = null, string? StackTrace = null);

