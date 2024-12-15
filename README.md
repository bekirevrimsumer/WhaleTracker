# WhaleTracker - Solana Wallet Token Tracker

WhaleTracker, Solana cüzdanlarındaki token hareketlerini takip eden ve analiz eden bir uygulamadır. Belirtilen cüzdanların tuttukları coin'leri, yeni token hareketlerini belirli aralıklarla kontrol eder. Bu hareketleri kaydeder ve kullanıcıya bildirim gönderir.

## Özellikler

- Solana cüzdanlarını ekleme ve takip etme
- Token bilgilerini otomatik güncelleme
- Gerçek zamanlı bildirimler (SignalR ile)
- Token değerlerini USD cinsinden görüntüleme
- Cüzdan bazında toplam değer hesaplama

## Teknolojiler

### Backend
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- SignalR
- Solscan Pro API

### Frontend
- React
- TypeScript
- Material-UI
- Axios
- React Router

## Kurulum

### Ön Gereksinimler
- .NET 8 SDK
- Node.js ve npm
- PostgreSQL
- Docker (opsiyonel)

### Backend Kurulumu

1. PostgreSQL veritabanını başlatın:
```bash
cd docker
docker-compose up -d
```

2. Veritabanı migration'larını uygulayın:
```bash
cd src/WhaleTracker.API
dotnet ef database update
```

3. API'yi başlatın:
```bash
dotnet run
```

API uygulamasının çalıştığı portu frontend'de güncelleyin. Şu anki port 5036.

### Frontend Kurulumu

1. Gerekli paketleri yükleyin:
```bash
cd src/whaletracker_frontend
npm install
```

2. Frontend uygulamasını başlatın:
```bash
npm start
```

Frontend uygulaması http://localhost:3000 adresinde çalışacaktır.

### Solscan API Ayarları

1. [Solscan Pro](https://pro-api.solscan.io/pro-api-docs/v2.0) üzerinden bir API anahtarı alın
2. API anahtarını `src/WhaleTracker.API/appsettings.json` dosyasına ekleyin:
```json
{
  "SolscanApi": {
    "ApiKey": "your_api_key_here"
  }
}
```
