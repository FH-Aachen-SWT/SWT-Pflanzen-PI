# SWT-Pflanzen-PI 🌱

## Projektübersicht

**SWT-Pflanzen-PI** ist ein hochschulinternes Softwaretechnikprojekt eines fünfköpfigen MATSE-Entwicklerteams der FH Aachen. Ziel ist die Entwicklung eines interaktiven Prototyps, der reale Pflanzen mit digitaler Darstellung verknüpft, um Verantwortung, Nachhaltigkeit und technisches Verständnis spielerisch zu vermitteln.

Im Zentrum steht eine reale Pflanze, deren Zustand mittels Sensorik erfasst und in Form einer virtuellen, pixelartbasierten Figur auf einem Display dargestellt wird – inspiriert vom Prinzip klassischer Tamagotchis. Die digitale Pflanze reagiert emotional auf den Pflegezustand (z. B. glücklich, traurig, erschöpft) und schafft so eine unmittelbare Rückkopplung zwischen Handlung und Wirkung.

Der Projektantrag entstand in Kooperation mit dem **Beitragsservice NRW** und verfolgt einen pädagogisch geprägten Ansatz.

---

## Motivation

In einer zunehmend digitalisierten Lebenswelt verlieren insbesondere Kinder und Jugendliche häufig den Bezug zu natürlichen Prozessen. Gleichzeitig gewinnt digitale Bildung stetig an Bedeutung. Dieses Projekt setzt genau an dieser Schnittstelle an:

- Förderung von **Verantwortungsbewusstsein** und **Nachhaltigkeit**
- Verknüpfung von **Naturerfahrung** und **digitaler Technik**
- Niedrigschwelliger Zugang durch alltägliche Pflanzen
- Spielerisches Lernen durch emotionale Rückkopplung

Digitale Technologien werden dabei nicht als Gegensatz zur Natur verstanden, sondern als Mittel, um ökologische Zusammenhänge bewusster erfahrbar zu machen.

---

## Projektidee

Eine reale Pflanze wird mithilfe eines Feuchtigkeitssensors überwacht. Die erfassten Messdaten werden von einem Raspberry Pi verarbeitet und auf einem angeschlossenen Display visualisiert.

Die virtuelle Pflanze zeigt je nach Zustand unterschiedliche Darstellungen:

- 💧 **Optimale Feuchtigkeit** → vital, glücklich
- ⚠️ **Zu trocken** → traurig, schlapp
- 🌊 **Zu feucht** → erschöpft, gestresst

Diese visuelle Darstellung soll beim Nutzer eine emotionale Bindung erzeugen und Pflegehandlungen unmittelbar belohnen. Ergänzend sind langfristig spielerische Mechaniken (z. B. Streaks, Zustände über mehrere Tage) denkbar, die positive Routinen fördern.

---

## Technisches Konzept

### Hardware
- **Raspberry Pi 2**
- **Display:** 480 × 320 Pixel
- **Sensor:** Capacitive Soil Moisture Sensor v1.2
- **Betriebssystem:** Raspberry Pi OS

### Software
- **Programmiersprache:** C#
- **Framework:** .NET 8
- **Architektur:**  
  - Sensorzugriff über ein **Sensor-Interface**
  - Trennung von Datenerfassung, Logik und Darstellung
- **Anzeige:**  
  - Pixelart-Grafiken / GIFs
  - Assets werden manuell erstellt und lokal auf dem Raspberry Pi abgelegt
  - Anzeige über eine geeignete UI-Lösung (noch in Evaluation)

---

## Aktueller Projektstand

- Repository ist angelegt
- Grobe Projekt- und Architekturidee steht
- Hardware ist definiert
- Sensor-Modell ist ausgewählt
- Softwarestruktur (u. a. Sensor-Interface) ist geplant

⏳ **Noch offen / in Arbeit:**
- Flashen des Raspberry Pi
- Erste lauffähige C#-Anwendung auf dem Pi
- Anbindung des Feuchtigkeitssensors
- Anzeige der ersten statischen Pixelart-Zustände

---

## Geplante Erweiterungen

In späteren Projektphasen sind unter anderem vorgesehen:

- Weitere Sensoren:
  - Lichtintensität
  - Temperatur
  - Bodenparameter
- Erweiterte Zustandslogik (z. B. Langzeitpflege)
- Weitere Darstellungszustände und Animationen
- Optionale Audioausgabe („Mir ist zu trocken!“)
- Einsatz in schulischen oder außerschulischen Bildungskontexten

---

## Team

Das Projekt wird entwickelt von einem fünfköpfigen MATSE-Team der FH Aachen:

- Lukas  
- Sofia  
- Stefan  
- Lina  
- Julian  

*(gemeinsamer Nenner: Softwarekompetenz – kein grüner Daumen)*

---

## Hinweise

Dieses Projekt befindet sich in einer frühen Entwicklungsphase. Struktur, Architektur und Umfang können sich im Verlauf der Umsetzung weiterentwickeln.

---

## Lizenz

Noch nicht festgelegt.
