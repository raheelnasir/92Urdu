using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MudBlazor.Services;
using View.Authentication;
using View.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthenticationCore();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddSingleton<UserAccountService>();
builder.Services.AddSingleton<IUploadImage>();
builder.Services.AddSingleton<ISignupUser>();
builder.Services.AddHttpClient<IUserProfile, ISignupUser>(
    c => { c.BaseAddress = new Uri("https://api.92urdu.com/"); }); // Remove '/api/'

builder.Services.AddSingleton<IUpdateUserProfileInformation>();
builder.Services.AddSingleton<ISetGhazalDetails>();
builder.Services.AddSingleton<IPostGhazalVerses>();

builder.Services.AddSingleton<ISetNazamDetails>();
builder.Services.AddSingleton<IPostNazamVerses>();

builder.Services.AddSingleton<IPostPhrases>();

builder.Services.AddSingleton<ICreateUser>();
builder.Services.AddHttpClient<IUserProfile, ICreateUser>(
    c => { c.BaseAddress = new Uri("https://api.92urdu.com/"); }); // Remove '/api/'

// Register the IGetUsersData service
builder.Services.AddHttpClient<IGetUsersData>(
    c => { c.BaseAddress = new Uri("https://api.92urdu.com/"); });

builder.Services.AddHttpClient<IUpdateUsersProfileDataStatus>(
    c => { c.BaseAddress = new Uri("https://api.92urdu.com/"); });

builder.Services.AddSingleton<IDeleteUsersProfile>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
