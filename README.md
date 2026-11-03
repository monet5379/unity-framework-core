# Framework Core

`com.monet5379.framework-core` — 스튜디오 공통 **Inspector attribute**와 **Utility** UPM 패키지.

## 포함

- **Runtime/Inspector** — `[Button]`, `[FoldoutGroup]` 등 Inspector attribute (`Monet5379.Core.Inspector`)
- **Runtime/Utility** — 확장 메서드·컬렉션·Behaviour 베이스 등 (`Monet5379.Core.Utility`)
- **Editor/Inspector** — PropertyDrawer·Inspector 확장 (`Monet5379.Core.Inspector.Editor`)

설치·의존·asmdef 참조: [`Packages/com.monet5379.framework-core/README.md`](Packages/com.monet5379.framework-core/README.md).

아키텍처: [`docs/architecture/`](docs/architecture/) (추가 예정).

## 개발

소스는 `Packages/com.monet5379.framework-core/`만 UPM 패키지다. 호스트 Unity 프로젝트는 소비 repo(`unity-framework` 등)에서 `file:` 또는 Git URL로 연결한다.
