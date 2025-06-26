# HttpClient to Curl Visualizer

Visual Studio 2022 için HttpClient response'larını curl komutuna çeviren özel bir debugger visualizer.

## 🚀 Özellikler

- ✅ HttpResponseMessage objelerini curl komutuna çevirir
- ✅ HTTP method'ları destekler (GET, POST, PUT, DELETE, vb.)
- ✅ Tüm header'ları dahil eder
- ✅ Request body'sini ekler
- ✅ Kullanıcı dostu interface
- ✅ Tek tıkla kopyalama özelliği

## 📦 Kurulum

### Otomatik Kurulum

1. [Releases](https://github.com/ikeskin/httpclient-curl-visualizer/releases) sayfasından en son sürümü indirin
2. ZIP dosyasını çıkarın
3. `HttpClientCurlVisualizer.dll` dosyasını şu konuma kopyalayın:
   ```
   %USERPROFILE%\Documents\Visual Studio 2022\Visualizers\
   ```
4. Visual Studio'yu yeniden başlatın

### Manuel Kurulum

```bash
# Repository'yi klonlayın
git clone https://github.com/ikeskin/httpclient-curl-visualizer.git
cd httpclient-curl-visualizer

# Projeyi derleyin
msbuild HttpClientCurlVisualizer.sln /p:Configuration=Release

# DLL'i kopyalayın
copy "HttpClientCurlVisualizer\bin\Release\HttpClientCurlVisualizer.dll" "%USERPROFILE%\Documents\Visual Studio 2022\Visualizers\"
```

## 🔧 Kullanım

1. HttpClient ile HTTP isteği yapan kodunuzu debug modunda çalıştırın
2. `HttpResponseMessage` nesnesinin üzerine mouse ile gelin
3. Magnifying glass (🔍) ikonuna tıklayın
4. "HTTP Response to Curl" seçeneğini seçin
5. Açılan pencerede curl komutunu görebilir ve kopyalayabilirsiniz

## 📸 Ekran Görüntüleri

```csharp
// Örnek kullanım
using var client = new HttpClient();
var response = await client.GetAsync("https://api.example.com/data");
// Debug sırasında response değişkeninin üzerine gelin
```

## 🏗️ Geliştirme

### Gereksinimler

- Visual Studio 2022
- .NET Framework 4.7.2 veya üzeri
- Microsoft.VisualStudio.DebuggerVisualizers package

### Build

```bash
# Debug build
msbuild HttpClientCurlVisualizer.sln /p:Configuration=Debug

# Release build
msbuild HttpClientCurlVisualizer.sln /p:Configuration=Release
```

## 🤝 Katkıda Bulunma

1. Fork edin
2. Feature branch oluşturun (`git checkout -b feature/amazing-feature`)
3. Değişikliklerinizi commit edin (`git commit -m 'Add amazing feature'`)
4. Branch'inizi push edin (`git push origin feature/amazing-feature`)
5. Pull Request oluşturun

## 📄 Lisans

Bu proje MIT lisansı altında lisanslanmıştır. Detaylar için [LICENSE](LICENSE) dosyasını inceleyebilirsiniz.

## 🐛 Sorun Bildirimi

Herhangi bir sorunla karşılaştığınızda [Issues](https://github.com/ikeskin/httpclient-curl-visualizer/issues) sayfasından bildirebilirsiniz.

## 📝 Notlar

- HttpResponseMessage'da RequestMessage null olabilir. Bu durumda visualizer uygun bir uyarı mesajı gösterecektir.
- Visualizer sadece Visual Studio 2022 ile uyumludur.
- Curl komutlarında özel karakterler otomatik olarak escape edilir.

## 🔗 Bağlantılar

- [Visual Studio Marketplace](https://marketplace.visualstudio.com/) (Yakında)
- [NuGet Package](https://www.nuget.org/) (Yakında)