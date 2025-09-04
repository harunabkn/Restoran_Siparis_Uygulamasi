# 🍽️ Restoran Sipariş Otomasyonu  

Restoran Sipariş Otomasyonu, restoranlarda sipariş ve ödeme süreçlerinin dijital ortamda yönetilmesini sağlayan bir Windows Form uygulamasıdır. Proje; sipariş yönetimi, personel yetkilendirmesi, stok takibi ve raporlama gibi işlevleri kapsayan kapsamlı bir otomasyon sistemidir.  

---

## 🚀 Özellikler  

- **Kullanıcı Yetkilendirme & Roller**  
  - **Müdür:** Personel yönetimi, masa ekleme/çıkarma, günlük raporlama.  
  - **Şef:** Menü düzenleme, yemek ekleme/çıkarma, fiyat güncelleme.  
  - **Kasa Personeli:** Sipariş kapatma, ödeme alma, günlük gelir/gider raporu.  
  - **Garson:** Masa durumu yönetimi, sipariş alma/düzenleme.  

- **Sipariş Yönetimi**  
  - Masalara bağlı sipariş oluşturma, güncelleme ve iptal etme.  
  - Sipariş detayları üzerinde ürün/miktar bazlı kontrol.  
  - Sipariş durumu takibi (*Hazırlanıyor, Tamamlandı, İptal*).  

- **Menü Yönetimi**  
  - Kategorilere göre menü oluşturma.  
  - Yeni ürün ekleme, ürün fiyat ve stok güncelleme.  

- **Stok Takibi**  
  - Ürün stok kontrolü.  
  - Stoğu biten ürünleri sistemden otomatik kaldırma.  

- **Ödeme & Raporlama**  
  - Nakit/Kart gibi farklı ödeme yöntemlerini destekleme.  
  - Günlük ve detaylı işletme raporları.  
  - Personel bazlı performans raporu.  

---

## 🗃️ Veritabanı Tasarımı  

Projede **MsSQL** kullanılmıştır. Başlıca tablolar:  

- **Personel** → Çalışan bilgileri (rol, kullanıcı adı, maaş, iletişim vb.)  
- **Masa** → Masa kapasitesi ve durum bilgileri (*Boş, Dolu, Temizlik Gerekiyor*).  
- **Ürün** → Yemek ve içecek bilgileri (fiyat, stok, kategori).  
- **Sipariş** → Sipariş temel bilgileri (hangi masa, hangi garson, zaman).  
- **Sipariş Detay** → Sipariş edilen ürünlerin detayları.  
- **Ödeme** → Siparişe bağlı ödeme bilgileri.  
- **Sipariş Durumu** → Siparişlerin aşamalarını takip eden tablo.  
- **Menü & Menü-Ürün İlişkisi** → Aktif menülerin yönetimi.  

---

## 💻 Kullanılan Teknolojiler  

- **C# .NET Windows Forms** → Arayüz ve iş mantığı geliştirme.  
- **MsSQL** → Veritabanı tasarımı ve yönetimi.  
- **Entity Framework / ADO.NET** → Veritabanı işlemleri (CRUD operasyonları).  
- **GitHub & Git** → Versiyon kontrolü ve takım çalışması.  

---

## 🖥️ Arayüz Özellikleri  

- **Giriş Paneli** → Kullanıcı rolüne göre farklı arayüzlere yönlendirme.  
- **Ana Menü** → Kullanıcının yetkisine göre farklı menü butonları.  
- **Masalar Ekranı** → Masa doluluk durumu takibi ve düzenlemesi.  
- **Menü Yönetimi** → Ürün ekleme, düzenleme ve silme ekranı.  
- **Ödeme Sayfası** → Sipariş kapatma ve farklı ödeme türleriyle işlem yapabilme.  

---

## 📌 Proje Yapısı  
📦 RestoranSiparisOtomasyonu
├─ 📂 Database # MSSQL tabloları ve ilişkileri
├─ 📂 Forms # Windows Form arayüzleri (UI/UX)
├─ 📂 BusinessLogic # Sipariş, ödeme ve raporlama iş mantığı
├─ 📂 Models # Veritabanı modelleri
├─ 📂 Reports # Günlük/aylık raporlama modülü
└─ Program.cs # Uygulama giriş noktası


---

## ⚡ Kurulum & Çalıştırma  

1. **Projeyi klonlayın**  
   ```bash
   git clone https://github.com/kullaniciadi/RestoranSiparisOtomasyonu.git

Veritabanını oluşturun

Database/Script.sql dosyasını MsSQL üzerinde çalıştırarak tabloları oluşturun.
Uygulamayı çalıştırın

Visual Studio içerisinde projeyi açın.
app.config içerisindeki veritabanı bağlantı ayarlarını güncelleyin.
Debug/Run ile uygulamayı başlatın.
👨‍💻 Geliştirici Ekibi
Erhan Dündar → Backend & İş Mantığı
Ezgi Kaplan → UI/UX & Form Tasarımı
Harun Abukan → Veritabanı & Veri Yönetimi

📜 Lisans
Bu proje eğitim amaçlı geliştirilmiştir.

text
MIT License
