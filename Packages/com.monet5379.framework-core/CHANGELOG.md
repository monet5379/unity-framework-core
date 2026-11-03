# Changelog

이 파일은 [Semantic Versioning](https://semver.org/) 형식을 따른다.

## [Unreleased]

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
