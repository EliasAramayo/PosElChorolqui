# AGENTS.md — PosElChorolqui

Convenciones obligatorias para trabajar en este repositorio. El código se escribe en inglés; el texto visible y la documentación, en español neutro y profesional.

## Ruta rápida

1. Escribir el código y los identificadores en inglés; la interfaz y los mensajes, en español neutro (sin argentinismos ni voseo en los artefactos).
2. El servicio es dueño de SQLite; el escritorio solo llama por HTTP en loopback.
3. Comandos: `dotnet build PosElChorolqui.sln`, `dotnet test`, `dotnet run --project src/PosElChorolqui.LocalHost`, `dotnet run --project src/PosElChorolqui.Desktop`.

## Detalles

| Tema | Convención |
|------|------------|
| Nombre | La solución y los proyectos se llaman exactamente `PosElChorolqui` (respetar mayúsculas en código, archivos y documentos). |
| Idioma | Código, clases, métodos, variables y comentarios en inglés. Texto de interfaz, mensajes de error y documentos (`README.md`, `docs/`, este archivo) en español neutro y profesional. |
| Dependencias | `Contracts` no referencia a nadie. `Domain` → `Contracts`. `Application` → `Domain`, `Contracts`. `Infrastructure` → `Domain`, `Application`, `Contracts`. `LocalHost` → `Application`, `Infrastructure`, `Contracts`, `Domain`. `Desktop` → solo `Contracts`. `UnitTests` → `Domain`, `Application`, `Contracts`. Sin ciclos. |
| Backend | Dueño de SQLite mediante `AddPosPersistence`. Cadena en `ConnectionStrings:PosDatabase`. Puerto en `LocalHost:Port` (inicial 5117); enlace siempre a `127.0.0.1`, nunca a `0.0.0.0`. Endpoints mínimos con manejo de errores en el borde (`ProblemDetails`). |
| Escritorio | MVVM con CommunityToolkit. `BackendHealthClient` con URL base y timeout desde configuración. Estado real del servicio en pantalla (`Estado del servicio: Conectado` / `No disponible: <motivo>`); prohibidos los datos inventados o las pantallas de mentira. |
| Configuración | `appsettings.json` local + `appsettings.example.json` publicado. No publicar `appsettings.Development.json`, `*.local.json`, `.db`/`.sqlite` ni secretos. Se publican las migraciones y los `*.example.json`. |
| Alcance | Sin ventas, productos, inventario, usuarios, móvil, Docker ni nube hasta que una etapa los incluya. No agregar funciones fuera de la etapa en curso. |

## Lista de comprobación

- [ ] El cambio respeta el grafo de dependencias y el idioma de cada artefacto.
- [ ] `dotnet build PosElChorolqui.sln` y `dotnet test` pasan antes de entregar.
- [ ] `git status` muestra solo archivos intencionales (sin binarios, bases de datos ni secretos).

## Siguiente paso

Ante una duda de diseño, consultar `docs/architecture.md` antes de crear un proyecto o invertir una dependencia.
