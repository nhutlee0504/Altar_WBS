using ALtar_WBS.Data;
using ALtar_WBS.Interface;
using ALtar_WBS.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Title", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter 'Bearer' [space] and then your token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, new string[] { } }
    });
});

// Lấy cấu hình JWT từ appsettings.json
var jwtSection = builder.Configuration.GetSection("JWT"); // Sửa Configuration thành builder.Configuration
var key = Encoding.UTF8.GetBytes(jwtSection["Secret"]);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSection["ValidIssuer"],
        ValidAudience = jwtSection["ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});


builder.Services.AddDbContext<ApplicationDbContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddScoped<InterfaceAuth, ServiceAuth>();
builder.Services.AddScoped<InterfaceRole, ServiceRole>();
builder.Services.AddScoped<InterfaceUser, ServiceUser>();
builder.Services.AddScoped<InterfaceNotification, ServiceNotification>();
builder.Services.AddScoped<InterfaceTeacher, ServiceTeacher>();
builder.Services.AddScoped<InterfaceStudent, ServiceStudent>();
builder.Services.AddScoped<ISubjectCategory, SubjectCategoryService>();
builder.Services.AddScoped<InterfaceSubject, ServiceSubject>();
builder.Services.AddScoped<InterfaceCourse, ServiceCourse>();
builder.Services.AddScoped<InterfaceTeacherSalary, ServiceTeacherSalary>();
builder.Services.AddScoped<InterfaceCourseSubject, ServiceCourseSubject>();
builder.Services.AddScoped<InterfacePayment, ServicePayment>();
builder.Services.AddScoped<InterfaceSchedule, ServiceSchedule>();
builder.Services.AddScoped<InterfaceClasses, ServiceClasses>();
builder.Services.AddScoped<InterfaceClassTeacher, ServiceClassTeacher>();
builder.Services.AddScoped<InterfaceAttendance, AttendanceService>();
builder.Services.AddScoped<InterfaceGrade, ServiceGrade>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();
builder.Services.AddScoped<InterfaceEnrollment, ServiceEnrollment>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection(); // Thêm nếu bạn muốn sử dụng HTTPS
app.UseAuthentication(); // Đảm bảo dùng trước app.UseAuthorization()
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
