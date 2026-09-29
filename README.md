# BÁO CÁO BÀI TẬP / DỰ ÁN LẬP TRÌNH WEB
# HỆ THỐNG BÁN HÀNG TRỰC TUYẾN ESHOP (BLAZOR SERVER & CLEAN ARCHITECTURE)

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

Dự án này là ứng dụng thương mại điện tử **eShop** được xây dựng trên nền tảng **ASP.NET Core Blazor Server** kết hợp với **Kiến trúc Sạch (Clean Architecture)** theo chuỗi bài giảng thực hành (10 Videos).

Hệ thống cho phép người dùng:
1. **Duyệt & Tìm kiếm sản phẩm:** Tìm kiếm thời gian thực theo từ khóa, hiển thị sản phẩm dạng lưới thẻ (Product Cards) kèm hình ảnh, tên và giá thành.
2. **Xem chi tiết sản phẩm:** Xem mô tả chi tiết, hình ảnh phóng to, lựa chọn số lượng và thêm vào giỏ hàng.
3. **Quản lý Giỏ hàng (Shopping Cart):** Cập nhật tăng/giảm số lượng, xóa từng món hàng, tính toán tổng tiền tự động.
4. **Quản lý Trạng thái (State Management):** Số lượng giỏ hàng trên thanh điều hướng (`TopNavBar`) được đồng bộ tức thì ở mọi trang nhờ cơ chế State Store (`IShoppingCartStateStore`) và Event Broadcasting.
5. **Tiến trình Đặt hàng (Checkout / Place Order):** Điền thông tin giao hàng (Tên, Địa chỉ, Thành phố, Tỉnh, Quốc gia) với validation đầy đủ, xem tóm tắt đơn hàng (`OrderSummaryComponent`).
6. **Xác nhận Đơn hàng (Order Confirmation):** Lưu trữ đơn hàng và hiển thị hóa đơn xác nhận đơn hàng thành công kèm mã tra cứu duy nhất (`UniqueId`).

---

## 🏗️ KIẾN TRÚC HỆ THỐNG (CLEAN ARCHITECTURE)

Dự án được phân tách chặt chẽ theo các tầng của Clean Architecture nhằm đảm bảo tính độc lập, khả năng mở rộng và kiểm thử:

```
c:\Lap_Trinh_Web\Blazor_demo\
│
├── 📦 eShop.CoreBusiness/              -- TẦNG LÕI NGHIỆP VỤ (ENTITIES & BUSINESS RULES)
│   ├── Models/
│   │   ├── Product.cs                  -- Thực thể Sản phẩm
│   │   ├── Order.cs                    -- Thực thể Đơn hàng (chứa LineItems, tính tổng, validation)
│   │   └── OrderLineItem.cs            -- Thực thể Chi tiết đơn hàng
│   └── Services/
│       ├── interfaces/
│       │   └── IOrderService.cs        -- Nghiệp vụ kiểm tra & xác thực đơn hàng
│       └── OrderService.cs             -- Cài đặt logic nghiệp vụ đơn hàng
│
├── 📦 eShop.UseCases/                  -- TẦNG TRƯỜNG HỢP SỬ DỤNG (APPLICATION USE CASES)
│   ├── PluginInterfaces/
│   │   ├── DataStore/
│   │   │   ├── IProductRepository.cs   -- Interface truy xuất dữ liệu sản phẩm
│   │   │   └── IOrderRepository.cs     -- Interface truy xuất & lưu trữ đơn hàng
│   │   └── UI/
│   │       ├── IShoppingCart.cs        -- Interface thao tác giỏ hàng
│   │       └── IShoppingCartStateStore.cs -- Interface quản lý sự kiện trạng thái giỏ hàng
│   ├── SearchProductScreen/            -- Use Cases cho màn hình tìm kiếm & danh mục
│   ├── ViewProductScreen/              -- Use Cases cho màn hình chi tiết sản phẩm
│   └── ShoppingCartScreen/             -- Use Cases giỏ hàng (Xem, Thêm, Sửa số lượng, Xóa, Đặt hàng)
│
├── 📦 Plugins/ (DATA ACCESS & STATE)   -- TẦNG TRIỂN KHAI CÔNG NGHỆ BÊN NGOÀI
│   ├── eShop.DataStore.HardCoded/      -- Lưu trữ sản phẩm mẫu & danh sách đơn hàng
│   │   ├── ProductRepository.cs
│   │   └── OrderRepository.cs
│   └── eShop.ShoppingCart.LocalStorage/-- Triển khai giỏ hàng lưu bằng ProtectedLocalStorage
│
└── 🌐 Blazor_demo/ (PRESENTATION)      -- TẦNG GIAO DIỆN NGƯỜI DÙNG (BLAZOR SERVER)
    ├── Controls/                       -- Các Blazor Components tái sử dụng
    │   ├── TopNavBar.razor             -- Thanh điều hướng chuẩn trên cùng (Brand + Cart count)
    │   ├── ProductCardComponent.razor  -- Thẻ hiển thị sản phẩm dạng lưới (Card)
    │   ├── SearchBarComponent.razor    -- Thanh tìm kiếm sản phẩm
    │   ├── AddToCartComponent.razor    -- Nút thêm vào giỏ hàng
    │   ├── LineItemComponent.razor     -- Dòng sản phẩm trong giỏ hàng (tăng/giảm/xóa)
    │   ├── OrderSummaryComponent.razor -- Khối tóm tắt hóa đơn & tổng tiền
    │   └── DisplayCountComponent.razor -- Hiển thị số lượng giỏ hàng trên Header
    ├── Pages/                          -- Các trang chức năng chính (Routable Components)
    │   ├── SearchProductComponent.razor-- Trang chủ / Danh mục sản phẩm ("/", "/products")
    │   ├── ViewProductComponent.razor  -- Chi tiết sản phẩm ("/product/{id}")
    │   ├── ShoppingCartComponent.razor -- Xem & điều chỉnh giỏ hàng ("/cart")
    │   ├── PlaceOrderComponent.razor   -- Điền thông tin đặt hàng ("/placeorder")
    │   └── OrderConfirmationComponent.razor -- Hoàn tất đặt hàng ("/orderconfirm/{uniqueId}")
    ├── Shared/                         -- Bố cục giao diện chung
    │   └── MainLayout.razor            -- Bố cục chuẩn (TopNavBar full-width, không sidebar cũ)
    └── Program.cs                      -- Cấu hình Dependency Injection (DI) & Services
```

