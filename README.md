# PosElChorolqui — Etapa 1: fundación

Punto de venta local para El Chorolquí. Esta etapa crea la fundación: solución .NET 10 con backend local (ASP.NET Core), cliente de escritorio (Avalonia) y persistencia SQLite lista para crecer. Sin ventas, productos, inventario ni nube.

## Ruta rápida

1. Instalar el SDK de .NET 10 (10.0.403 o superior): `dotnet --version`.
2. Iniciar el servicio local:
   ```powershell
   dotnet run --project src/PosElChorolqui.LocalHost
   ```
3. Comprobar el estado en otra terminal:
   ```powershell
   curl http://127.0.0.1:5117/health
   ```
   Respuesta esperada: `{"service":"PosElChorolqui.LocalHost","status":"Healthy","checkedAtUtc":"..."}`.
4. Iniciar el escritorio (con el servicio en ejecución):
   ```powershell
   dotnet run --project src/PosElChorolqui.Desktop
   ```
   La ventana muestra `PosElChorolqui` y el estado real del servicio.

## Detalles

| Tema | Decisión |
|------|----------|
| Solución | `PosElChorolqui.sln`: 6 proyectos en `src/`, pruebas en `tests/`. Respetar las mayúsculas exactas del nombre en todo el código y la documentación. |
| Servicio local | `GET /health` devuelve el nombre del servicio, el estado y la hora UTC. Escucha solo en `http://127.0.0.1:<puerto>`; el puerto sale de `LocalHost:Port` (`appsettings.json`, valor inicial 5117). Nunca se enlaza `0.0.0.0`. |
| Configuración | Cada host tiene `appsettings.json` + `appsettings.example.json`. El archivo `.example.json` es la referencia que se publica; los valores reales o secretos van en archivos locales ignorados por git. |
| Escritorio | Llama a `GET /health` al iniciar y con el botón `Actualizar`. Muestra `Estado del servicio: Conectado` o `No disponible: <motivo>`. Habla con el servicio solo por HTTP en loopback; nunca referencia el dominio ni SQLite. |
| Base de datos | EF Core 10 + SQLite. La cadena de conexión sale de `ConnectionStrings:PosDatabase` (`Data Source=poselchorolqui.db` por defecto). No se publica ningún archivo `.db`/`.sqlite` con datos reales. |
| Comandos | `dotnet build PosElChorolqui.sln` · `dotnet test` · `dotnet run --project src/PosElChorolqui.LocalHost` · `dotnet run --project src/PosElChorolqui.Desktop` |
| Fuera de alcance | Ventas, productos, inventario, usuarios, móvil, Docker, Azure, PostgreSQL o cualquier servicio en la nube. |

## Lista de comprobación

- [ ] `dotnet build PosElChorolqui.sln` termina sin errores.
- [ ] `curl http://127.0.0.1:5117/health` devuelve `Healthy`.
- [ ] El escritorio muestra `Estado del servicio: Conectado` con el servicio en ejecución.
- [ ] Con el servicio detenido, el escritorio muestra `No disponible: <motivo>` en lugar de fallar.
- [ ] `git status` no muestra archivos `.db`, `.sqlite` ni secretos.

## Siguiente paso

Leer `docs/architecture.md` para el mapa del monolito modular y el camino hacia servicio de Windows y nube.
