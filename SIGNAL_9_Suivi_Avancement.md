# SIGNAL 9 — Suivi d'avancement

> Ce document accompagne `SIGNAL_9_GDD_Initial.md` (document de conception) et sert de journal de bord technique. Il liste ce qui est fait, ce qui est en cours, et les prochaines étapes.

Dernière mise à jour : voir historique Git.

---

## Décisions techniques fixées

- **Moteur** : Unity, template **Universal 2D** (URP).
- **Nom du projet** : `Signal9`.
- **Identité visuelle** : palette quasi monochrome (gris anthracite/noir #1b1c1e / #141516), un seul accent bleu-gris terne (#4a7fb5) pour les éléments actifs. Barre latérale d'applications (pas d'icônes éparpillées sur le bureau). Typographie petite et dense, labels en majuscules discrètes façon logiciel professionnel. Aucune couleur saturée, aucun effet néon — cohérent avec la Section 32 du GDD (sobre, fonctionnel, légèrement vieillissant, éviter le cyberpunk). Maquette de référence validée le [date de cette session].
- **Doublage** : voix complètes pour les personnages et communications (voir Section 33.1 du GDD).
- **Fin du jeu** : ambiguïté volontaire et définitive, y compris en interne (voir Section 21 du GDD).
- **Chronologie d'ARCHIVE** : origine années 1990, dérive progressive de l'objectif, lien avec l'accident de 2009 volontairement laissé incertain (voir Section 13.1 du GDD).
- **Versioning** : Git + GitHub (`Nath4an-s/Signal9`), pas de VCS intégré Unity (mode "Visible Meta Files").

---

## Ce qui a été fait

### Mise en place du projet
- [x] Création du projet Unity (`Signal9`, template Universal 2D).
- [x] Initialisation Git, `.gitignore` Unity en place.
- [x] Dépôt GitHub créé et connecté (`origin`), premier push effectué.
- [x] Nettoyage du VCS intégré Unity (Plastic SCM désactivé, mode "Visible Meta Files").

### Structure du projet
- [x] Arborescence de dossiers créée dans `Assets` : `_Scripts` (avec sous-dossier `Managers`), `_UI`, `_Data`, `_Prefabs`, `_Sprites`, `_Audio`, `_Scenes`.
- [x] Scène par défaut renommée en `Desktop` et déplacée dans `_Scenes`.

### Premier écran (bureau)
- [x] Canvas principal créé (Screen Space - Overlay, Canvas Scaler en "Scale With Screen Size", résolution de référence 1920x1080).
- [x] Panel `Background` en plein écran, couleur définitive `#1B1C1E`.
- [x] `GameManager.cs` créé dans `_Scripts/Managers`, attaché à un GameObject vide dans la scène `Desktop` (squelette vide pour l'instant, sert de point d'ancrage).
- [x] Test en Play : l'écran de fond s'affiche correctement.
- [x] Panel `Sidebar` (180px, fond `#141516`, bordure droite `#2A2B2D`, anchor left-stretch).
- [x] Label `Label_Applications` (TextMeshPro).
- [x] 7 boutons d'application dans Sidebar (Dossiers, Base de données, Analyse photo, Analyse vidéo, Analyse audio, Téléphone, Graphe) via Vertical Layout Group.
- [x] Style actif appliqué sur `Btn_Dossiers` (fond `#242527`, bordure gauche bleue `#4A7FB5`, texte clair).
- [x] Panel `MainContent` (reste de l'espace, fond `#1B1C1E`).
- [x] Panel `CaseWindow` avec `TitleBar` ("CASE #0017 — Disparition").
- [x] `ContentArea` avec `ColumnLeft` (Location/Status/Agent) et `ColumnRight` (Attached Files) via Horizontal + Vertical Layout Groups.
- [x] Débordement vertical de ColumnLeft/ColumnRight corrigé (Control Child Size + Child Force Expand sur les Layout Groups).

### Peaufinage UI
- [x] 7 icônes téléchargées (Tabler/Feather), renommées et importées dans `_Sprites`.
- [x] Icônes assignées sur les 7 boutons de la Sidebar (Image + Horizontal Layout Group par bouton).
- [x] États hover/pressed configurés sur les 7 boutons (Button > Transition > Color Tint).
- [x] Bouton de fermeture (×) sur la TitleBar de CaseWindow.
- [x] Test responsive (plusieurs résolutions en Play).
- [ ] État actif dynamique des boutons Sidebar — reporté volontairement à l'étape du système de navigation (après CaseManager).

---

## En cours / prochaine étape immédiate

- [ ] Mettre à jour `case_0017.json` avec le vrai contenu narratif ("La maison vide" — cf. `SIGNAL_9_Affaire_MaisonVide.md`) : `title`, `attachedFiles` (ajout de `SALON_PHOTO.jpg`, retrait de `CCTV_01.mp4`), `evidence`, `requiredDiscoveries`.
- [ ] Produire les assets manquants : `SALON_PHOTO.jpg` (avec le reflet caché), `POLICE_REPORT.pdf` (ou fenêtre texte), et ajouter `SALON_PHOTO.jpg` au mapping manuel fichier→sprite (`imageMappings` sur `CaseWindowUI`).
- [ ] Une fois le contenu narratif en place : tester la boucle d'enquête complète avec de vrais utilisateurs — cf. étape 7 du plan.

> Le système technique qui supportait ces étapes (fenêtre d'analyse faciale, hotspot conditionnel, validation du case via `DiscoveryManager`) est maintenant fait — voir sections ci-dessous. Il ne reste que le contenu narratif et les assets à produire pour boucler l'Affaire #1.

### Système de fenêtres (fait)
- [x] Câblage des clics : `Btn_AnalysePhoto` / `Btn_BaseDeDonnees` ouvrent leurs fenêtres respectives depuis la Sidebar.
- [x] Clic sur un fichier `.jpg` dans `CaseWindow` (`FileEntryUI`) ouvre `ImageViewerController` avec l'image associée (mapping manuel temporaire).
- [x] Boutons de fermeture (×) fonctionnels sur `ImageWindowFrame` et `DatabaseWindowFrame`.
- [x] Parcours MVP testé de bout en bout : ouvrir photo → zoomer → lire plaque → ouvrir base de données → rechercher → obtenir le propriétaire.
- [x] **Restructuration en vraies fenêtres de bureau** : suppression des anciens overlays plein écran modaux (`ImageViewerWindow`, `DatabaseWindow`), toutes les fenêtres (`CaseWindow`, `ImageWindowFrame`, `DatabaseWindowFrame`) regroupées dans une couche commune `WindowsLayer`, par-dessus `Sidebar`/`MainContent` sans les bloquer.
- [x] `WindowDragHandler.cs` créé et attaché aux 3 barres de titre : déplacement au clic-glisser, passage au premier plan au clic (`SetAsLastSibling`).
- [x] Plusieurs fenêtres peuvent maintenant être ouvertes simultanément, déplacées indépendamment, sans bloquer les clics vers Sidebar/MainContent en arrière-plan — comportement de bureau multi-fenêtres validé en Play.

### Système de base de données (fait)
- [x] `DatabaseRecord` / `DatabaseRecordList` créées (structure sérialisable : plate, ownerName, ownerAddress, vehicleModel, notes).
- [x] `DatabaseManager` créé (singleton, charge `database_plates.json` via `Resources.Load` en `Awake()`).
- [x] Recherche tolérante au format : normalisation des plaques (suppression tirets/espaces, mise en majuscules) des deux côtés de la comparaison — `61LE335` trouve `61-LE-335`.
- [x] Fichier `database_plates.json` de test créé dans `Assets/_Data/Resources/` (2 entrées).
- [x] `DatabaseWindow` créée (même style que CaseWindow/ImageViewer) avec champ de recherche (Input Field TMP) et bouton Rechercher.
- [x] `DatabaseWindowUI` branché : affiche les résultats (`ResultGroup`) ou un message "Aucun résultat." (`Result_NotFound`) selon la recherche.
- [x] Testé en Play : recherche exacte et recherche avec/sans tirets fonctionnent, cas "aucun résultat" fonctionne.

### Image Viewer (fait)
- [x] `ImageViewerWindow` créée (overlay semi-transparent + `ImageWindowFrame` centrée, style cohérent avec `CaseWindow`).
- [x] `ImageTitleBar` avec titre du fichier et bouton de fermeture (×).
- [x] `ViewportMask` (Mask + Image) pour clipper l'image affichée aux bords de la fenêtre.
- [x] `DisplayedImage` affichée avec `Preserve Aspect`.
- [x] Script `ImageZoomPan.cs` : zoom à la molette (`OnScroll`) et déplacement au clic-glisser (`OnDrag`), avec clamp de position pour empêcher l'image de sortir du champ de vision quel que soit le niveau de zoom.
- [x] Testé en Play : zoom fluide depuis le centre, pan limité aux bords, aucun vide visible.
- [x] `ImageViewerController` expose `CurrentFileName` et notifie les `PhotoHotspot` enfants à chaque ouverture d'image (`RefreshHotspots()`).

### Système d'analyse faciale (fait)
- [x] `FaceRecord.cs` / `FaceRecordList` créées (structure sérialisable : faceId, identityName, role, matchPercent, notes).
- [x] `FaceDatabaseManager` créé (singleton, charge `face_database.json` via `Resources.Load` en `Awake()`).
- [x] `face_database.json` de test créé dans `Assets/_Data/Resources/` (entrée `reflet_salon` → M. LAURENT, 97.8%).
- [x] `PhotoHotspot.cs` : bouton invisible positionné sur `SALON_PHOTO.jpg`, avec champ `requiredFileName` — n'est cliquable (`interactable = true`) que lorsque l'image actuellement affichée dans l'Image Viewer correspond au fichier attendu.
- [x] `FaceAnalysisWindowUI.cs` : fenêtre dédiée (`FaceAnalysisWindowFrame`), inactive par défaut, activée au clic sur le hotspot (`SetActive(true)` + `SetAsLastSibling()`, cohérent avec le comportement multi-fenêtres existant).
- [x] Bug corrigé : `FaceAnalysisWindowFrame` portait encore un composant `DatabaseWindowUI` résiduel (copier-coller depuis `DatabaseWindowFrame`) — retiré.
- [x] Bug corrigé : le champ `Window Root` de `FaceAnalysisWindowUI` pointait sur `FaceAnalysisWindowRoot` (le porteur du script) au lieu de `FaceAnalysisWindowFrame` (le panel visuel) — la fenêtre ne s'affichait pas malgré une exécution correcte du script.
- [x] Animation d'analyse (3s, remplace l'ancien texte statique "ANALYSE EN COURS...") : barre de progression (`ScanBarFill`, Image Filled/Horizontal), curseur lumineux qui suit la progression (`ScanBarHead`), compteur de pourcentage (`PercentText`), texte de statut qui change de phrase par palier (`StatusText`).
- [x] Sprite `white_pixel.png` créé et ajouté à `_Sprites` : carré blanc plat sans coins arrondis ni dégradé, utilisé sur `ScanBarTrack`/`ScanBarFill`/`ScanBarHead` (le sprite `UISprite` par défaut d'Unity avait des coins arrondis qui s'étiraient de façon visible en mode Filled sur une barre fine).
- [x] Résultat affiché sous forme de bloc texte façon terminal (`MATCH: 97.8%` / `IDENTITY: M. LAURENT` / notes).

### Système de découvertes / validation du case (fait)
- [x] `DiscoveryManager.cs` créé (singleton, attaché au GameObject `GameManager` aux côtés de `CaseManager`/`DatabaseManager`/`FaceDatabaseManager`).
- [x] `DatabaseWindowUI` : une recherche de plaque réussie appelle `DiscoveryManager.Instance.Unlock("vehicle_owner")`.
- [x] `FaceAnalysisWindowUI` : un match facial réussi appelle `DiscoveryManager.Instance.Unlock("reflection_face")`.
- [x] `DiscoveryManager.CheckCaseCompletion()` compare les découvertes débloquées à `CaseData.requiredDiscoveries` du case actuellement chargé (`CaseManager.Instance.CurrentCase`).
- [x] `CaseWindowUI` : singleton `Instance` ajouté, nouvelle méthode `MarkCaseResolved()` — passe `status` à `"RÉSOLU"` et colore le texte en bleu-gris accent (`#4A7FB5`), appelée automatiquement par `DiscoveryManager` une fois toutes les `requiredDiscoveries` trouvées.
- [x] Parcours complet testé en Play : ouvrir dossier → zoomer véhicule → lire plaque → rechercher BDD (`vehicle_owner` débloqué) → ouvrir SALON_PHOTO → cliquer le reflet → analyse 3s animée → résultat facial (`reflection_face` débloqué) → case passe à `RÉSOLU`.

### Système de données (fait)
- [x] Classe `CaseData` créée (structure sérialisable : caseId, title, location, status, assignedAgent, attachedFiles, evidence, requiredDiscoveries).
- [x] `CaseManager` créé (singleton, charge le JSON via `Resources.Load`, chargement en `Awake()` pour garantir l'ordre d'exécution avant les autres scripts).
- [x] Affaire de test `case_0017.json` créée dans `Assets/_Data/Resources/`.
- [x] `CaseWindowUI` créé et branché sur `CaseWindow` : affiche dynamiquement titre, location, status, agent, et génère la liste de fichiers via un prefab `FileEntry` instancié depuis les données du JSON (plus de texte codé en dur).
- [x] Testé en Play : les données du JSON s'affichent correctement dans l'interface.

---

## Ce qu'il reste à faire

Rappel de la feuille de route recommandée (Section 46 du GDD) :

1. ~~Définir l'identité visuelle de l'interface.~~ → **fait** (maquette de référence validée)
2. ~~Créer le prototype Unity du bureau.~~ → **fait** (Sidebar, CaseWindow, icônes, hover states, responsive)
3. ~~Créer la base de données fictive.~~ → **partiellement fait** (une affaire test pilotée par JSON ; reste à créer une vraie base de données de recherche, distincte des affaires)
4. ~~Créer le système de recherche.~~ → **fait** (recherche par plaque, tolérante au format)
5. ~~Créer le visualiseur d'images.~~ → **fait** (zoom + pan avec clamp)
6. ~~Créer une première affaire complète.~~ → **partiellement fait** (CASE_0017 fonctionnel techniquement, système de découvertes/validation en place — reste le contenu narratif : `case_0017.json` mis à jour + assets, cf. Section 3-4 du GDD)
7. Tester la boucle d'enquête avec de vrais utilisateurs.
8. ~~Créer le système de déblocage.~~ → **fait** (`DiscoveryManager`, unlocks `vehicle_owner`/`reflection_face`, résolution automatique du case)
9. Créer le graphe des connexions.
10. Écrire les 5 premières affaires.
11. ~~Définir précisément la chronologie d'ARCHIVE.~~ → fait en amont (voir Section 13.1 du GDD)
12. Construire le vertical slice.

### Détail des prochaines étapes techniques (court terme)

- [x] Définir la palette de couleurs, la typographie et le style des fenêtres (maquette de référence validée) — cf. Section 32 du GDD.
- [ ] Reproduire cette identité visuelle dans Unity : couleurs, polices, style de fenêtres/panels sur le Canvas existant.
- [ ] Ajouter une barre latérale d'applications cliquables (Dossiers, Base de données, Analyse photo/vidéo/audio, Téléphone, Graphe) reprenant la disposition de la maquette.
- [ ] Faire en sorte qu'un clic sur une icône ouvre une fenêtre (Panel UI), même vide pour l'instant.
- [ ] Créer la classe C# représentant une affaire (`caseId`, `title`, `status`, `evidence`, `requiredDiscoveries` — cf. Section 36 du GDD) et un `CaseManager` capable de charger un JSON de test.
- [ ] Créer une affaire fictive de test (1 seule, pour le MVP).
- [ ] Ajouter une fenêtre "Image Viewer" avec zoom/déplacement.
- [ ] Relier le tout en un premier puzzle jouable de bout en bout : photo → zoom → plaque → recherche base de données → propriétaire → document débloqué (objectif MVP, Section 39 du GDD).

---

## Notes / points de vigilance

- Toujours committer avec un message clair après chaque étape validée (voir historique Git pour le détail).
- Le MVP ne doit couvrir **qu'une seule affaire** — ne pas se disperser avant que la boucle de base soit amusante (principe rappelé en Section 39 et 41 du GDD).