---

## 📋 NỘI DUNG THỰC HIỆN THEO LỘ TRÌNH 10 VIDEO (FRANK LIU)

| Video | Nội dung bài học | Trạng thái |
| :---: | :--- | :---: |
| **01** | Tổ chức lại cấu trúc Clean Architecture (Tách CoreBusiness, UseCases, DataStore, Presentation). | ✅ Hoàn thành |
| **02** | Xây dựng các thực thể `Order`, `OrderLineItem` và các Business Rules (Validation, Tính tổng tiền). | ✅ Hoàn thành |
| **03** | Trừu tượng hóa giỏ hàng (`IShoppingCart`) và xây dựng `AddProductToCartUseCase`. | ✅ Hoàn thành |
| **04** | Cài đặt Plugin lưu trữ giỏ hàng phía Client với `ProtectedLocalStorage` và viết Unit Test. | ✅ Hoàn thành |
| **05** | Chuẩn hóa giao diện: Bỏ thanh sidebar cũ, chuyển sang thanh điều hướng ngang `TopNavBar`, hiển thị danh sách sản phẩm dạng thẻ Bootstrap (`ProductCardComponent`). | ✅ Hoàn thành |
| **06** | Xây dựng màn hình xem giỏ hàng `ShoppingCartComponent`, component chi tiết `LineItemComponent` và `OrderSummaryComponent`. | ✅ Hoàn thành |
| **07** | Quản lý trạng thái giỏ hàng phản ứng tức thời (State Management) với `IShoppingCartStateStore` và hiển thị số lượng giỏ hàng trên Header. | ✅ Hoàn thành |
| **08** | Xây dựng Use Cases xóa sản phẩm (`IDeleteProductFromCartUseCase`), cập nhật số lượng (`IUpdateQuantityUseCase`) và lưu trữ đơn hàng `OrderRepository`. | ✅ Hoàn thành |
| **09** | Xây dựng chức năng đặt hàng (`PlaceOrderUseCase`), form thông tin khách hàng `CustomerFormComponent` và màn hình `PlaceOrderComponent`. | ✅ Hoàn thành |
| **10** | Xây dựng màn hình xác nhận đơn hàng `OrderConfirmationComponent` hiển thị chi tiết hóa đơn theo mã đơn hàng duy nhất. | ✅ Hoàn thành |

---

## 🛠️ CÔNG NGHỆ SỬ DỤNG

- **Ngôn ngữ:** C# 12 / .NET 8 (và tương thích .NET 10 SDK)
- **Framework:** ASP.NET Core Blazor Server
- **Kiến trúc:** Clean Architecture (Onion / Hexagonal Architecture)
- **Giao diện & Styling:** HTML5, CSS3, Bootstrap 5, Open-Iconic
- **Lưu trữ dữ liệu:** In-Memory Data Store & Browser `ProtectedLocalStorage`
- **Giao tiếp thời gian thực:** SignalR (Tích hợp sẵn trong Blazor Server)

---

## 🚀 HƯỚNG DẪN CÀI ĐẶT VÀ CHẠY ỨNG DỤNG

### 1. Yêu cầu môi trường
- Đã cài đặt **.NET 8.0 SDK** (hoặc mới hơn, ví dụ .NET 9 / .NET 10).
- Trình duyệt Web hiện đại (Google Chrome, Microsoft Edge, Firefox, Brave,...).

### 2. Các bước khởi chạy dự án

1. Mở cửa sổ dòng lệnh (**Command Prompt** hoặc **PowerShell**).
2. Di chuyển vào thư mục dự án Blazor Web:
   ```powershell
   cd C:\Lap_Trinh_Web\Blazor_demo\Blazor_demo
   ```
3. Biên dịch kiểm tra dự án (đảm bảo 0 lỗi):
   ```powershell
   dotnet build
   ```
4. Khởi chạy ứng dụng:
   ```powershell
   dotnet run
   ```
5. Mở trình duyệt web và truy cập theo đường dẫn hiển thị trên terminal:
   - **HTTPS:** `https://localhost:7224`
   - **HTTP:** `http://localhost:5124`

---

## 🎯 DANH SÁCH ĐƯỜNG DẪN (ROUTES) TRONG ỨNG DỤNG

- **Trang chủ / Danh sách sản phẩm:** `https://localhost:7224/` hoặc `https://localhost:7224/products`
- **Xem chi tiết sản phẩm:** `https://localhost:7224/product/{id}` (Ví dụ: `/product/1`)
- **Giỏ hàng:** `https://localhost:7224/cart`
- **Đặt hàng:** `https://localhost:7224/placeorder`
- **Xác nhận đơn hàng:** `https://localhost:7224/orderconfirm/{uniqueId}`

---
*© 2026 - Hoàng Đức Dương (MSSV: 23K4080007) - Đại học Kinh Tế Huế.*
