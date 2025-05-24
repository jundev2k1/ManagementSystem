# Task Management System

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
docker-compose up taskmanagementsystem --build
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

## 📝 Notes

> Đôi lúc chạy Docker Compose lần đầu có thể bị lỗi do Database chưa sẵn sàng, vui lòng thử lại vài lần
