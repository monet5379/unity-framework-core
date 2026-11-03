# Framework Core

스튜디오 게임 베이스·타이틀에서 공유하는 Inspector attribute와 장르 중립 Utility UPM 패키지.

## 요구 사항

- Unity **6000.3** 이상 (`package.json`)
- 패키지 의존: `com.unity.ugui` (`ImageEx` 등 UI 확장)
- **함께 pin할 패키지:** `com.monet5379.conditional-log` — UPM git 패키지는 git 전이 의존을 풀지 못하므로 소비 프로젝트 `manifest.json`에 직접 추가한다.

## 설치

### Git URL (권장)

**Window → Package Manager → + → Install package from git URL**

```text
https://github.com/monet5379/unity-framework-core.git?path=/Packages/com.monet5379.framework-core#v0.2.0
```

태그 push 전에는 `#main` 또는 커밋 SHA를 사용한다.

### 로컬 `file:` (개발)

소비 프로젝트 `Packages/manifest.json` 예:

```text
"com.monet5379.framework-core": "file:../../../../unity-framework-core/Packages/com.monet5379.framework-core"
```

경로는 소비 repo와 이 패키지 repo의 상대 위치에 맞게 조정한다. `file:` pin은 커밋하지 않는 것을 권장한다.

## asmdef (소비 프로젝트)

게임·프레임워크 코드가 **자체 asmdef** 안에 있으면 필요한 어셈블리를 참조한다.

| 참조 대상 | 언제 |
|-----------|------|
| `Monet5379.Core.Inspector` | Inspector attribute만 사용 |
| `Monet5379.Core.Utility` | Utility API (`XBehaviour` 등 — Inspector asmdef 포함) |
| `Monet5379.Core.Event` | `GlobalEventBus<TKey>` · `GlobalCallback` (게임 enum은 소비 repo) |
| `Monet5379.Core.Inspector.Editor` | 에디터 전용 확장 (보통 게임 Editor asmdef) |

## 불변조건

- 네임스페이스: 런타임 `Monet5379`, 에디터 `Monet5379.Inspectors` / `Monet5379.Inspectors.Internal`.
- **Utility는 장르 중립만** — 타이틀 데이터·Addressables 경로·게임 enum은 패키지에 두지 않는다. `Utility/Data/` 같은 게임 전용 트리는 금지.
- **Localization 전용 Inspector** (`StringTableKey*` 등)는 `unity-framework`에 남는다 — JSON 경로가 타이틀/베이스에 묶여 있다.
- **DOTween·TMP 결합 Utility** (`UITextEx` 등)는 베이스 프레임워크에 남는다.

상세 Architecture: repo `docs/architecture/` (Inspector · Utility · Event).
