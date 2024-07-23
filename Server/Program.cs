using Net6;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddCors(opts =>
{
    opts.AddPolicy("__Cors", policyBuilder =>
    {
        policyBuilder.AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<vkspDbContext>();
builder.Services.AddScoped<IConceptService, ConceptService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IDynamicService, DynamicService>();
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<ISdxBackupService, SdxBackupService>();
builder.Services.AddScoped<ICollectionConceptService, CollectionConceptService>();
builder.Services.AddScoped<IConceptTagService, ConceptTagService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("__Cors");

app.UseAuthorization();

app.MapControllers();

app.Run();

