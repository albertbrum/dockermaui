# Guia de Configuração do Projeto Dockermaui

Este documento contém o passo a passo completo para configurar e executar o projeto Dockermaui, que inclui aplicações MAUI para Windows e Android, além de uma aplicação web Blazor.

## Pré-requisitos

- **Sistema Operacional**: Windows 10/11
- **VS Code**: Instalado com extensão C# (ms-dotnettools.csharp)
- **Git**: Para clonar o repositório (opcional)

## 1. Instalação do .NET 10 SDK

1. Baixe o .NET 10 SDK de [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download).
2. Instale o SDK x64 para Windows.
3. Verifique a instalação: `dotnet --version` (deve mostrar 10.x.x).

## 2. Instalação dos Workloads MAUI

No terminal, execute:
```
dotnet workload install maui
dotnet workload install maui-android
```

## 3. Atualização do Projeto para .NET 10

Os arquivos .csproj foram atualizados para .NET 10:
- `WebApp/WebApp.csproj`: TargetFramework = net10.0
- `SharedUI/SharedUI.csproj`: TargetFramework = net10.0, PackageReference Microsoft.AspNetCore.Components.Web = 10.0.0
- `AppMaui/AppMaui.csproj`: Já estava em net10.0
- `dockerfile`: Imagens atualizadas para .NET 10

## 4. Configuração do VS Code

### Arquivos .vscode criados:
- `.vscode/launch.json`: Configurações de debug para Windows, Android e Web.
- `.vscode/tasks.json`: Tarefas de build para cada plataforma.

### Como executar pela GUI:
1. Abra o painel Run and Debug (`Ctrl + Shift + D`).
2. Selecione a configuração desejada:
   - ".NET MAUI (Windows)"
   - ".NET MAUI (Android)"
   - ".NET Web App"
3. Clique em ▶️ para executar.

## 5. Configuração da Aplicação Web

- A página inicial (`/`) agora tem uma tela de login.
- Credenciais: Usuário `admin`, Senha `password`.
- Após login, redireciona para `/counter`.

### Para executar:
```
cd WebApp
dotnet run
```
Acesse em `http://localhost:5006`.

## 6. Configuração da Aplicação MAUI para Windows

### Para executar:
```
cd AppMaui
dotnet run --framework net10.0-windows10.0.19041.0
```

## 7. Configuração da Aplicação MAUI para Android

### Instalação do Android SDK:
1. Instale o Android Studio de [https://developer.android.com/studio](https://developer.android.com/studio).
2. No SDK Manager, instale:
   - SDK Platforms: Android 14 (API 34) ou Android 7.1 (API 25)
   - SDK Tools: Android SDK Build-Tools, Android Emulator, etc.
   - Android SDK Command-line Tools

### Configuração de Variáveis de Ambiente:
```
setx ANDROID_HOME "C:\Users\%USERNAME%\AppData\Local\Android\Sdk"
setx PATH "%PATH%;%ANDROID_HOME%\platform-tools;%ANDROID_HOME%\tools;%ANDROID_HOME%\tools\bin;%ANDROID_HOME%\cmdline-tools\latest\bin;%ANDROID_HOME%\emulator"
```

**Para usuários do PowerShell:**
```
[Environment]::SetEnvironmentVariable("ANDROID_HOME", "$env:USERPROFILE\AppData\Local\Android\Sdk", "User")
$path = [Environment]::GetEnvironmentVariable("PATH", "User")
$newPath = "$path;$env:ANDROID_HOME\platform-tools;$env:ANDROID_HOME\tools;$env:ANDROID_HOME\tools\bin;$env:ANDROID_HOME\cmdline-tools\latest\bin;$env:ANDROID_HOME\emulator"
[Environment]::SetEnvironmentVariable("PATH", $newPath, "User")
```

### Instalação do JDK:
1. Baixe JDK 21 de [https://www.oracle.com/br/java/technologies/downloads/#jdk21-windows](https://www.oracle.com/br/java/technologies/downloads/#jdk21-windows).
2. Configure:
```
setx JAVA_HOME "C:\Program Files\Java\jdk-21.0.10"
setx PATH "%PATH%;%JAVA_HOME%\bin"
```

**Para usuários do PowerShell:**
```
[Environment]::SetEnvironmentVariable("JAVA_HOME", "C:\Program Files\Java\jdk-21.0.10", "User")
$path = [Environment]::GetEnvironmentVariable("PATH", "User")
$newPath = "$path;$env:JAVA_HOME\bin"
[Environment]::SetEnvironmentVariable("PATH", $newPath, "User")
```
3. Verifique a instalação: `java -version` (deve mostrar 21.x.x).

### Criação do Emulador:
No terminal do VS Code, configure o ambiente primeiro:
```
$env:ANDROID_HOME = "$env:USERPROFILE\AppData\Local\Android\Sdk"
$env:PATH += ";$env:ANDROID_HOME\platform-tools;$env:ANDROID_HOME\tools;$env:ANDROID_HOME\tools\bin;$env:ANDROID_HOME\cmdline-tools\latest\bin;$env:ANDROID_HOME\emulator"
```

Então, execute:
```
sdkmanager --licenses  # Aceite todas as licenças (pressione 'y' para cada uma)
sdkmanager --install "system-images;android-25;default;x86"
avdmanager create avd -n "MeuEmulador" -k "system-images;android-25;default;x86" --force
emulator -avd MeuEmulador
```

**Nota**: Se o pacote não estiver disponível, verifique com `sdkmanager --list | findstr android-25` e use o nome correto.

### Para executar o app no emulador:
Selecione ".NET MAUI (Android)" no VS Code Run panel.

## 8. Usando Docker (Opcional)

O projeto inclui um `dockerfile` para executar a versão web em container.

### Construir e executar:
```
docker build -t dockermaui-web .
docker run -d -p 8080:8080 --name dockermaui dockermaui-web
```
Acesse em `http://localhost:8080`.

## 9. Estrutura do Projeto

- `AppMaui/`: Aplicação MAUI (Windows, Android, iOS, Mac)
- `WebApp/`: Aplicação web Blazor
- `SharedUI/`: Componentes compartilhados
- `dockerfile`: Para containerização
- `RunContainer.bat`: Script para executar o container

## 10. Solução de Problemas

- **Erro de porta ocupada**: Use `netstat -ano | findstr :porta` para encontrar o PID, então `taskkill /PID <PID> /F`.
- **Build falha para Android**: Verifique se Android SDK e JDK estão configurados.
- **Emulador não inicia**: Certifique-se de que HAXM ou WHPX está habilitado.

## 11. Próximos Passos

- Teste todas as plataformas.
- Personalize as aplicações conforme necessário.
- Para produção, configure CI/CD com GitHub Actions ou similar.

Se tiver dúvidas, consulte este guia ou os logs de erro.