public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // ������� ���������� ���������� middleware/�����������
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "��������� �������������� ������");

            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var result = System.Text.Json.JsonSerializer.Serialize(new { message = "������ �������" });
            await context.Response.WriteAsync(result);
        }
    }
}
