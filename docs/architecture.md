# Arquitectura — PosElChorolqui Etapa 1

Monolito modular offline-first: el servicio local es dueño de los datos y el escritorio es un cliente HTTP más. Esta etapa deja el esqueleto y las reglas; las funciones de negocio llegan en etapas posteriores.

## Ruta rápida

1. El escritorio (`Desktop`) solo conoce `Contracts` y llama al servicio por HTTP en loopback.
2. El servicio (`LocalHost`) compone `Application` + `Infrastructure` y expone `GET /health`.
3. Agregar una función nueva significa: contrato → dominio → caso de uso → persistencia → endpoint → pantalla. Nunca al revés.

## Detalles

| Tema | Decisión |
|------|----------|
| Estilo | Monolito modular en una sola solución. Cada proyecto tiene una dirección de dependencia fija (ver `AGENTS.md`); si aparece un ciclo, el diseño está mal, no la regla. |
| Capas | `Contracts` (DTO compartidos, sin dependencias) → `Domain` (entidades y reglas; hoy solo `BaseEntity` con auditoría) → `Application` (puertos y servicios reutilizables: `IClock`, `IHealthService`) → `Infrastructure` (EF Core + SQLite, `PosDbContext`, registro por cadena de conexión) → `LocalHost` (composición y endpoints). |
| Escritorio | Avalonia + MVVM con CommunityToolkit. `MainViewModel` consume `BackendHealthClient` (HttpClient tipado con URL base y timeout desde configuración). Sin datos inventados: si el servicio no responde, la pantalla lo dice. |
| Servicio local | Minimal API con `PosHost.CreateBuilder` separado de `Program` para poder instalarlo como servicio de Windows más adelante (en esta etapa no hay instalación). Enlace fijo a `127.0.0.1`; solo el puerto es configurable (`LocalHost:Port`, inicial 5117). Errores de borde con `ProblemDetails`. |
| Persistencia | SQLite vía `AddPosPersistence`, cadena `ConnectionStrings:PosDatabase`. Sin entidades mapeadas todavía; `ApplyConfigurationsFromAssembly` recogerá las configuraciones futuras. Sin migraciones en esta etapa. |
| Configuración | `appsettings.json` para valores locales, `appsettings.example.json` como referencia publicada. `appsettings.Development.json` y `*.local.json` están ignorados: nunca contienen secretos publicados. |
| Evolución | Camino previsto: entidades de negocio → migraciones EF → servicio de Windows (reutilizando `PosHost`) → cliente móvil y sincronización en la nube. Nada de eso existe hoy y no se simula. |

## Lista de comprobación

- [ ] Un cambio en `Domain` no obliga a tocar `Desktop`.
- [ ] `Desktop` no referencia `Domain`, `Application` ni `Infrastructure`.
- [ ] El servicio responde en loopback y rechaza cualquier intento de exposición externa (enlace fijo en código).
- [ ] Ningún archivo `.db`/`.sqlite` ni secreto llega al repositorio.

## Siguiente paso

Ver `AGENTS.md` para las convenciones de trabajo que mantienen este mapa vigente.
