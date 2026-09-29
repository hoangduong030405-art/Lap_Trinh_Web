using Blazor_demo;
using Blazor_demo.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using eShop.CoreBusiness.Models;
using eShop.CoreBusiness.Services;
using eShop.UseCases.PluginInterfaces.DataStore;
using eShop.UseCases.PluginInterfaces.StateStore;
using eShop.UseCases.PluginInterfaces.UI;
using eShop.UseCases.SearchProductScreen;
using eShop.UseCases.ViewProductScreen;
using eShop.UseCases.ShoppingCartScreen;
using eShop.UseCases.OrderConfirmationScreen;
using eShop.DataStore.HardCoded;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Thiết lập văn hóa định dạng en-US để hiển thị ký hiệu tiền tệ $ và giá tiền đúng chuẩn video bài giảng
var cultureInfo = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

// Đăng ký Dependency Injection theo bài giảng Clean Architecture
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<ISearchProduct, Searchproduct>();
builder.Services.AddTransient<IViewProduct, ViewProduct>();

// ShoppingCart Plugin & StateStore & Use Cases (Video 03, 04, 06, 07, 08, 09, 10)
builder.Services.AddScoped<IShoppingCart, ShoppingCart>();
builder.Services.AddScoped<IShoppingCartStateStore, ShoppingCartStateStore>();
builder.Services.AddTransient<IAddProductToCartUseCase, AddProductToCartUseCase>();
builder.Services.AddTransient<IViewShoppingCartUseCase, ViewShoppingCartUseCase>();
builder.Services.AddTransient<IDeleteProductFromCartUseCase, DeleteProductFromCartUseCase>();
builder.Services.AddTransient<IUpdateQuantityUseCase, UpdateQuantityUseCase>();
builder.Services.AddTransient<IPlaceOrderUseCase, PlaceOrderUseCase>();
builder.Services.AddTransient<IViewOrderConfirmationUseCase, ViewOrderConfirmationUseCase>();

//builder.Services.AddTransient<ICustomerService, CustomerService>();
//builder.Services.AddSingleton<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

var app = builder.Build();

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(cultureInfo),
    SupportedCultures = new[] { cultureInfo },
    SupportedUICultures = new[] { cultureInfo }
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
