using System.Net;

namespace Catalog.API.Models;

public record ResultResponse<T>(T Data, HttpStatusCode StatusCode, string? ErrorMessage = null, string? StackTrace = null);

