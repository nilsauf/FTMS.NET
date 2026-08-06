# AGENTS.md

FTMS.NET is a .NET client library for the Bluetooth LE **Fitness Machine Service (FTMS)**. The Bluetooth spec PDFs live in `docs/` (`FTMS_v1.0-1.pdf` is the authoritative FTMS spec; `Assigned_Numbers.pdf` and `GATT_Specification_Supplement.pdf` are context).

## Layout

- `FTMS.NET/` — the library. Subfolders mirror the FTMS feature areas: `Control/`, `Data/`, `Features/`, `State/`, `Exceptions/`, `Utils/`.
- `FTMS.NET.Tests/` — xUnit v3 + Moq + Microsoft.Reactive.Testing + coverlet. One test class file per source area.
- `docs/` — the Bluetooth FTMS spec PDFs; check these before changing parsing/format code.

## Build & test

Both projects multi-target `net8.0;net9.0;net10.0` (set in `Directory.Build.props`), so a bare `dotnet test` runs the suite three times.

- Fast, focused verification: `dotnet test -f net10.0` (or `dotnet build -f net10.0`).
- No lint/format script exists; style is enforced by `.editorconfig` via `dotnet format`.
- `dotnet pack` produces the NuGet package; the version is derived from git by GitVersion (GitHubFlow, `main` label `alpha`) — never hand-edit version numbers.

## Architecture (read this before touching the public surface)

- The library does **not** do BLE. Callers supply an `IFitnessMachineServiceConnection` wrapping their BLE library; `FitnessMachineServiceFactory` extension methods on that interface assemble `IFitnessMachineService` (`FitnessMachineServiceFactory.cs`).
- `FitnessMachineService` itself is `internal`; the public API is the `IFitnessMachineService*` interfaces.
- Optional characteristics (Control Point, Machine/Training State) are swapped for `ThrowingCharacteristic` when absent; required ones throw `NeededCharacteristicNotAvailableException` via `EnsureAvailableCharacteristic`.
- `FtmsUuids.cs` builds a UUID→name dictionary with source-generated reflection (`[SourceReflection]`, `SourceGeneration.Reflection`, kept AOT-friendly). To register a new UUID, add it as a `public static readonly Guid` field — it is picked up automatically.
- Live data flows out as a DynamicData `IChangeSet<IFitnessMachineValue, Guid>` (`IFitnessMachineService.Connect()`).

## Domain gotchas (all were real bugs)

- Bit indexing is **standard 8-bit, LSB-first** (`IsBitSet(pos)` with pos 0 = LSB). A previous bug used 7-bit indexing — do not reintroduce it.
- FTMS feature-flag and frame byte offsets have been wrong before (e.g. Target Setting Features offset, UInt24 field encoding). Verify offsets against `docs/FTMS_v1.0-1.pdf` before changing.
- Little-endian values; some fields are `UInt24` (`FTMS.NET/Utils/UInt24.cs`).
- **No range validation is intentional**: control requests do not validate against the machine's advertised feature ranges (see README "Remarks"). Do not add it.

## Testing conventions

- Tests run through a real `IFitnessMachineServiceConnection` fake (`FakeConnection`/`FakeCharacteristic` in `FitnessMachineServiceFactory.Tests.cs`) or Moq for internals — `InternalsVisibleTo("FTMS.NET.Tests")` grants access.
- Reactive streams are tested with `Microsoft.Reactive.Testing` (ReactiveUI's TestScheduler).
- Run the full `dotnet test` (all TFMs) before finishing; the single-TFM run is only for fast iteration.

## Release / CI

`.github/workflows/cicd.yml`: build+test (with coverage) → pack → upload artifacts; publishes to NuGet (secret `NUGET_API_KEY`) from `release/*` branches, `main`, and on GitHub releases. Local debug builds are fine for iteration.
