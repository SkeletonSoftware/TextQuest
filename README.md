# 🎮 Text Quest
Textová hra s umělou inteligencí v .NET 8!

## 🔧 Co potřebuješ
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Ollama](https://ollama.com/download/windows)
  - Návod níže
- Editor jako [VS Code](https://code.visualstudio.com/download)
  - Ideálně s extension [C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit)

## Jak ninstalovat Ollama - Llama3.2 ve Windows
1. Stáhnout a nainstalovat Ollama: https://ollama.com/download/windows
2. Potom je potřeba nainstalovat model llama3.2 - powershell: `ollama run llama3.2`
3. Po instalaci Llama3.2 můžete komunikovat s AI
4. Lze otestovat třeba pozdravem :)[run.sh](TextQuestDraft/run.sh)
5. Llama3.2 běží na portu: http://localhost:11434/api/generate

## ✨ Co se ve hře dá dělat
- Výběr postavy 
- Inventář a souboje
- AI vypravěč vytváří dobrodružství podle tvých vstupů
[run.sh](TextQuestDraft/run.sh)

## Known Issues
- Někdy se AI může "zaseknout" a neodpovídat, v tom případě restartuj hru.
- Některé příkazy nemusí fungovat správně, pokud AI nerozumí kontextu.
- Některé části hry mohou být nedokončené nebo obsahovat chyby, protože se jedná o experimentální projekt.

## Jak to funguje?
Hra využívá AI model Llama3.2 pro generování textu a interakci s hráčem. AI reaguje na příkazy a vytváří příběh na základě tvých rozhodnutí.
Část aplikace je řizena pomocí .NET 8, část pomocí AI API.

## Zadání
Vytvoř jednoduchou textovou hru, kde hráč může interagovat s AI vypravěčem. 
Hráč si vybere postavu a AI generuje příběh na základě jeho rozhodnutí. Hra by měla mít jednoduchý inventář a možnost soubojů.

## Cíl workshopu
- projít všechny // TODO: a doplnit je
- Jak se dělá konzolová aplikace v dot netu 
- Naučíte se používat DI kontejner a dependency injection
- Jednoduché provolávání API
- Základy práce s konfiguračními soubory
- Základy práce s AI modelem pomocí Ollama
- Co je to Ollama a jak funguje

## Co můžeme vylepšit?
- Přidat cíl - třeba porazit 10 nepřátel
- Přidat možnost útěku
- Ukládat si tahy a pak je načítat ze souboru - lze pak zřejmě využít pro AI jako historie
- Udělat napojení na OpenAI API pro lepší generování textu
- ... další nápady jsou vítány!

Enjoy!