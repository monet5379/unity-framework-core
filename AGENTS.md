# Framework Core — Agent Guide

```text
profile: personal
```

스튜디오 게임 베이스·타이틀에서 공유하는 **Inspector attribute**와 **장르 중립 Utility** UPM 패키지다. Cursor는 **이 repo + unity-studio-kit** 멀티 루트로 연다.

| Kit | 경로 |
|-----|------|
| 공통 | `unity-studio-kit/docs/common/` · `.cursor/rules/common/` |
| 글쓰기 | `unity-studio-kit/docs/common/WritingGuide.md` |
| 프로필 | `unity-studio-kit/docs/personal/` · `.cursor/rules/personal/` |

## 프로젝트 개요

- **목적:** `Monet5379` Inspector attribute·Drawer(에디터)와 공통 Utility를 UPM으로 분리·재사용
- **Git 루트:** 이 저장소 최상단
- **UPM 루트:** `Packages/com.monet5379.framework-core/`

## 레이아웃

```text
unity-framework-core/
├── AGENTS.md
├── README.md
├── docs/architecture/
└── Packages/com.monet5379.framework-core/
    ├── package.json · CHANGELOG.md · README.md
    ├── Runtime/Inspector/     Monet5379.Core.Inspector.asmdef
    ├── Runtime/Utility/         Monet5379.Core.Utility.asmdef
    └── Editor/Inspector/        Monet5379.Core.Inspector.Editor.asmdef
```

## 에이전트가 지킬 것

- Kit **common** + **personal** (README 계약 + `docs/architecture/Architecture_*`; Plan/ADR는 요청 시만).
- `.meta` 생성·수정·삭제 금지 (소비 프로젝트·Unity가 생성).
- 네임스페이스 변경 금지 — 런타임 `Monet5379`, 에디터 `Monet5379.Inspectors` / `Monet5379.Inspectors.Internal`.
- `com.monet5379.conditional-log`는 `package.json` 전이 의존으로 넣지 않는다. README·Architecture에 소비 프로젝트 pin을 적는다.
- 구조·public API·asmdef 경계 변경 시 `docs/architecture/` 갱신. 계약 변경 시 패키지 README 먼저.
- 베이스·타이틀 코드를 코어로 옮길지는 [`docs/CorePromotion.md`](docs/CorePromotion.md) 승격 조건으로 판단한다. 태그 전에는 같은 문서 §태그 전 점검을 본다.
- Shell에서 Unity `-batchmode` · 프로젝트 `scripts/` 래퍼 실행 금지 — Kit `unity-cli-agent.mdc`.
- 커밋은 사용자 **명시 요청** 시에만. Kit CommitMessages — `type(scope): 한글 제목` + **본문(why) 필수**

## asmdef

| asmdef | 역할 |
|--------|------|
| `Monet5379.Core.Inspector` | 런타임 Inspector attribute·compat |
| `Monet5379.Core.Utility` | 장르 중립 Utility (`Utility/Data/` 금지) |
| `Monet5379.Core.Inspector.Editor` | PropertyDrawer·Inspector UI (Editor only) |

`Monet5379.Core.Utility` → `Monet5379.Core.Inspector`, `ConditionalLog.Runtime`. Editor → `Monet5379.Core.Inspector`.

## 이 repo override

| 항목 | 값 |
|------|-----|
| Unity (package.json) | 6000.3 |
| rootNamespace (asmdef) | `Monet5379` |
| UPM id | `com.monet5379.framework-core` |

### 커밋 (이 repo)

| 항목 | 값 |
|------|-----|
| 언어 | 한글 |
| scope (정본) | `package`, `docs` |
