# 🧹 Clean Architecture Projesi: Basit Task Yönetimi 🚀
Bu proje, Clean Architecture prensiplerine uygun olarak geliştirilmiş basit bir task (görev) yönetimi uygulamasıdır
Proje, katmanlı mimariyi ve SOLID prensiplerini kullanarak geliştirilmiştir. Taner Saydam'ın Udemy kursundan hazırlanmıştır.
# 🌟 Öne Çıkan Özellikler
- Clean Architecture: Katmanlı mimari ile geliştirilmiş, bağımsız ve test edilebilir bir yapı.

- SOLID Prensipleri: Kodun esnek, sürdürülebilir ve genişletilebilir olması sağlanmıştır.

- Entity Framework Core: Veritabanı işlemleri için ORM aracı.

- MediatR: CQRS (Command Query Responsibility Segregation) pattern'ı kullanılarak geliştirilmiştir.

- Dependency Injection: ASP.NET Core'un yerleşik DI mekanizması ile servislerin yönetimi.

- Swagger: API dokümantasyonu için Swagger entegrasyonu.

# 🏗️ Mimari Yapısı
Proje, Clean Architecture'ın temel katmanlarına ayrılmıştır:

## 1. Domain Layer (Çekirdek Katman)
Açıklama: Projenin çekirdek iş mantığını içerir. Entity'ler, value object'ler, interface'ler ve domain servisleri bu katmanda bulunur.
Örnek: Task entity'si ve ITaskRepository interface'i.

## 2. Application Layer (Uygulama Katmanı)
Açıklama: İş kurallarını ve use case'leri içerir. CQRS pattern'ı kullanılarak geliştirilmiştir.
Örnek: CreateTaskCommand, GetAllTasksQuery gibi command ve query'ler.

## 3. Infrastructure Layer (Altyapı Katmanı)
Açıklama: Veritabanı erişimi, harici servisler gibi teknik detaylar bu katmanda bulunur.
Örnek: TaskRepository ve DbContext implementasyonları.

## 4. Presentation Layer (Sunum Katmanı)
Açıklama: Kullanıcı arayüzü veya API endpoint'leri bu katmanda bulunur.
Örnek: TasksController ve Swagger dokümantasyonu.

# 🛠️ Kurulum ve Çalıştırma
- Visual Studio 2022
- MSSQL

# Adım Adım Kurulum
- Depoyu Klonlayın:
- Veritabanını Ayarlayın:appsettings.json dosyasında veritabanı bağlantı dizesini güncelleyin.
- Projeyi Çalıştırın:
- Swagger ile API'yi Test Edin:
  
# 🚀 Nasıl Çalışır?
Proje, Clean Architecture prensiplerine uygun olarak geliştirilmiştir. İşte temel iş akışı:

## 1.Task Oluşturma:
CreateTaskCommand kullanılarak yeni bir task oluşturulur.Task, veritabanına kaydedilir.
- Task Listeleme:
GetAllTasksQuery kullanılarak tüm task'lar listelenir.
- Task Güncelleme:
UpdateTaskCommand kullanılarak mevcut bir task güncellenir.
- Task Silme:
DeleteTaskCommand kullanılarak bir task silinir.

# 📂 Proje Yapısı
![image](https://github.com/user-attachments/assets/3d772699-5b62-479c-af40-72405d7aed52)

  
