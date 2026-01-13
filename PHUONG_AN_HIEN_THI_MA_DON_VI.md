# Phương án: Hiển thị chỉ mã đơn vị thay vì "Mã - Tên"

## Vấn đề

Hiện tại MudAutocomplete đang hiển thị:
- **"NX - Công ty Cổ Phần Đầu Tư & Phát Triển Dịch Vụ Nhà Xanh"**

User muốn hiển thị:
- **"NX"** (chỉ mã đơn vị)

## Phân tích code hiện tại

### MudAutocomplete (dòng 206-241):
```razor
<MudAutocomplete T="DanhMucKhachHangModel" 
                 Value="@selectedKhachHang"
                 ToStringFunc="@((DanhMucKhachHangModel x) => x != null ? $"{x.MaDonVi} - {x.TenDonVi}" : string.Empty)"
                 ... />
```

### Vấn đề:
- `ToStringFunc` đang format: `"{x.MaDonVi} - {x.TenDonVi}"`
- Cần đổi thành chỉ: `x.MaDonVi`

## Giải pháp đề xuất

### **Phương án 1: Chỉ hiển thị mã đơn vị (Đơn giản nhất)** ⭐⭐⭐⭐⭐

#### Mô tả:
- Đổi `ToStringFunc` để chỉ hiển thị `MaDonVi`
- Dropdown vẫn hiển thị đầy đủ "Mã - Tên" để user dễ chọn (nếu MudAutocomplete hỗ trợ)
- Input field chỉ hiển thị mã

#### Implementation:

```razor
<MudAutocomplete T="DanhMucKhachHangModel" 
                 Value="@selectedKhachHang"
                 ToStringFunc="@((DanhMucKhachHangModel x) => x != null ? x.MaDonVi : string.Empty)"
                 ... />
```

#### Ưu điểm:
- ✅ Đơn giản: Chỉ cần đổi một dòng
- ✅ Hiển thị đúng theo yêu cầu: Chỉ mã đơn vị
- ✅ Không ảnh hưởng logic khác

#### Nhược điểm:
- ⚠️ Dropdown có thể cũng chỉ hiển thị mã (nhưng user vẫn có thể search theo tên)

---

### **Phương án 2: Hiển thị mã trong input, đầy đủ trong dropdown** ⭐⭐⭐⭐

#### Mô tả:
- Input field hiển thị chỉ mã
- Dropdown hiển thị đầy đủ "Mã - Tên" để user dễ chọn
- Cần kiểm tra xem MudAutocomplete có hỗ trợ `OptionTemplate` không

#### Implementation:

```razor
<MudAutocomplete T="DanhMucKhachHangModel" 
                 Value="@selectedKhachHang"
                 ToStringFunc="@((DanhMucKhachHangModel x) => x != null ? x.MaDonVi : string.Empty)"
                 OptionTemplate="@((DanhMucKhachHangModel x) => $"{x.MaDonVi} - {x.TenDonVi}")"
                 ... />
```

#### Lưu ý:
- Cần kiểm tra xem MudAutocomplete có property `OptionTemplate` không
- Nếu không có, có thể dùng `Option` template

---

## Khuyến nghị

**Chọn Phương án 1** vì:
1. ✅ Đơn giản nhất: Chỉ cần đổi `ToStringFunc`
2. ✅ Đáp ứng yêu cầu: Hiển thị chỉ mã đơn vị
3. ✅ User vẫn có thể search theo mã hoặc tên (SearchFunc vẫn hoạt động)
4. ✅ Không cần thay đổi nhiều code

---

## Các file cần sửa

1. ✅ `QLSX-Web/Pages/NhapXuats/NhapXuatsForm.razor`
   - Đổi `ToStringFunc` từ `$"{x.MaDonVi} - {x.TenDonVi}"` thành `x.MaDonVi`

---

## Lưu ý

1. **SearchFunc**: Vẫn hoạt động bình thường, user có thể search theo mã hoặc tên
2. **Dropdown**: Có thể chỉ hiển thị mã, nhưng user vẫn có thể chọn từ dropdown
3. **TenDonVi**: Vẫn được lưu vào `nhapxuat.TenDonVi` khi chọn, chỉ là không hiển thị trong input
