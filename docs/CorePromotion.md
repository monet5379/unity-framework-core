# 코어 승격 기준

베이스(`unity-framework`)나 타이틀 fork에 있는 코드를 `com.monet5379.framework-core`로 옮길지 정하는 기준과, 옮기거나 새로 만들 때의 절차를 정한다. 기본값은 **옮기지 않는 것**이다. 아래 세 조건을 모두 만족할 때만 코어로 올린다. 남은 코드를 전부 코어로 옮기는 것은 목표가 아니다.

## 대상

- 베이스 `unity-framework`의 `Assets/Project/Scripts/`
- 타이틀 fork의 Framework 코드 (예: mobile-idle `Assets/Framework/Scripts/`)
- 타이틀에서 새로 만들면서 다른 프로젝트에서도 쓸 것 같은 기능

## 승격 조건

세 가지를 **모두** 만족해야 한다.

1. **동기화 수요가 있다.** 두 프로젝트 이상에서 같은 코드를 손으로 맞춘 적이 있거나, 지금 만드는 기능을 다른 프로젝트가 곧 쓴다. 한 번도 동기화할 일이 없었던 코드는 fork에 있어도 비용이 없다.
2. **타이틀 소유 요소를 싸게 끊을 수 있다.** 타이틀 enum·에셋·싱글톤·서드파티 의존을 끊는 비용이 이중 수정 비용보다 작아야 한다.
3. **API가 안정적이다.** 시그니처가 자주 바뀌는 코드는 수정할 때마다 태그와 pin 갱신이 따라붙는다. 안정된 뒤에 올린다.

조건 2의 비용은 끊는 방식으로 가늠한다.

| 끊는 방식 | 비용 | 예 |
|-----------|------|----|
| 키 타입을 제네릭으로 | 작음 | `GlobalEventBus<TKey>` — 타이틀은 `GlobalEventType`과 facade만 소유 |
| provider 인터페이스 + 등록 시점 | 중간 | `ILocalizationSource` — 타이틀이 `BeforeSceneLoad`에 `LocalizationContext.Register` |
| 서드파티 추상화 + 어댑터 | 큼 | Audio — DOTween·LeanPool을 인터페이스 뒤로 |

## 코어에 두지 않는 것

- **타이틀이 확장하는 enum:** `GlobalEventType` · `GamePrefTypes` · `SoundNames` · `GameSceneNames`. 예외는 스튜디오 고정 목록인 `LanguageNames` 하나다.
- **타이틀 데이터·라벨 등록처:** `ScriptableDataManager` · `GameJsonData` · `StringData` · `AddressableLabels` · `GameLogTags`
- **부트·씬 골격:** `GameApp` · `GameBoot` · `SceneTransition` · `Game*Scene`. 씬 에셋·`ProjectSettings`·`AddressableAssetsData`도 UPM으로 배포하지 않는다.
- **`Assets/Plugins`·`ThirdParty`에 의존하는 코드:** DOTween(`UITextEx` · `UICanvasGroupFader`), LeanPool(`AudioObject`)
- **소비 repo 타입을 참조하는 코드:** 패키지 안에서 `GameLogTags`·`UITextEx` 같은 Framework 타입을 쓰지 않는다. 로그 태그는 패키지 자체 `[LogTagCatalog]`(`CoreEventLogTags` · `LocalizationLogTags`)에 둔다.

## 현재 판정

2026-11-03 기준 베이스에 남은 영역이다. 코드가 바뀌면 이 표보다 코드가 이긴다.

