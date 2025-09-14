# AiNavigator - Dokumentacja projektu

## Wprowadzenie

AiNavigator to aplikacja powstała w ramach projektu na zajęcia "Uniwersalne metody projektowania aplikacji na urządzenia mobilne i wbudowane". Projekt stanowi przykład współczesnego podejścia do tworzenia aplikacji wieloplatformowych z wykorzystaniem technologii .NET MAUI oraz API zewnętrznych do dostarczania aktualnych informacji o modelach AI.

## Cele i zadania aplikacji

Podstawowym celem aplikacji jest rozwiązanie problemu nieaktualnych danych na temat bieżących modeli językowych w ramach określonych kategorii. W dynamicznie rozwijającym się świecie sztucznej inteligencji, gdzie nowe modele AI pojawiają się regularnie, aplikacja zapewnia dostęp do najnowszych informacji poprzez wykorzystanie API ChatGPT do wyszukiwania i analizowania aktualnych danych.

Drugim ważnym założeniem projektu było stworzenie rozwiązania umożliwiającego uruchomienie aplikacji na różnych platformach - urządzeniu mobilnym, desktopie oraz w przeglądarce - z poziomu jednego wspólnego kodu źródłowego. To podejście demonstrate możliwości technologii .NET MAUI jako platformy do tworzenia aplikacji wieloplatformowych.

Trzecim kluczowym elementem jest praktyczne wykorzystanie rozwiązań OpenAI, konkretnie API ChatGPT, do wyszukiwania aktualnych informacji, ich mapowania oraz prezentacji użytkownikowi końcowemu w przystępnej formie.

## Architektura aplikacji

Aplikacja została zaprojektowana w architekturze klient-serwer, gdzie część serwerowa to Web API napisane w .NET, a część kliencka to aplikacja .NET MAUI komunikująca się z API poprzez HTTP.

### Warstwa serwerowa (API)

Serwer API odpowiada za komunikację z zewnętrznym API OpenAI oraz zarządzanie danymi aplikacji. Głównym komponentem jest klasa `ModelsService`, która obsługuje logikę biznesową związaną z pobieraniem informacji o modelach AI. Serwis wykorzystuje system cache'owania oparty na `IDistributedCache`, co pozwala na optymalizację wydajności poprzez przechowywanie wyników zapytań przez 24 godziny.

System bazodanowy oparty na Entity Framework Core przechowuje historię zapytań użytkowników. Każde zapytanie jest zapisywane wraz z podsumowaniem oraz szczegółami znalezionych modeli. Dane są grupowane według unikalnego identyfikatora zapytania, co umożliwia śledzenie kompletnych sesji wyszukiwania.

### Warstwa kliencka (MAUI)

Aplikacja kliencka została zbudowana z wykorzystaniem .NET MAUI i prezentuje nowoczesny, responsywny interfejs użytkownika. Design aplikacji charakteryzuje się wykorzystaniem gradientów, cieni oraz przemyślanej kolorystyki, tworząc atrakcyjne wizualnie doświadczenie użytkownika.

Główny ekran aplikacji umożliwia wybór kategorii modeli AI z predefiniowanej listy obejmującej generowanie tekstu, video, programowanie, grafiki oraz agentów AI. Po wyborze kategorii i uruchomieniu wyszukiwania, aplikacja prezentuje listę pięciu najlepszych modeli w danej kategorii wraz z ich szczegółową analizą.

## Funkcjonalności aplikacji

### Wyszukiwanie modeli AI

Główną funkcjonalnością aplikacji jest wyszukiwanie najlepszych modeli AI w wybranej kategorii. System wykorzystuje zaawansowany prompt wysyłany do API ChatGPT, który instruuje model aby przeanalizował dostępne źródła informacji i przedstawił ranking pięciu najlepszych rozwiązań. Odpowiedź jest strukturyzowana w formacie JSON i zawiera szczegółowe informacje o każdym modelu, w tym nazwę, opis, zalety, wady oraz ranking.

### Cache'owanie wyników

Aplikacja implementuje inteligentny system cache'owania, który przechowuje wyniki wyszukiwań przez 24 godziny. To rozwiązanie znacząco poprawia wydajność aplikacji oraz redukuje koszty związane z używaniem API OpenAI. Cache jest organizowany według kategorii, co pozwala na efektywne zarządzanie danymi.

