# Architecture: Inspector

## 개요 (Overview)

*Odin 호환 인스펙터 attribute와 `[Button]`을 Runtime stub + Editor UI Toolkit으로 그리는 계약을 정리한다.*

`com.monet5379.framework-core` UPM은 Odin Inspector 없이 **필드 그룹·조건 표시·인스펙터 버튼**을 쓸 수 있게 한다. Runtime에는 attribute **선언만** 두고(`Monet5379.Core.Inspector`), 그리기·캐시·조건 평가는 **`Monet5379.Core.Inspector.Editor`**의 `Editor/Inspector/`가 담당한다. 전용 `CustomEditor`가 없는 `MonoBehaviour` · `ScriptableObject`는 기본 인스펙터가 이 파이프라인을 탄다. asmdef·Utility와의 관계는 [Architecture_Utility.md](Architecture_Utility.md) · 패키지 [README](../../Packages/com.monet5379.framework-core/README.md)를 본다.

## 책임과 경계 (Responsibilities & Boundaries)

*attribute stub과 Editor 그리기의 소유 범위를 구분한다.*

### 포함 범위 (In Scope)

- Runtime `Packages/com.monet5379.framework-core/Runtime/Inspector/` — `[Button]` · Foldout/조건/라벨용 Odin-compat attribute stub
- Editor `Packages/com.monet5379.framework-core/Editor/Inspector/` — UI Toolkit 기본 인스펙터 · `FrameworkInspectorBase` · `ButtonDrawer.Append`
- `FieldAttributeCache`가 읽는 필드 attribute와 `ButtonMethodCache`가 읽는 `[Button]` 메서드

### 제외 범위 (Out of Scope)

