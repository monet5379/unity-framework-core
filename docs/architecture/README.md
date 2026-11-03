# Architecture

`com.monet5379.framework-core` UPM의 **현재** 구조·경계·불변조건이다. 형식·골격은 Kit [Documentation.md](https://github.com/monet5379/unity-studio-kit/blob/main/docs/game/Documentation.md) Architecture 작성 규약을 따른다.

## 목차

| 문서 | 내용 |
|------|------|
| [Architecture_Inspector.md](Architecture_Inspector.md) | Odin-compat attribute · UI Toolkit catch-all · `[Button]` |
| [Architecture_Utility.md](Architecture_Utility.md) | 장르 중립 Utility · asmdef · 베이스에 남기는 예외(`UITextEx` · `SoundAssetEx`) |
| [Architecture_Event.md](Architecture_Event.md) | `GlobalEventBus<TKey>` · SubsystemRegistration Reset 레지스트리 |
| [Architecture_Localization.md](Architecture_Localization.md) | `LanguageNames` · `ILocalizationSource` · `UILocalizedText` · `[StringTableKey]` |

소비 베이스(`unity-framework`)의 asmdef·부트·로컬라이즈는 [unity-framework `docs/studio/architecture/`](https://github.com/monet5379/unity-framework/tree/main/docs/studio/architecture)를 본다.

## 관련 링크

- [README.md](../../README.md)
- [AGENTS.md](../../AGENTS.md)
- [Packages/com.monet5379.framework-core/README.md](../../Packages/com.monet5379.framework-core/README.md)
