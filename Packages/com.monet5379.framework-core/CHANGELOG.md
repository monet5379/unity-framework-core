# Changelog

이 파일은 [Semantic Versioning](https://semver.org/) 형식을 따른다.

## [Unreleased]

### Changed

- `docs/architecture/` — Event·Localization Architecture에 Flow·변경 가이드 보강 · 목차에 Localization 추가
- repo `README.md` — Event·Localization asmdef 목록 반영

## [0.3.2] - 2026-11-03

### Fixed

- `LocalizedTextContentBinding` — Framework 전용 `UITextEx.ResetText` 호출 제거 (패키지 asmdef에서 CS1061)
- `Runtime/Localization/Localized/` 폴더 `.meta` 추가 (immutable PackageCache import 경고)

### Notes

- Git URL pin: `https://github.com/monet5379/unity-framework-core.git?path=/Packages/com.monet5379.framework-core#v0.3.2`

## [0.3.1] - 2026-11-03

### Fixed

- `Monet5379.Core.Localization` · `Monet5379.Core.Localization.Editor` asmdef 및 신규 Runtime/Editor 스크립트 `.meta` 누락 — UPM import 후 `Monet5379.Core.Localization` 어셈블리 미생성

### Notes

- Git URL pin: `https://github.com/monet5379/unity-framework-core.git?path=/Packages/com.monet5379.framework-core#v0.3.1`

## [0.3.0] - 2026-11-03

### Added

- `Monet5379.Core.Localization` — `LanguageNames` · `ILocalizationSource` · `LocalizationContext` · `LocalizationLogTags` · `UILocalizedText` · binding · `LocalizedTextStyleTable` · `[StringTableKey]`
- `Monet5379.Core.Localization.Editor` — `StringTableKeyDrawer` · catalog (기본 `StringUI.json` · `StringDialogue.json` 경로)
- [Architecture_Localization.md](../../docs/architecture/Architecture_Localization.md)

### Notes

- Git URL pin: `https://github.com/monet5379/unity-framework-core.git?path=/Packages/com.monet5379.framework-core#v0.3.0`
- 소비 프로젝트는 `com.monet5379.conditional-log` · `com.monet5379.tmp-font-pipeline`를 함께 pin한다.

## [0.2.0] - 2026-11-03

### Added

- `Monet5379.Core.Event` — `GlobalCallback` · `GlobalEventBus<TKey>` · `GlobalEventBusRegistry` · `CoreEventLogTags`
- [Architecture_Event.md](../../docs/architecture/Architecture_Event.md)

### Notes

- Git URL pin: `https://github.com/monet5379/unity-framework-core.git?path=/Packages/com.monet5379.framework-core#v0.2.0`
- 소비 프로젝트는 `com.monet5379.conditional-log`를 함께 pin한다 (`Monet5379.Core.Event` · `Monet5379.Core.Utility` asmdef 참조).

## [0.1.2] - 2026-11-03

### Fixed

- `package.json.meta` · `Monet5379.Core.Inspector.Editor.asmdef.meta` — `guid` 32자 16진수 규격 (Unity import 경고·에러)

## [0.1.1] - 2026-11-03

### Fixed

- UPM import — 누락된 폴더·`asmdef` `.meta` 추가 (`Monet5379.Core.*` 컴파일)
- `Runtime/Utility/Extension` GUID — `Assets/Project/Scripts/Utility/Extension`과 충돌 제거

## [0.1.0] - 2026-11-03

### Added

- UPM 골격 — `Monet5379.Core.Inspector` · `Monet5379.Core.Utility` · `Monet5379.Core.Inspector.Editor` asmdef
- `unity-framework`에서 Inspector·Utility·Editor Inspector 코드 이전 (별도 작업)

### Notes

- Git URL pin (릴리스 후): `https://github.com/monet5379/unity-framework-core.git?path=/Packages/com.monet5379.framework-core#v0.1.0`
- 소비 프로젝트는 `com.monet5379.conditional-log`를 함께 pin한다 (`Monet5379.Core.Utility` asmdef 참조).
