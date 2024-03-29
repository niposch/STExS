# Benutzerhandbuch für STExS

"Ist das ein Bug? Ist das ein Feature? Nein, das ist STExS" ~ Nick, Software Architekt

---

- Testnutzeraccounts:

| email             | passwort | rolle |
| ----------------- | -------- | ----- |
| dev@test.com      | Test333! | Admin |
| dev-teacher@test.com | Test333! | Lehrer      |
| dev-user@test.com | Test333! | Student      |
## Nutzertypen
- Nutzer sind unterteilt in 3 Rollen: _Student_, _Lehrer_ und _Admin_
- Beim Registrieren wird dem Nutzer automatisch die Rolle _Student_ zugewiesen
- Alle Rollen haben Zugriff auf die Profilseite und können Module (die Sie nicht selber verwalten) mit den darin enthaltenen Übungen bearbeiten.


- _Lehrer_ und _Admins_ können im Gegensatz zu _Studenten_ Module (auch Kapitel und Übungen) erstellen und bearbeiten.
- _Studenten_ können lediglich Modulen beitreten und Übungen lösen.
- _Admins_ haben die Möglichkeit die Rollen von Nutzern zu ändern.

## Genereller Aufbau von Lerninhalten
- Lerninhalte sind mittels Modulen (modules), Kapiteln (chapters) und Übungen (exercises) strukturiert.
- Module, Kapitel und Übungen haben jeweils einen Titel und eine Beschreibung (welche auch Styling und sogar Bilder unterstützt).
- Übungen sind von einem Typ (Code Output, Parson Puzzle, Cloze Text (Lückentext))

### Module
- können von Admins und Lehrern erstellt werden (und bearbeitet werden, sofern Nutzer der Eigentümer des Moduls ist).
- haben zusätzlich eine maximale Teilnehmeranzahl, die der Eigentümer festlegen kann.
- ein Modul enthält beliebig viele Kapitel (können im Modul erstellt werden)
- Wenn ein Student Übungen lösen möchte, muss er / sie in dem jeweilige Modul beigetreten sein.
- das Beitreten muss vom Modul-Eigentümer genehmigt werden.
- ein Modul kann Archiviert sein, dann können Nutzer nicht mehr beitreten.

### Kapitel 
- sind einem Modul zugeordnet
- können auf der Modul-Bearbeiten-Seite erstellt werden
- beinhalten beliebig viele Übungen von beliebigen Typen (können im Kapitel erstellt werden)

### Übungen
- eine Übung kann einem / mehreren Kapiteln zugeordnet sein
- können auf der Kapitel-Bearbeiten-Seite erstellt werden oder aus bereits existierenden Übungen kopiert werden (siehe Kapitel Bearbeiten Seite)
- jede Übung hat einen Titel, eine Beschreibung und eine maximal erreichbare Punktzahl.
- eine Übung ist von genau einem Typ (muss beim Erstellen festgelegt werden)
  - Code Output: Beschreibung für die Frage, Expected Answer für erwartete (korrekte) Antwort
  - Parson Puzzle: Beschreibung für die Frage, Codezeilen und deren korrekte Reihenfolge (per Drag & Drop)
  - Cloze Text: Beschreibung für die Frage, Lückentext mit korrekten Wörtern (Lücken mittels \[\[\]\] gekennzeichnet)

---

## Layout
- 2 verschiedene Layouts: _Header only_ (nur bei Startseite, Login und Registration) und _Sidebar_ (sonst überall)
- Layout mit Sidebar
- ![img.png](images/sidebar_layout.png)
- Header Only Layout
- ![img_1.png](images/header_only.png)

## Funktionen
- Admins und Lehrer können alles, was Studenten auch können (aber können nicht ihre eigenen Module lösen)
- siehe Abschnitt _Funktionen explizit für Lehrer und Admins_ für alle Funktionen, die nur für Lehrer und Admins verfügbar sind
- siehe Abschnitt _Funktionen explizit für Admins_ für alle Funktionen, die nur für Admins verfügbar sind