| 영역 | 판정 | 막는 것 |
|------|------|---------|
| `Prefs/` (`GamePrefs`) | 보류 — 수요가 생기면 1순위 | `GamePrefTypes` enum. 제네릭 키로 풀 수 있다 |
| `Audio/` | 보류 | `SoundNames` enum · DOTween · LeanPool |
| `UICanvasGroupFader` · `UITextEx` | 남김 | DOTween (공식 UPM 아님) |
| `Application/` · `Scene/` · `GameBoot` | 남김 | 타이틀마다 바뀌는 골격 |
| `Data/` · `Resource/` · `Setting/` · `Develop/` | 남김 | 타이틀 등록처. `GameLocalizationSource`처럼 코어 인터페이스를 구현하는 쪽 |
| `Global/` facade · `GlobalEventType` | 남김 | 로직은 이미 코어(`GlobalEventBus<TKey>`)에 있다 |

## 새 공통 기능을 만들 때

타이틀 작업 중에 공통 기능이 필요해지면 타이틀에 먼저 만들지 않고 코어에서 만든다.

1. 소비 프로젝트 `Packages/manifest.json`의 `com.monet5379.framework-core`를 `file:` 경로로 바꾼다 (패키지 [README](../Packages/com.monet5379.framework-core/README.md) §로컬 `file:`).
2. 멀티 루트에 이 repo를 추가하고 `Packages/com.monet5379.framework-core/`에서 구현한다. 소비 repo에서는 호출 코드와 provider 구현만 바꾼다.
3. Unity에서 소비 프로젝트를 열어 신규 파일의 `.meta`가 생성되는지와 컴파일 결과를 확인한다.
4. `package.json` version · `CHANGELOG.md` · 패키지 README를 갱신하고 커밋·태그를 push한다. 아래 §태그 전 점검을 먼저 본다.
5. 소비 프로젝트 manifest를 git URL `#v<version>`으로 되돌린다. `file:` pin은 커밋하지 않는다.
6. 다른 소비 프로젝트는 필요할 때 태그만 올린다.

## 기존 코드를 옮길 때

- **GUID를 보존한다.** Unity를 닫은 상태에서 `.cs`와 `.meta`를 함께 옮긴다. MonoBehaviour·ScriptableObject는 씬·에셋이 GUID로 참조하므로 필수다.
- **타이틀 사본을 정리한다.** fork 타이틀의 사본은 패키지와 GUID가 같다. 사본과 패키지를 diff해서 타이틀에서만 고친 내용이 있으면 패키지에 먼저 반영하고 태그를 올린다. 그다음 사본을 지우고 pin을 추가한다. 사본이 남은 채로 pin하고 Unity를 열면 GUID가 충돌해 참조가 깨질 수 있다.
- **asmdef references를 추가한다.** asmdef끼리는 `autoReferenced`가 적용되지 않는다. asmdef가 없는 코드(Assembly-CSharp)만 자동으로 참조한다.
- 구조·public API가 바뀌면 `docs/architecture/`와 소비 repo Architecture를 함께 갱신한다.

## 태그 전 점검

- [ ] 패키지 폴더에 untracked `.meta`가 없다 (`git status`). git 패키지는 읽기 전용이라 `.meta` 없는 파일을 Unity가 무시한다. `v0.1.1` · `v0.3.1`이 이 누락을 고친 태그다.
- [ ] 패키지 코드가 소비 repo 타입을 참조하지 않는다. `v0.3.2`가 `UITextEx` 참조를 고친 태그다.
- [ ] 새 git 의존이 생겼으면 README «함께 pin할 패키지»에 적었다. `package.json` `dependencies`에는 git URL을 넣을 수 없다.
- [ ] `package.json` version, 태그 이름, `CHANGELOG.md` 항목이 같다.
- [ ] 소비 프로젝트 pin을 고친 태그 이상으로 올렸다. 고장 난 태그에 pin을 남기지 않는다.

## 관련 문서

- [README.md](../README.md) · [AGENTS.md](../AGENTS.md)
- 패키지 [README](../Packages/com.monet5379.framework-core/README.md) — 설치 · 함께 pin할 패키지 · asmdef
- [docs/architecture/](architecture/README.md) — 코어 모듈별 현재 구조
- unity-framework [docs/studio/](https://github.com/monet5379/unity-framework/tree/main/docs/studio) — 베이스에 남는 골격
