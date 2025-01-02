Lưu ý khi mở app.
•Mở class GlobalConnection để đổi 4 địa chỉ thư mục cho phù hợp với từng máy
           +Địa chỉ Conntection string sql.
           +Ba địa chỉ còn lại là ba thư mục tương ứng có trong app ( DashBoardImage,Books_Directory, Student_Directory.
•Chạy file sqlquery để tạo bảng trong cơ sở dữ liệu library_management.

Hướng dẫn sử dụng:
Giao diện Log in và Sign up bao gồm các chức năng chính sau:
1.Log in: Người dùng chỉ cần nhập username và password là có thể sử dụng được hệ thống.
2.Sign up: Nếu như chưa có tài khoản thì chỉ cần nhập email, username và password để tạo tài khoản
Ngoài ra, còn có chức năng phân quyền giữa admin và người dùng thường (nếu tên tài khoản có từ khóa “admin” thì mới có thể update hoặc xóa thông tin books và students)
Giao diện chính bao gồm các chức năng chính sau:
1.Dashboard: Hiển thị tổng quan về trạng thái của sách, bao gồm: 
oSố lượng sách có sẵn.
oSố sách đã được mượn.
oSố sách đã trả.
2.Add Books: Cho phép thêm thông tin sách mới vào hệ thống (bao gồm hình ảnh, các thông tin cơ bản, ngày thêm và trạng thái).
3.Issue Books: Xử lý thông tin việc cho mượn sách ( bao gồm thông tin người mượn và thpng6 tin sách được mượn).
4.Return Books: Cập nhật trạng thái sách đã trả vào hệ thống.
5.Add Students: Quản lý thông tin sinh viên mượn sách từ thư viện.
6.Logout: Đăng xuất người dùng khỏi hệ thống.
