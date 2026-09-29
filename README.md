# BÁO CÁO BÀI TẬP / DỰ ÁN LẬP TRÌNH WEB
# HỆ THỐNG THƯƠNG MẠI ĐIỆN TỬ ESHOP (BLAZOR SERVER & CLEAN ARCHITECTURE)
### THEO CHUỖI BÀI GIẢNG 10 VIDEO (FRANK LIU - VIDEOPART3)

---

## 👤 THÔNG TIN SINH VIÊN / THÔNG TIN CÁ NHÂN

- **Họ và tên sinh viên:** Hoàng Đức Dương
- **Mã số sinh viên (MSSV):** 23K4080007
- **Lớp / Khóa:** K57 Tin Học Kinh Tế
- **Môn học / Học phần:** Lập Trình Ứng Dụng Web
- **Giảng viên hướng dẫn:** ThS. Hà Ngọc Long
- **Khoa / Trường:** Khoa Hệ Thống Thông Tin Kinh Tế - Trường Đại học Kinh Tế, Đại học Huế
- **Ngày hoàn thành / Nộp bài:** 29/09/2026
- **Kho lưu trữ mã nguồn (Repository / GitHub):** [https://github.com/hoangduong030405-art/Lap_Trinh_Web](https://github.com/hoangduong030405-art/Lap_Trinh_Web)

---

## 📖 GIỚI THIỆU TỔNG QUAN DỰ ÁN

Dự án này là giải pháp thương mại điện tử **eShop** hoàn chỉnh được xây dựng trên nền tảng **ASP.NET Core Blazor Server** kết hợp chặt chẽ với **Kiến trúc Sạch (Clean Architecture)** và **Mô hình Module hóa (Razor Class Libraries - RCL)** theo đúng chuẩn 10 video bài giảng thực hành của Frank Liu (`videopart3`).

Toàn bộ giải pháp được cấu trúc thành Solution riêng biệt **`eShop.sln`** (và **`eShop.slnx`**) gồm đầy đủ **9 Projects** độc lập, phân chia rõ ràng theo Solution Folders:
1. **Duyệt & Tìm kiếm sản phẩm:** Tìm kiếm thời gian thực theo từ khóa (`SearchBarComponent`), hiển thị sản phẩm dạng lưới thẻ (`ProductCardComponent`) kèm hình ảnh, tên, thương hiệu và đơn giá.
2. **Xem chi tiết sản phẩm:** Xem thông tin mô tả chi tiết, hình ảnh sản phẩm chất lượng cao, nút **Add to Cart** điều hướng mượt mà sang giỏ hàng (`ViewProductComponent`).
3. **Quản lý Giỏ hàng (Shopping Cart):** Cập nhật tăng/giảm số lượng trực tiếp (`LineItemComponent`), xóa món hàng, tự động tính tổng số lượng và thành tiền (`OrderSummaryComponent`).
4. **Quản lý Trạng thái phản ứng tức thì (State Store):** Số lượng hiển thị trên giỏ hàng tại thanh điều hướng (`TopNavBar`) được đồng bộ tức thì ở mọi trang nhờ `IShoppingCartStateStore` và cơ chế Event Broadcasting.
5. **Tiến trình Đặt hàng (Checkout / Place Order):** Điền thông tin giao hàng khách hàng (`CustomerFormComponent`) với kiểm tra hợp lệ dữ liệu (Data Annotation Validation).
6. **Xác nhận Đơn hàng (Order Confirmation):** Lưu trữ đơn hàng vào Repository, tạo mã tra cứu đơn hàng duy nhất (`UniqueId`) và hiển thị hóa đơn xác nhận đơn hàng thành công (`OrderConfirmationComponent`).

---

## 🏗️ CẤU TRÚC 9 DỰ ÁN CHUẨN TRONG SOLUTION `eShop.sln`

Cấu trúc Solution tuân thủ 100% video hướng dẫn của Frank Liu:

```
c:\Lap_Trinh_Web\Blazor_demo\
│
├── 📜 eShop.sln / eShop.slnx                   -- SOLUTION TỔNG CHỨA 9 PROJECTS
│
├── 📦 eShop.CoreBusiness/                      -- [Core] Thực thể nghiệp vụ & quy tắc kinh doanh
│   ├── Models/
│   │   ├── Product.cs                          -- Thực thể Sản phẩm
│   │   ├── Order.cs                            -- Thực thể Đơn hàng (Validation, LineItems)
│   │   └── OrderLineItem.cs                    -- Chi tiết từng món trong đơn hàng
│   └── Services/
│       ├── IOrderService.cs                    -- Interface nghiệp vụ kiểm tra đơn hàng
│       └── OrderService.cs                     -- Cài đặt logic nghiệp vụ đơn hàng
│
├── 📦 eShop.UseCases/                          -- [Application] Các ca sử dụng (Use Cases)
│   ├── PluginInterfaces/
│   │   ├── DataStore/
│   │   │   ├── IProductRepository.cs           -- Interface truy xuất dữ liệu sản phẩm
│   │   │   └── IOrderRepository.cs             -- Interface truy xuất & lưu trữ đơn hàng
│   │   ├── StateStore/
│   │   │   └── IStateStore.cs                  -- Interface chung cho State Management
│   │   └── UI/
│   │       ├── IShoppingCart.cs                -- Interface giỏ hàng phía Client
│   │       └── IShoppingCartStateStore.cs      -- Interface thông báo sự kiện cập nhật giỏ hàng
│   ├── SearchProductScreen/                    -- SearchProductUseCase
│   ├── ViewProductScreen/                      -- ViewProductUseCase
│   ├── ShoppingCartScreen/                     -- ViewShoppingCart, AddProductToCart, UpdateQuantity, DeleteProduct
│   └── OrderPlacementScreen/                   -- PlaceOrderUseCase, ViewOrderConfirmationUseCase
│
├── 📁 Plugins/                                 -- [Infrastructure Plugins] Các plugin công nghệ
│   ├── 📦 eShop.DataStore.HardCoded/           -- Cài đặt lưu trữ dữ liệu In-Memory
│   │   ├── ProductRepository.cs
│   │   └── OrderRepository.cs
│   ├── 📦 eShop.ShoppingCart.LocalStorage/     -- Cài đặt giỏ hàng bằng Browser LocalStorage / IJSRuntime
│   │   └── ShoppingCart.cs
│   └── 📦 eShop.StateStore.DI/                 -- Quản lý trạng thái bằng Dependency Injection
│       ├── StateStoreBase.cs
│       └── ShoppingCartStateStore.cs
│
├── 📁 eShop.Web.Modules/                       -- [Razor Class Libraries] Các module giao diện tách rời
│   ├── 📦 eShop.Web.Common/                    -- Thành phần UI dùng chung
│   │   └── Controls/SearchBarComponent.razor   -- Khung tìm kiếm sản phẩm
│   ├── 📦 eShop.Web.CustomerPortal/            -- Cổng thông tin & trải nghiệm khách hàng
│   │   ├── Controls/
│   │   │   ├── CartComponent.razor             -- Nút giỏ hàng + số lượng trên Header
│   │   │   ├── CustomerFormComponent.razor     -- Form nhập thông tin khách hàng
│   │   │   ├── LineItemComponent.razor         -- Dòng chi tiết giỏ hàng
│   │   │   ├── OrderSummaryComponent.razor     -- Tóm tắt đơn hàng & tổng tiền
│   │   │   ├── ProductCardComponent.razor      -- Thẻ hiển thị sản phẩm dạng lưới
│   │   │   └── ProductItemComponent.razor      -- Dòng hiển thị sản phẩm dạng bảng
│   │   ├── Pages/
│   │   │   ├── SearchProductComponent.razor    -- Trang danh sách & tìm kiếm ("/", "/products")
│   │   │   ├── ViewProductComponent.razor      -- Trang chi tiết sản phẩm ("/product/{id}")
│   │   │   ├── ShoppingCartComponent.razor     -- Trang giỏ hàng ("/cart")
│   │   │   ├── PlaceOrderComponent.razor       -- Trang đặt hàng ("/placeorder")
│   │   │   └── OrderConfirmationComponent.razor-- Trang xác nhận đơn hàng ("/orderconfirm/{uniqueId}")
│   │   └── ViewModels/
│   │       └── CustomerViewModel.cs            -- Model dữ liệu xác thực form khách hàng
│   └── 📦 eShop.Web.AdminPortal/               -- Cổng quản trị eShop (Dành cho Admin)
│
└── 🌐 eShop.Web/                               -- [Host Application] Dự án Blazor Server điều phối
    ├── Properties/launchSettings.json          -- Cấu hình port chạy (5200 / 7200)
    ├── Shared/
    │   ├── MainLayout.razor                    -- Bố cục ứng dụng chuẩn, hiện đại
    │   └── TopNavBar.razor                     -- Thanh điều hướng Header tích hợp CartComponent
    ├── App.razor                               -- Router nạp động Assembly từ CustomerPortal & AdminPortal
    └── Program.cs                              -- Đăng ký DI cho toàn bộ 8 dự án thành phần
```

---

## 📋 NỘI DUNG THỰC HIỆN THEO 10 VIDEO (FRANK LIU - VIDEOPART3)

| Video | Nội dung kỹ thuật theo bài giảng Frank Liu | Mức độ hoàn thành |
| :---: | :--- | :---: |
| **01** | Tạo cấu trúc Solution `eShop.sln`, phân tách tầng Clean Architecture (`eShop.CoreBusiness`, `eShop.UseCases`, `Plugins`, `eShop.Web.Modules`). | ✅ Đã hoàn thành 100% |
| **02** | Xây dựng các thực thể `Order`, `OrderLineItem` và các Business Rules (Validation, Tính tổng tiền). | ✅ Đã hoàn thành 100% |
| **03** | Trừu tượng hóa giỏ hàng (`IShoppingCart`) và xây dựng `AddProductToCartUseCase`. | ✅ Đã hoàn thành 100% |
| **04** | Cài đặt Plugin lưu trữ giỏ hàng phía Client (`eShop.ShoppingCart.LocalStorage`) sử dụng JS Interop / LocalStorage. | ✅ Đã hoàn thành 100% |
| **05** | Thiết kế giao diện hiện đại: Chuẩn hóa `TopNavBar`, hiển thị danh sách sản phẩm dạng thẻ Bootstrap (`ProductCardComponent`) trong module `eShop.Web.CustomerPortal`. | ✅ Đã hoàn thành 100% |
| **06** | Xây dựng màn hình xem giỏ hàng `ShoppingCartComponent`, component chi tiết `LineItemComponent` và `OrderSummaryComponent`. | ✅ Đã hoàn thành 100% |
| **07** | Quản lý trạng thái giỏ hàng phản ứng tức thời (State Store) với `eShop.StateStore.DI`, hiển thị số lượng giỏ hàng trên Header qua `CartComponent`. | ✅ Đã hoàn thành 100% |
| **08** | Xây dựng Use Cases xóa sản phẩm (`IDeleteProductFromCartUseCase`), cập nhật số lượng (`IUpdateQuantityUseCase`) và lưu trữ đơn hàng `OrderRepository`. | ✅ Đã hoàn thành 100% |
| **09** | Xây dựng chức năng đặt hàng (`PlaceOrderUseCase`), form thông tin khách hàng `CustomerFormComponent` với Data Annotations và trang `PlaceOrderComponent`. | ✅ Đã hoàn thành 100% |
| **10** | Xây dựng màn hình xác nhận đơn hàng `OrderConfirmationComponent` hiển thị chi tiết hóa đơn theo mã đơn hàng duy nhất (`UniqueId`). | ✅ Đã hoàn thành 100% |

---

## 🛠️ CÔNG NGHỆ & KỸ THUẬT NỔI BẬT

- **Nền tảng:** .NET 8 / C# 12 (Tương thích hoàn toàn với .NET 9 & .NET 10 SDK).
- **Framework UI:** ASP.NET Core Blazor Server.
- **Mô hình kiến trúc:** Clean Architecture + Modular Razor Class Libraries (RCL).
- **Quản lý trạng thái (State Management):** Observer Pattern / In-Memory State Store với Action Subscription.
- **Client Storage:** LocalStorage tích hợp qua JS Runtime an toàn.
- **Dependency Injection:** Tách biệt triệt để interfaces và implementations, cho phép hoán đổi plugin linh hoạt.

---

## 🚀 HƯỚNG DẪN BIÊN DỊCH VÀ KHỞI CHẠY ESHOP

### 1. Yêu cầu môi trường
- Đã cài đặt **.NET 8.0 SDK** (hoặc mới hơn).

### 2. Biên dịch toàn bộ Solution `eShop.sln`
Mở PowerShell tại thư mục gốc `c:\Lap_Trinh_Web\Blazor_demo\`:
```powershell
dotnet build eShop.sln
```
> Kết quả: **`Build succeeded. 0 Warning(s), 0 Error(s)`** trên cả 9 projects.

### 3. Khởi chạy ứng dụng eShop
Chạy trực tiếp dự án Host `eShop.Web`:
```powershell
dotnet run --project eShop.Web/eShop.Web.csproj
```
Hoặc:
```powershell
cd eShop.Web
dotnet run
```

### 4. Truy cập ứng dụng trên trình duyệt
Mở trình duyệt Web và truy cập:
- **HTTP:** [http://localhost:5200](http://localhost:5200)
- **HTTPS:** [https://localhost:7200](https://localhost:7200)

---

## 🎯 CÁC ĐƯỜNG DẪN TRẢI NGHIỆM TRONG ESHOP

- **Trang chủ & Danh sách sản phẩm:** `http://localhost:5200/` hoặc `http://localhost:5200/products`
- **Xem chi tiết sản phẩm:** `http://localhost:5200/product/1`
- **Giỏ hàng trực tuyến:** `http://localhost:5200/cart`
- **Điền thông tin đặt hàng:** `http://localhost:5200/placeorder`
- **Hóa đơn xác nhận đơn hàng:** `http://localhost:5200/orderconfirm/{uniqueId}`

---
*© 2026 - Hoàng Đức Dương (MSSV: 23K4080007) - Lập Trình Web - Đại học Kinh Tế, Đại học Huế.*
