# Task Management System

Thời gian bắt đầu: `2025-05-24 17:00:00`

## Tổng quan

Dự án bao gồm:
- **Web API**: ASP.NET Core (Minimal API, Clean Architecture/CQRS)
- **Web App**: ReactJs
- **Database**: PostgreSQL
- **Quản lý toàn bộ bằng Docker Compose**

## ▶️ Khởi động hệ thống bằng Docker Compose

### 1. Build và chạy toàn bộ:

> Mở terminal tại vị trí file DockerCompse (.../src), chạy lệnh
```bash
docker-compose -p taskmanagementsystem up --build
```

### 2. Dừng toàn bộ:

```bash
docker-compose down
```

### 3. Xây lại từ đầu:

```bash
docker-compose up --build --force-recreate
```

### * Note: Kết nối đến PostgreSQL (Docker Container)

```bash
docker exec -it - applicationdb psql -U postgres
```

## 🌐 Các dịch vụ và cổng mặc định

| Service         | Mô tả             | Host                                                          | Port                      |
| --------------- | ----------------- | ------------------------------------------------------------- | ------------------------- |
| `webapp`        | React             | [http://localhost](http://localhost:3000)                     | `3000`                    |
| `webapi`        | ASP.NET Core API  | [http://localhost](http://localhost:4000)                     | `4000`                    |
| `applicationdb` | PostgreSQL        | localhost                                                     | `4100` → container `5432` |
| `API Document`  | Swagger           | [http://localhost](http://localhost:4000/swagger/index.html)  | `4000`                    |

## 📋 API Documentary

- Hiện ngoại trừ 2 endpoint của `auth` là Login và Register ra thì các endpoint khác đều phải xác thực mới call tới được, bằng không thì response sẽ là *401 (Lỗi xác thực)*
- Cách thức xác thực:
  + B1. Call đến `/auth/login` hoặc `/auth/register` thành công sẽ lấy được **token**
  + B2. Nhấn vào `🔒 Authorizor` và nhập đúng mã token nhận được (Lưu ý: Chỉ nhập mã token, không cần bắt đầu với `'Bearer '` vì sẽ được thêm sẵn)
  + B3. Giờ đã có thể Call đến bất cứ endpoint nào

## 🔎 Đôi nét về dự án

- Backend:
    + Công nghệ: Restful API, Minimal API (Carter), Clean Architecture/CQRS, PostgreSQL/Entity Framework
    + Design pattern: Unit of Work, Repository, Factory
    + Một số chức năng đã thực hiện: Search criteria (linh hoạt, nhiều giá trị), Error handling, CRUD for task entity, migration postgre
    + Dự định thực hiện: Search full-text trong entity, Caching, Logout (Refresh token), Phân quyền User, unit Test,...
- Client:
    + Xử lý cơ bản giao diện UI (Chưa hoàn chỉnh)
    + Hiện xử lý giao tiếp với server đang gặp vấn đề và chưa giải quyết xong, do đó đang set dummy dữ liệu để chạy được trên localhost
  
## 📝 Notes

> Đôi lúc chạy Docker Compose lần đầu có thể bị lỗi do Database chưa sẵn sàng, vui lòng thử lại vài lần

```bash
docker-compose -p taskmanagementsystem up webapi
```
