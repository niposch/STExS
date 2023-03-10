# Benutzerhandbuch für STExS

"Ist das ein Bug? Ist das ein Feature? Nein, das ist STExS" ~ Nick, Software Architekt

---

## Nutzertypen
- Nutzer sind unterteilt in 3 Rollen: _Student_, _Lehrer_ und _Admin_
- Beim Anmelden wird dem Nutzer automatisch die Rolle _Student_ zugewiesen
- Alle Rollen haben Zugriff auf die Profilseite und können Module (die Sie nicht selber verwalten) mit den darin enthaltenen Übungen bearbeiten.


- _Lehrer_ und _Admins_ können im Gegensatz zu _Studenten_ Module (auch Kapitel und Übungen) erstellen und bearbeiten.
- _Studenten_ können lediglich Modulen beitreten und Übungen lösen.
- _Admins_ haben die Möglichkeit die Rollen von Nutzern zu ändern.

## Genereller Aufbau von Lerninhalten
- Lerninhalte sind mittels Modulen (modules), Kapiteln (chapters) und Übungen (exercises) strukturiert.
- Module, Kapitel und Übungen haben jeweils einen Titel und eine Beschreibung.
- Übungen sind von einem Typ (Code Output, Parson Puzzle, Cloze Text (Lückentext))

### Module
- können von Admins und Lehrern erstellt werden (und bearbeitet werden, sofern Nutzer der Eigentümer des Moduls ist).
- haben zusätzlich eine maximale Teilnehmeranzahl, die der Eigentümer festlegen kann.
- ein Modul enthält beliebig viele Kapitel (können im Modul erstellt werden)
- Wenn ein Student Übungen lösen möchte, muss er / sie in das jeweilige Modul beigetreten sein.
- das Beitreten muss vom Modul-Eigentümer genehmigt werden.
- ein Modul kann Archiviert sein, dann können Nutzer nicht mehr beitreten.

### Kapitel 
- sind einem Modul zugeordnet
- können auf der Modul-Bearbeiten-Seite erstellt werden
- beinhalten beliebig viele Übungen von beliebigen Typen (können im Kapitel erstellt werden)

### Übungen
- eine Übung kann einem / mehreren Kapiteln zugeordnet sein (Übung wird für jedes Kapitel kopiert)
- können auf der Kapitel-Bearbeiten-Seite erstellt werden
- eine Übung ist "öffentlich" zugänglich und kann von jedem Lehrer / Admin in seinen Modulen (bzw. Kapiteln) verwendet werden.
- jede Übung hat einen Titel, eine Beschreibung und eine maximal erreichbare Punktzahl.
- eine Übung ist von genau einem Typ (muss beim Erstellen festgelegt werden)
  - Code Output: Beschreibung für die Frage, Expected Answer für erwartete (korrekte) Antwort
  - Parson Puzzle: Beschreibung für die Frage, Codezeilen und deren korrekte Reihenfolge (per Drag & Drop)
  - Cloze Text: Beschreibung für die Frage, Lückentext mit korrekten Wörtern (Lücken mittels [[]] gekennzeichnet)

---

## Funktionen
- Admins und Lehrer können alles, was Studenten auch können (aber können nicht ihre eigenen Module lösen)

### Funktionen für alle Rollen
- Login (mittels E-Mail und Passwort) auf der _Login_ Seite
- Registrieren auf der _Registrieren_ Seite (E-Mail, Passwort, Vorname, Nachname, Matrikelnummer, Nutzername, Geschlecht (optional))
- Profilseite mit sämtlichen Nutzerdaten (hier auch Nutzerdaten änderbar)
- Sidebar: Übersicht über alle beigetretenen Module (und Admins / Lehrer auch Module, die ihnen gehören)
- Dashboard: Übersicht über beigetretene Module + Liste aller Module
- Modul-Detailseite: Übersicht über ein Modul (Titel, Beschreibung, Kapitel, Teilnehmeranzahl, Eigentümer, Erstellungsdatum)
- Kapitel-Lösen: Liste aller Übungen im Kapitel, Anzeigen der aktuellen Übung mit Lösungsmöglichkeit (je nach Übungstyp)
  - mit automatischen Zwischenspeichern und manueller finaler Abgabe
  - nach finaler Abgabe wird die erreichte Punktzahl angezeigt (zunächst die automatisch vergebene Punktzahl, nach manuellem Grading auch die manuell vergebene Punktzahl)
- E-Mail verifizieren: Nach dem Login sollte der Nutzer eine E-Mail erhalten (ist implementiert, allerdings nicht einfach ohne Mailserver umsetzbar, daher deaktiviert), die ihn auffordert seine E-Mail zu verifizieren.

### Funktionen explizit für Lehrer und Admins
- Erstellen und Verwalten von Modulen, Kapiteln und Übungen
- Erstellen, Bearbeiten, Archivieren, Löschen von Modulen über die Modul-Seite
- Einsehen von Daten für ein Modul, manuelle Bewertung von Abgaben über die Data-Dashboard / Grading-Seite (erreichbar über Modul-Seite / Modul-Bearbeiten-Seite)
- Akzeptieren / Ablehnen von Beitrittsanfragen von Studenten über die Modul-Seite
- Erstellen, Bearbeiten, Löschen neuer Kapitel über die Modul-Bearbeiten-Seite
- Erstellen, Bearbeiten, Löschen und Hinzufügen von bereits existierenden Übungen zu einem Kapitel über die Kapitel-Bearbeiten-Seite
- Einsehen von Abgaben, Bearbeitungszeiten von einer Aufgabe und vergabe von Punkten (Grading-Seite)
  - Historie von Abgaben mit Zeitstempel und temporären / finaler Lösung für eine Aufgabe
  - Möglichkeit zur Bearbeitung der (automatisch vergebenen) Punktzahl für eine Aufgabe
  - Automatisch vergebene Punktzahlen werden für die verschiedenen Übungenstypen unterschiedlich berechnet
    - Code Output vergleicht die erwartete Antwort mit der tatsächlichen Antwort (volle Punktzahl oder nicht)
    - Parson Puzzle vergleicht die korrekte Reihenfolge und Einrückung mit der tatsächlichen Reihenfolge und Einrückung (allerdings kann eine Verschiebung einer Zeile 0 Punkte zur Folge haben, da alle darauf folgenden Zeilen in der falschen Zeile stehen)
    - Cloze Text vergleicht die korrekten Antworten mit den eingegebenen Antworten (pro korrekter Lücke einen Punkt)

### Funktionen explizit für Admins
- User-Verwaltung: Liste aller Nutzer mit Rollen und Möglichkeit diese zu ändern, Anzeige ob E-Mail verifiziert