- Odin·NaughtyAttributes **본체** 및 전 attribute parity (ValueDropdown · MinMaxSlider 등 **stub만** 두고 drawer 미구현인 것은 아래 표 참고)
- 씬·프리팹 YAML 배선 · `[InitializeOnLoad]` Setup (소비 프로젝트 에디터 체크리스트)
- 타이틀 fork의 타입별 `CustomEditor` 구현 (패키지는 catch-all만 제공; fork에서 `FrameworkInspectorBase` 상속 또는 `ButtonDrawer`만 호출 가능)
- **Localization 전용** `[StringTableKey]` · catalog · drawer — JSON 경로가 베이스/타이틀에 묶여 있어 `unity-framework` 등 소비 repo에 둔다 ([Architecture_Localization](https://github.com/monet5379/unity-framework/blob/main/docs/studio/architecture/Architecture_Localization.md) 정본)

## 주요 타입과 관계 (Key Types & Relationships)

*Runtime stub · Editor 진입점 · Internal 구현의 경로만 적는다.*

```text
Runtime (Monet5379.Core.Inspector)
Packages/com.monet5379.framework-core/Runtime/Inspector/
├── ButtonAttribute.cs · ButtonSizes.cs
└── Compat/
    ├── ConditionAttributes.cs   ShowIf · EnableIf · DisableIf · ReadOnly · GUIColor
    ├── GroupAttributes.cs       FoldoutGroup · ShowIfGroup (stub)
    └── LabelAttributes.cs       Title · LabelText · SuffixLabel · HideLabel · InfoBox · InfoMessageType

Editor (Monet5379.Core.Inspector.Editor)
Packages/com.monet5379.framework-core/Editor/Inspector/
├── FrameworkInspectorBase.cs          CreateInspectorGUI → Internal
├── ButtonDrawer.cs                    public Append(root, editor)
├── Editors/
│   ├── MonoBehaviourInspector.cs      [CustomEditor(typeof(MonoBehaviour), true)]
│   └── ScriptableObjectInspector.cs   [CustomEditor(typeof(ScriptableObject), true)]
└── Internal/
    ├── InspectorLayoutBuilder.cs
    ├── FieldAttributeCache.cs
    ├── InspectorConditionEvaluator.cs
    ├── InspectorGuiColorUtility.cs
    └── ButtonMethodCache.cs
```

| 식별자 | 역할 |
|--------|------|
| `FrameworkInspectorBase` | UI Toolkit 인스펙터 베이스. `InspectorLayoutBuilder.Build` 후 root 반환 |
| `MonoBehaviourInspector` · `ScriptableObjectInspector` | 더 구체적인 `CustomEditor`가 없을 때 catch-all 적용 (`true` = derived types 포함) |
| `ButtonDrawer` | 대상 타입에 `[Button]`이 있으면 root 하단 Foldout에 버튼 추가. 다른 Editor에서 `Append` 재사용 |
| `InspectorLayoutBuilder` | `m_Script` 비활성 표시 → 최상위 `SerializedProperty` 순회 · attribute 반영 · 조건 attribute가 있는 중첩 클래스는 자식까지 전개 · `root.Bind` |
| `FieldAttributeCache` | 타입 계층의 instance 필드 ↔ property 이름별 attribute 메타 캐시 |
| `InspectorConditionEvaluator` | ShowIf / EnableIf / DisableIf 평가 (`@` 멤버 표현식 미지원) |
| `InspectorGuiColorUtility` | `GUIColorAttribute.GetColor` 인스턴스 메서드 → 필드 배경색 |
| `ButtonMethodCache` | 상속 계층의 `[Button]` 수집 · override는 파생 선언 하나 · `HideUnlessOverridden`는 최초 `virtual`이 아닌 `override`가 계층에 있을 때만 · Order · 코루틴/필수 파라미터 거부 |

**네임스페이스:** Runtime attribute는 **`Monet5379`**. Editor만 **`Monet5379.Inspectors`** · **`Monet5379.Inspectors.Internal`** (`internal` 구현).

### 필드 attribute — Editor 반영 여부

| Attribute | Runtime stub | Editor 그리기 |
|-----------|--------------|---------------|
| `FoldoutGroup` | O | O — 중첩 경로 `/` · `Order` · `Expanded` |
| `Title` | O | O |
| `LabelText` · `SuffixLabel` | O | O |
| `InfoBox` | O | O — `VisibleIf` / `VisibleIfMember` · 메시지 `@`/`$` 멤버 참조 |
| `ShowIf` · `EnableIf` · `DisableIf` | O | O — 다중 attribute는 AND |
| `ReadOnly` | O | O — 필드 비활성 |
| `GUIColor` | O | O — `Color` 반환 인스턴스 메서드 |
| `HideLabel` | O | **미연동** (캐시 없음) |
| `ShowIfGroup` | O | **미연동** (그룹 조건 전용 drawer 없음) |

`Compat/ValueAttributes.cs` 계열(MinValue · ValueDropdown 등)은 **패키지 Runtime에 없다**. fork에서 stub·drawer를 추가할 때 이 표를 갱신한다.

### `[Button]` 제약

- 반환 `IEnumerator`(코루틴) 메서드는 버튼 비활성 + 툴팁 사유
- 파라미터가 있으면 **모든 파라미터에 기본값**이 있어야 호출 가능
- static/instance 모두 지원. instance 호출 시 `Undo.RecordObjects` · `SetDirty` · 프리팹 instance modification 기록
- `HideUnlessOverridden = true`이면 상속 계층에서 그 가상 메서드를 `override`한 선언이 있을 때 버튼을 그린다. 최초 `virtual` 선언만 있으면 숨긴다. 중간 타입이 override하면 그 타입과, override하지 않은 하위 타입 인스펙터에도 보인다. `XBehaviour.AutoGetComponents` · `AutoAddComponents` · `AutoSetting` · `AutoNaming`이 이 플래그를 쓴다

## 흐름 (Flow)

*에디터에서 인스펙터가 그려지는 순서이다.*

### Editor 흐름 (Editor Flow)

```text
1. Unity가 대상 타입용 CustomEditor 선택
   → 전용 editor 없음 → MonoBehaviourInspector / ScriptableObjectInspector
2. FrameworkInspectorBase.CreateInspectorGUI
3. InspectorLayoutBuilder.Build
   a. m_Script (비활성)
   b. 최상위 property 수집 · FieldAttributeCache · SortOrder/선언 순 정렬
   c. FoldoutGroup 경로별 Foldout · Title/InfoBox · PropertyField
      (중첩 [Serializable] 타입에 조건 attribute가 있으면 Foldout + 자식별 c·d 재귀)
   d. ShowIf/EnableIf/DisableIf/ReadOnly 바인딩 (TrackSerializedObjectValue)
   e. ButtonDrawer.Append — [Button] Foldout
4. serializedObject.ApplyModifiedProperties는 Unity 기본 PropertyField 경로
```

전용 `CustomEditor`가 있으면 catch-all은 **적용되지 않는다**. attribute만 쓰려면 해당 Editor가 `FrameworkInspectorBase`를 상속하거나, IMGUI/UI Toolkit GUI 끝에 `ButtonDrawer.Append`를 호출한다.

## 불변조건과 주의점 (Invariants & Gotchas)

*Inspector 계약을 깨면 필드가 안 보이거나 버튼이 잘못 호출된다.*

- **참조 방향:** `Monet5379.Core.Inspector.Editor` → `Monet5379.Core.Inspector`만. Runtime asmdef는 Editor를 references에 넣지 않는다. 게임 Runtime asmdef는 Core.Inspector를 참조할 수 있으나 Core.Editor를 참조하지 않는다.
- **Runtime attribute:** stub은 **런타임 동작 없음**. `[Button]`은 Editor에서만 invoke된다.
- **Catch-all 범위:** `[CustomEditor(typeof(MonoBehaviour), true)]`는 프로젝트 전 MonoBehaviour에 영향한다. Unity 기본 인스펙터 대신 UI Toolkit 파이프라인이 적용된다.
- **조건식:** `InspectorConditionEvaluator`는 멤버 이름 문자열만 본다. `@` 로 시작하는 Odin 스타일 표현식은 **평가하지 않고** ShowIf 쪽은 visible true로 취급한다.
- **중첩 클래스:** `[Serializable]` 필드 타입 안에 `ShowIf`·`EnableIf`·`DisableIf`·`ReadOnly`가 하나라도 있을 때만 전개한다. 이때 조건 멤버는 **같은 중첩 객체의 형제 serialized 필드**로 찾는다(프로퍼티·대상 객체 멤버로 fallback 없음). 배열·List 원소와 `FoldoutGroup`은 전개 대상이 아니다. `InfoBox` VisibleIf·메시지 멤버와 `GUIColor`는 최상위 대상 객체 기준이라 중첩 필드에 쓰지 않는다.
- **InfoBox 메시지:** `@member` · `$member` 접두는 **메시지 문자열** 치환용(필드/프로퍼티 `ToString()`).
- **Foldout 상태:** `viewDataKey` 접두 `Monet5379.InspectorLayout.Foldout.` · 버튼 Foldout `Monet5379.ButtonDrawer.Foldout` — 에디터 세션 간 접기 상태 유지.
- **ReadOnly 이름 충돌:** `Monet5379.ReadOnlyAttribute` stub을 쓴다. `UnityEngine.ReadOnlyAttribute`와 같은 파일에서 혼용하지 않는다.
- **소비 repo 중복:** 타이틀 fork에 Inspector 사본(GUID 동일)을 패키지와 **동시에** 두면 타입 중복·catch-all 이중 등록이 난다. pin 전에 사본을 제거한다.

## 변경 가이드 (Change Guidelines)

*attribute·drawer·catch-all을 수정할 때 확인할 경로이다.*

| 변경 | 확인 |
|------|------|
| Runtime attribute 추가·시그니처 | `Runtime/Inspector/` · 이 문서 «필드 attribute» 표 |
| 필드 그리기·정렬·Foldout | `Editor/Inspector/Internal/InspectorLayoutBuilder.cs` · `FieldAttributeCache.cs` |
| ShowIf/EnableIf/DisableIf 규칙 | `Editor/Inspector/Internal/InspectorConditionEvaluator.cs` |
| `[Button]` 수집·호출·Undo | `Editor/Inspector/Internal/ButtonMethodCache.cs` · `ButtonDrawer.cs` |
| 새 catch-all 또는 타입 전용 editor | `Editor/Inspector/Editors/` · `[CustomEditor]` 우선순위(Unity 규칙) |
| 공개 재사용 API | `FrameworkInspectorBase.cs` · `ButtonDrawer.Append`만 노출 유지 |
| Internal 배치 | 구현은 `Internal/` · namespace `Monet5379.Inspectors.Internal` |
| 버전 배포 | 태그 · 소비 `manifest.json` git URL · [CHANGELOG](../../Packages/com.monet5379.framework-core/CHANGELOG.md) |

구조·지원 attribute 목록이 바뀌면 이 파일 해당 절만 갱신한다. Utility·`XBehaviour`와 attribute 결합은 [Architecture_Utility.md](Architecture_Utility.md)를 본다.

## 관련 문서 (See also)

- [Architecture_Utility.md](Architecture_Utility.md) — Utility asmdef · `XBehaviour` · Inspector 참조
- [README.md](../../README.md) · [Packages/com.monet5379.framework-core/README.md](../../Packages/com.monet5379.framework-core/README.md) — pin · 함께 설치할 패키지
- [docs/architecture/README.md](README.md) — 목차
- [AGENTS.md](../../AGENTS.md) — 에이전트 범위 · `.meta` 금지
