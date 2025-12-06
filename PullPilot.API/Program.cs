using PullPilot.Interface;
using PullPilot.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IHmacValidator, HmacValidator>();
builder.Services.AddScoped<IGitHubService, GitHubService>();
builder.Services.AddScoped<ICodeReviewService, CodeReviewService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapPost("/api/webhook/github", async (
    HttpRequest request,
    IGitHubService githubService) =>
{
    string body;
    using (var reader = new StreamReader(request.Body))
        body = await reader.ReadToEndAsync();

    var signature = request.Headers["X-Hub-Signature-256"].ToString();

    var valid = await githubService.ValidateWebhookSignatureAsync(body, signature);
    if (!valid)
        return Results.Unauthorized();

    await githubService.ProcessPullRequestAsync(body);
    return Results.Ok();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Remove or comment out HTTPS redirection
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
