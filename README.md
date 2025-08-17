# 🌊 요리조리 항해기

> **쉴 틈 없이 몰려오는 귀엽고 개성 넘치는 바다 몬스터들을 화려한 공격으로 물리치는 하이퍼캐주얼 로그라이크 슈팅 게임**

---

## 🎮 게임 소개

바다 깊은 곳에서 몰려오는 문어, 해파리, 상어들! 

계속해서 강해지는 무기와 스킬로 무한히 펼쳐지는 바다를 헤쳐나가세요.

### 🎯 핵심 특징
- **🔄 로그라이크 성장**: 매번 다른 스킬 조합으로 새로운 재미
- **💥 다양한 공격**: 기본탄부터 화염, 얼음, 하푼, 펫까지
- **🌟 피버타임**: 절정의 순간, 무적 상태로 적들을 휩쓸어버리세요
- **📱 모바일 최적화**: 직관적인 터치 조작

---

## 👥 개발팀

| 서승표 | 허시웅 | 최은주 | 
|:------:|:------:|:------:|
| <img src="https://avatars.githubusercontent.com/u/77848438?s=400&u=989cc11697a95a011b032da379b40e0ab7796e46&v=4" alt="서승표" width="150"> | <img src="https://avatars.githubusercontent.com/u/211462962?v=4" alt="허시웅" width="150"> | <img src="https://avatars.githubusercontent.com/u/211294462?v=4" alt="최은주" width="150"> |
| **프로젝트 리드 & 잡부 Developer** | ** QA & Level Design Developer** | ** Sound & UI/UX Developer** |
| 시스템 설계, 매니저 구현 | 기초 몬스터 AI, 레벨 시스템 | 인터페이스, 효과음, 픽셀 디자인 |
| [GitHub](https://github.com/Seo98) | [GitHub](https://github.com/HeoSiung) | [GitHub](https://github.com/Sooezfor) |

---

## 🛠️ 기술 스택

### 개발 환경
- **Unity** - 게임 엔진
- **C#** - 메인 프로그래밍 언어

### 협업 도구
- **Discord** - 실시간 소통
- **GitHub** - 버전 관리 및 협업
- **Notion** - 기획 및 문서화

---

## 📁 프로젝트 구조

```
Assets/@Scripts/Main_Scripts/
├── 🎯 Manager/           # 게임 핵심 매니저들
│   ├── UIManager         # 화면 전환, 게임 상태 관리  
│   ├── AttackManager     # 공격 시스템 통합 관리
│   ├── SoundManager      # BGM & 효과음
│   └── LevelUpManager    # 경험치 & 스킬 시스템
│
├── ⚔️ Player Attack/     # 플레이어 공격 시스템
│   ├── BaseAttack        # 공격 기본 클래스
│   ├── BasicAttack       # 기본 공격 (3연사)
│   ├── IceAttack         # 유도 얼음탄
│   ├── FlameAttack       # 화염 브레스
│   └── PetAttack         # 자동 공격 펫
│
├── 🐙 Monster/           # 몬스터 시스템
│   ├── Monster           # 몬스터 기본 클래스
│   ├── Jellyfish         # 해파리 (기본형)
│   ├── Octopus           # 문어 (원거리)
│   ├── redOctopus        # 문어 (데미지형 원거리)
│   ├── Shark             # 상어 (추적형)
│   ├── BigJellyMob       # 큰 해파리 (기본형/방어형)
│   └── Boss_R            # 보스 (5가지 패턴)
│
└── 🎮 Player/            # 플레이어 & UI
    ├── PlayerController  # 터치 조작, 체력 관리
    ├── BonusItem         # 드롭 아이템
    └── UI/               # 게임 인터페이스
```

---

## 🎯 게임 시스템

### ⚡ 공격 시스템
| 무기 | 특징 | 업그레이드 |
|------|------|------------|
| **기본탄** | 빠른 연사, 최대 3연발 | 데미지↑, 연사속도↑ |
| **얼음탄** | 적 추적, 슬로우 효과 | 데미지↑, 쿨타임↓ |
| **작살** | 관통형, 고데미지 | 데미지↑, 쿨타임↓ |
| **화염** | 지속 데미지, 범위 공격 | 지속시간↑, 데미지↑ |
| **펫** | 자동 공격, 적 추적 | 데미지↑ |

### 🌟 피버타임
- **충전**: 시간에 따라 자동 충전
- **효과**: 5초간 무적 + 이동속도 증가
- **보스전**: 공격속도 증가 !

### 📈 성장 시스템
- **경험치**: 몬스터 처치 시 획득
- **레벨업**: 3가지 스킬 중 선택
- **스테이지**: 스테이지 진입 후 90초뒤뒤 보스 등장

---

## 🎬 게임 스크린샷

<img width="439" height="880" alt="image" src="https://github.com/user-attachments/assets/a01e7edc-b81d-4d90-83f7-4a656b2cda8a" />
<img width="449" height="871" alt="image" src="https://github.com/user-attachments/assets/cdcba55d-1e74-43b8-913b-59b5909bcc0d" />
<br/>
<img width="453" height="882" alt="image" src="https://github.com/user-attachments/assets/35c0b592-7fcc-4007-9f1b-2941a2a32c3a" />
<img width="430" height="868" alt="image" src="https://github.com/user-attachments/assets/23045b01-11aa-436c-b1ab-7ad5a30340d0" />

---
### 💡 기술적 성과
- **깔끔한 상속 구조**로 확장성 확보

### 🎨 게임 디자인
- **직관적인 UI/UX**로 누구나 쉽게 플레이
- **점진적 난이도 상승**으로 지루하지 않은 게임플레이
- **다양한 몬스터 패턴**으로 전략적 재미 제공

---
<div align="center">

**🌊 바다의 모험이 시작됩니다! 🌊**

*Made with ❤️ by 멋쟁이사자처럼 게임개발 5기 / 하이퍼캐주얼 5팀 Team 토멋토*

</div>
