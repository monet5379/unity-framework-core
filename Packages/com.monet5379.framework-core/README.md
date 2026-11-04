# Framework Core

스튜디오 게임 베이스·타이틀에서 공유하는 Inspector attribute, 장르 중립 Utility, 전역 이벤트 버스, 로컬 TMP UPM 패키지.

## 요구 사항

- Unity **6000.3** 이상 (`package.json`)
- 패키지 의존: `com.unity.ugui` (`ImageEx` 등 UI 확장)
- **함께 pin할 패키지:** UPM git 패키지는 git 전이 의존을 풀지 못하므로 소비 프로젝트 `manifest.json`에 직접 추가한다.
  - `com.monet5379.conditional-log` — `Monet5379.Core.Utility` · `Monet5379.Core.Event` · `Monet5379.Core.Localization`
  - `com.monet5379.tmp-font-pipeline` — `Monet5379.Core.Localization` (`FontRoleCatalog` · `FontUsageRole`)

## 설치

### Git URL (권장)

**Window → Package Manager → + → Install package from git URL**

```text
https://github.com/monet5379/unity-framework-core.git?path=/Packages/com.monet5379.framework-core#v0.3.4
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
| `Monet5379.Core.Localization` | `LanguageNames` · `UILocalizedText` · `LocalizationContext` |
| `Monet5379.Core.Inspector.Editor` | 에디터 전용 Inspector 확장 (보통 게임 Editor asmdef) |
| `Monet5379.Core.Localization.Editor` | `[StringTableKey]` drawer (JSON 키 catalog) |

로컬 TMP를 쓰는 타이틀은 Framework에서 `ILocalizationSource` 구현(예: `GameLocalizationSource`)을 **`BeforeSceneLoad`**에 `LocalizationContext.Register`한다. 계약은 [Architecture_Localization.md](../../docs/architecture/Architecture_Localization.md).

## StringTableKey JSON 경로 (Editor)

`StringTableKeyCatalog` 기본값:

- `Assets/Addressables/JSON/StringUI.json`
- `Assets/Addressables/JSON/StringDialogue.json`

AssetsLayout 관례와 맞춘다. 경로 설정 API는 제공하지 않는다.

## 불변조건

- 네임스페이스: 런타임 `Monet5379`, 에디터 `Monet5379.Inspectors` / `Monet5379.Inspectors.Internal`.
- **Utility는 장르 중립만** — 타이틀 JSON·Addressables 로더·게임 enum(`GlobalEventType`)은 패키지에 두지 않는다. **`LanguageNames`만** 스튜디오 고정 목록으로 패키지에 둔다.
- **DOTween·TMP 결합 Utility** (`UITextEx` 등)는 베이스 프레임워크에 남는다.

상세 Architecture: repo `docs/architecture/` (Inspector · Utility · Event · Localization).

| 문서 | 내용 |
|------|------|
| [Architecture_Inspector.md](../../docs/architecture/Architecture_Inspector.md) | attribute · catch-all Editor |
| [Architecture_Utility.md](../../docs/architecture/Architecture_Utility.md) | 장르 중립 Utility |
| [Architecture_Event.md](../../docs/architecture/Architecture_Event.md) | `GlobalEventBus<TKey>` · Reset 레지스트리 |
| [Architecture_Localization.md](../../docs/architecture/Architecture_Localization.md) | `ILocalizationSource` · `UILocalizedText` · binding |
