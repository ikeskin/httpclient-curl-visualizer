# Kurulum Talimatları

## Sistem Gereksinimleri

- Windows 10/11
- Visual Studio 2022 (Community, Professional, veya Enterprise)
- .NET Framework 4.7.2 veya üzeri

## Adım Adım Kurulum

### 1. DLL'i İndirin

GitHub Releases sayfasından `HttpClientCurlVisualizer.dll` dosyasını indirin.

### 2. Visualizers Klasörünü Bulun

Visualizers klasörü genellikle şu konumlarda bulunur:

**Varsayılan konum:**
```
C:\Users\[KullanıcıAdı]\Documents\Visual Studio 2022\Visualizers\
```

**Alternatif konumlar:**
- `%USERPROFILE%\Documents\Visual Studio 2022\Visualizers\`
- `%USERPROFILE%\My Documents\Visual Studio 2022\Visualizers\`

### 3. Klasör Oluşturun (Gerekirse)

Eğer Visualizers klasörü mevcut değilse:

1. File Explorer'ı açın
2. `%USERPROFILE%\Documents\Visual Studio 2022\` konumuna gidin
3. Sağ tık yapın ve "New Folder" seçin
4. Klasör adını "Visualizers" yapın

### 4. DLL'i Kopyalayın

`HttpClientCurlVisualizer.dll` dosyasını Visualizers klasörüne kopyalayın.

### 5. Visual Studio'yu Yeniden Başlatın

Değişikliklerin etkili olması için Visual Studio'yu tamamen kapatıp yeniden açın.

## Kurulumu Doğrulama

1. Visual Studio'da HttpClient kullanan bir proje açın
2. Debug modunda çalıştırın
3. `HttpResponseMessage` tipinde bir değişkenin üzerine mouse ile gelin
4. Magnifying glass (🔍) ikonunu görmelisiniz
5. İkona tıkladığınızda "HTTP Response to Curl" seçeneğini görmelisiniz

## Sorun Giderme

### Visualizer Görünmüyor

1. **DLL doğru konumda mı?**
   - Dosya yolunu kontrol edin
   - Klasör adının "Visualizers" olduğundan emin olun

2. **Visual Studio yeniden başlatıldı mı?**
   - VS'yi tamamen kapatıp açın
   - Gerekirse bilgisayarı yeniden başlatın

3. **Doğru VS sürümü mü?**
   - Bu visualizer sadece Visual Studio 2022 için tasarlanmıştır

### Hata Mesajları

**"Could not load file or assembly"**
- .NET Framework 4.7.2 kurulu olduğundan emin olun
- Visual Studio'nun güncel olduğundan emin olun

**"RequestMessage is null"**
- Bu normal bir durumdur, HttpResponseMessage'da request bilgisi mevcut değil
- HttpClient'ı kullanırken request referansını saklayın

## Manuel Build (Geliştiriciler İçin)

```bash
# Repository'yi klonlayın
git clone https://github.com/ikeskin/httpclient-curl-visualizer.git
cd httpclient-curl-visualizer

# Build edin
msbuild HttpClientCurlVisualizer.sln /p:Configuration=Release

# Sonucu kopyalayın
copy "HttpClientCurlVisualizer\bin\Release\HttpClientCurlVisualizer.dll" "%USERPROFILE%\Documents\Visual Studio 2022\Visualizers\"
```

## Kaldırma

Visualizer'ı kaldırmak için:

1. `%USERPROFILE%\Documents\Visual Studio 2022\Visualizers\` klasörüne gidin
2. `HttpClientCurlVisualizer.dll` dosyasını silin
3. Visual Studio'yu yeniden başlatın

## Destek

Sorunlarınız için:
- [GitHub Issues](https://github.com/ikeskin/httpclient-curl-visualizer/issues)
- [Documentation](https://github.com/ikeskin/httpclient-curl-visualizer/wiki)