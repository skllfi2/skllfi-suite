---
name: gost-spec-architect
description: Authoring formal engineering specifications, Russian state standard technical requirements (ГОСТ 34.602, ГОСТ 19.201, ISO/IEC/IEEE 29148), multi-echelon cybersecurity defense architectures, and formal software verification plans. Use PROACTIVELY when developing official technical specifications (ТЗ), security architectures, or regulatory compliance artifacts.
metadata:
  model: inherit
---

# Formal Technical Specification & ГОСТ Architecture

Professional standard for authoring exhaustive, audit-ready engineering documents and cybersecurity system specifications.

## When to use

- Creating formal Technical Specifications (Техническое задание, ТЗ) complying with ГОСТ 34.602-2020 and ГОСТ 19.201-78.
- Designing multi-echelon defense platforms (RASP, Anti-Tamper, Code Obfuscation, Entanglement).
- Calculating quantitative resilience metrics (e.g., CTBR - Cumulative Time to Breach, MTTA, MTTD).
- Structuring formal traceability matrices between business goals, attack vectors, and defensive countermeasures.

---

## 1. Document Structure & ГОСТ 34.602 Compliance

A compliant system specification must strictly include:
1. **Общие сведения:** Название, шифр документа, заказчик, разработчик, основание и плановые сроки.
2. **Назначение и цели создания системы:** Измеримые целевые критерии и автоматизируемые процессы.
3. **Требования к системе:**
   - Общесистемные требования (надежность, масштабируемость, безопасность).
   - Требования к функциям (подсистемы, сервисы, компоненты).
   - Требования к видам обеспечения (информационное, программное, техническое).
4. **Состав и содержание работ по созданию системы:** Этапы ОКР/НИОКР, вехи, приемка.
5. **Порядок контроля и приемки:** Виды испытаний (предварительные, опытная эксплуатация, приемочные).

---

## 2. Multi-Echelon Defense & Security Modeling

When specifying anti-tamper and cybersecurity platforms:
- **Depth of Defense:** Document protections across physical, hypervisor, kernel, runtime, and cognitive layers.
- **Cross-Layer Entanglement:** Specify verification mechanisms where higher-level runtime invariants depend cryptographically on lower-level state assertions.
- **Quantitative Metrics:** Define measurable security benchmarks:
  $$\text{CTBR} = \sum_{i=1}^{N} T_{\text{bypass}}(E_i)$$
  where $E_i$ represents the $i$-th defensive echelon.