### Historia wyszukiwań

System historii pozwala użytkownikom na przeglądanie poprzednich zapytań wraz z ich wynikami. Wszystkie zapytania są przechowywane w bazie danych z informacją o dacie wykonania, kategorii oraz szczegółach znalezionych modeli. Historia jest prezentowana w przejrzysty sposób, umożliwiając szybkie odnalezienie interesujących wyników.

### Szczegółowe informacje o modelach

Każdy znaleziony model AI jest prezentowany z kompletnym zestawem informacji obejmujących rangę w zestawieniu, szczegółowy opis, listę głównych zalet oraz potencjalnych wad. Dodatkowo dostępne są linki do oficjalnych stron modeli, co pozwala użytkownikom na dalsze poszerzenie wiedzy.

## Technologie i narzędzia

Projekt wykorzystuje szeroki stos technologiczny oparty na ekosystemie .NET. Warstwa serwerowa została zbudowana z wykorzystaniem ASP.NET Core Web API, co zapewnia wysoką wydajność oraz łatwość skalowania. Entity Framework Core służy jako warstwa dostępu do danych, umożliwiając efektywne zarządzanie bazą danych oraz automatyczne generowanie migracji.

System cache'owania został zaimplementowany z wykorzystaniem `IDistributedCache`, co daje możliwość łatwego przejścia na bardziej zaawansowane rozwiązania cache'ujące w przyszłości. AutoMapper jest używany do mapowania między różnymi reprezentacjami danych, co upraszcza konwersję między modelami domenowymi a DTO.

Aplikacja kliencka została stworzona w technologii .NET MAUI, która umożliwia tworzenie natywnych aplikacji dla różnych platform z jednego kodu źródłowego. XAML służy do definiowania interfejsu użytkownika z wykorzystaniem zaawansowanego systemu stylów oraz data binding.

## Sposób uruchomienia

Aby uruchomić projekt AiNavigator, konieczne jest posiadanie zainstalowanego .NET SDK w wersji obsługującej .NET MAUI oraz Visual Studio lub Visual Studio Code z odpowiednimi rozszerzeniami. Pierwszy krok to skonfigurowanie klucza API OpenAI w pliku konfiguracyjnym aplikacji serwerowej.

Po sklonowaniu repozytorium, należy najpierw uruchomić projekt API poprzez przejście do folderu zawierającego kod serwera i wykonanie polecenia dotnet run. Serwer domyślnie uruchamia się na porcie 7233 i udostępnia endpoint API pod adresem localhost.

Następnie należy uruchomić aplikację MAUI, która automatycznie podłączy się do lokalnie uruchomionego API. Aplikacja wykrywa platformę na której jest uruchamiana i odpowiednio konfiguruje adres bazowy dla komunikacji z serwerem.

## Bezpieczeństwo i konfiguracja

Aplikacja implementuje podstawowe mechanizmy bezpieczeństwa, w tym walidację certyfikatów SSL oraz obsługę błędów komunikacji HTTP. Klucz API OpenAI jest przechowywany w bezpieczny sposób w systemie konfiguracji .NET, co umożliwia łatwe zarządzanie różnymi środowiskami bez konieczności modyfikacji kodu.

System logowania błędów pomaga w diagnozowaniu problemów podczas komunikacji z zewnętrznymi API oraz obsługuje różne scenariusze awarii, zapewniając stabilne działanie aplikacji nawet w przypadku tymczasowych problemów z połączeniem.

## Podsumowanie

AiNavigator reprezentuje nowoczesne podejście do tworzenia aplikacji wieloplatformowych, łącząc zaawansowane technologie backendowe z atrakcyjnym interfejem użytkownika. Projekt demonstruje praktyczne wykorzystanie API sztucznej inteligencji w rzeczywistych scenariuszach oraz pokazuje możliwości technologii .NET MAUI w kontekście tworzenia aplikacji działających na różnych platformach z jednego kodu źródłowego.

Aplikacja może służyć jako punkt wyjścia do dalszego rozwoju lub jako przykład implementacji podobnych rozwiązań integrujących zewnętrzne API AI z aplikacjami mobilnymi i desktopowymi.
