# WhaleTracker - Solana Wallet Token Tracker

WhaleTracker, Solana cüzdanlarındaki token'ları takip eden ve analiz eden bir web uygulamasıdır. Kullanıcılar Solana cüzdanlarını ekleyebilir ve bu cüzdanlardaki token'ları manuel olarak güncelleyebilirler.

## Özellikler

- Solana cüzdanlarını ekleme ve yönetme
- Token bilgilerini manuel olarak güncelleme
- Token değerlerini USD cinsinden görüntüleme
- Cüzdan bazında toplam değer hesaplama
- Token adreslerini kopyalama
- Gerçek zamanlı bildirimler (SignalR ile)

## Teknolojiler

### Backend
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- SignalR

### Frontend
- React 19
- TypeScript
- Material-UI v6
- Axios
- React Router v7

### Deployment
- Docker
- Docker Compose

## Kurulum

### Ön Gereksinimler
- Docker Desktop
- Git

### Hızlı Başlangıç

1. Projeyi klonlayın:
```bash
git clone https://github.com/yourusername/whaletracker.git
cd whaletracker
```

2. Uygulamayı başlatın:
Windows için:
```bash
start.bat
```

Linux/Mac için:
```bash
chmod +x start.sh
./start.sh
```

Uygulama aşağıdaki adreslerde çalışacaktır:
- Frontend: http://localhost:3000
- Backend API: http://localhost:5036
- Swagger UI: http://localhost:5036/swagger
- PostgreSQL: localhost:5432

### Manuel Kurulum

Eğer Docker kullanmak istemiyorsanız:

1. PostgreSQL veritabanını kurun ve bir veritabanı oluşturun
2. Backend için connection string'i güncelleyin:
   - `src/WhaleTracker.API/appsettings.json`
   - `src/WhaleTracker.Infrastructure/appsettings.json`

3. Backend'i başlatın:
```bash
cd src/WhaleTracker.API
dotnet ef database update
dotnet run
```

4. Frontend'i başlatın:
```bash
cd src/whaletracker_frontend
npm install
npm start
```

## Kullanım

1. Ana sayfada "Add New Wallet" butonuna tıklayın
2. Solana cüzdan adresini ve opsiyonel olarak bir isim girin
3. Cüzdan detay sayfasında "Manual Token Update" bölümünden token verilerini güncelleyin. Verileri solscan'dan alıyoruz (Inspect > Network > Burada https://api-v2.solscan.io/v2/account/tokens?address= adresini aratıp bu istekte dönen json'u kopyalayıp buraya yapıştırıyoruz)
4. Token verilerini JSON formatında girin:
```json
{
  "success": true,
  "data": {
    "data_type": "onchain",
    "tokens": [
      {
        "tokenAddress": "...",
        "tokenName": "...",
        "tokenSymbol": "...",
        "balance": 100,
        "priceUsdt": 1.5,
        "tokenIcon": "..."
      }
    ]
  }
}
```
