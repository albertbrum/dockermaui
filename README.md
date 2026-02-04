# 📝 DOTNET MAUI MULTI-PLATFORM PROJECT
Dotnet maui project with a docker container for web deployment

---
# ⚙️ STRUCTURE
The project contains the following structure for the native deployment (Android, IOS, Windows) + Blazor web application.

```
/DOCKERMAUI (Root)
│
├── /SharedUI           # (Razor Class Library)
├── /AppMaui            # (Projeto Nativo - Android/Windows/iOS)
├── /WebApp             # (Projeto Blazor Server/Wasm - O que vai pro Docker)
│
└── Dockerfile          # (Config)

```