### Funktionen für alle Rollen
- Startseite betrachten 
- ![img.png](images/img.png)
- Login (mittels E-Mail und Passwort) auf der _Login_ Seite
- ![img.png](images/login.png)
- Registrieren auf der _Registrieren_ Seite (E-Mail, Passwort, Vorname, Nachname, Matrikelnummer, Nutzername, Geschlecht (optional))
  - Email muss bestätigt werden, dafür ist in der appsettings.json ein SMTP Server konfiguriert (standardkonfig versendet die Bestätigungsemails nicht, sondern legt sie in ein Testpostfach ab => [Entwicklerhandbuch](Entwicklerhandbuch.md#konfiguration))
- ![img.png](images/registration.png)
- Profilseite mit sämtlichen Nutzerdaten (hier auch änderbar)
- ![img.png](images/profile.png)
- Sidebar: Übersicht über alle beigetretenen Module (und Admins / Lehrer auch Module, die ihnen gehören)
- ![img.png](images/sidebar.png)
- Dashboard: Übersicht über beigetretene Module + Liste aller Module
- ![img.png](images/dashboard.png)
- Modul-Detailseite: Übersicht über ein Modul (Titel, Beschreibung, Kapitel, Teilnehmeranzahl, Eigentümer, Erstellungsdatum)
- ![img.png](images/module-details.png)
- Kapitel-Lösen: Liste aller Übungen im Kapitel, Anzeigen der aktuellen Übung mit Lösungsmöglichkeit (je nach Übungstyp)
  - mit automatischen Zwischenspeichern und manueller finaler Abgabe
  - nach finaler Abgabe wird die erreichte Punktzahl angezeigt (zunächst die automatisch vergebene Punktzahl, nach manuellem Grading auch die manuell vergebene Punktzahl)
- ![img.png](images/loesen-kapitel.png)
- E-Mail verifizieren: Nach der Registrierung sollte der Nutzer eine E-Mail erhalten (ist implementiert, allerdings nicht einfach ohne Mailserver umsetzbar, daher ein Testsmpt Server angebunden, Emails können abgerufen => [Entwicklerhandbuch](Entwicklerhandbuch.md#konfiguration)), die ihn auffordert seine E-Mail zu verifizieren.



### Funktionen explizit für Lehrer und Admins
- Erstellen und Verwalten von Modulen, Kapiteln und Übungen
- Erstellen, Bearbeiten, Archivieren, Löschen von Modulen über die Modul-Seite
- - Akzeptieren / Ablehnen von Beitrittsanfragen von Studenten über die Modul-Seite
- ![img.png](images/module-page.png)
- Einsehen von Daten für ein Modul, manuelle Bewertung von Abgaben über die Data-Dashboard / Grading-Seite (erreichbar über Modul-Seite / Modul-Bearbeiten-Seite)
- ![img.png](images/data-dashboard.png)
- Erstellen, Bearbeiten, Löschen neuer Kapitel über die Modul-Bearbeiten-Seite
- ![img.png](images/edit-module.png)
- Erstellen, Bearbeiten, Löschen und Hinzufügen von bereits existierenden Übungen zu einem Kapitel über die Kapitel-Bearbeiten-Seite
- ![img.png](images/edit-chapter.png)
- Einsehen von Abgaben, Bearbeitungszeiten von einer Aufgabe und vergabe von Punkten (Grading-Seite)
  - Historie von Abgaben mit Zeitstempel und temporären / finaler Lösung für eine Aufgabe
  - Möglichkeit zur Bearbeitung der (automatisch vergebenen) Punktzahl für eine Aufgabe
  - Automatisch vergebene Punktzahlen werden für die verschiedenen Übungenstypen unterschiedlich berechnet
    - Code Output vergleicht die erwartete Antwort mit der tatsächlichen Antwort (volle Punktzahl oder nicht)
    - Parson Puzzle vergleicht die korrekte Reihenfolge und Einrückung mit der tatsächlichen Reihenfolge und Einrückung (allerdings kann eine Verschiebung einer Zeile 0 Punkte zur Folge haben, da alle darauf folgenden Zeilen in der falschen Zeile stehen)
    - Cloze Text vergleicht die korrekten Antworten mit den eingegebenen Antworten (pro korrekter Lücke einen Punkt)
- ![img.png](images/lückentext.png)
- ![img.png](images/solutions.png)
- ![img.png](images/history.png)

### Funktionen explizit für Admins
- User-Verwaltung: Liste aller Nutzer mit Rollen und Möglichkeit diese zu ändern, Anzeige ob E-Mail verifiziert
- ![img.png](images/users.png)