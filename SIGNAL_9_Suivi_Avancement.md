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

- [ ] Créer le visualiseur d'image avec zoom (Image Viewer).
- [ ] Créer le système de recherche / base de données fictive.
- [ ] Relier le tout en un premier puzzle jouable de bout en bout (objectif MVP).

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
4. Créer le système de recherche.
5. Créer le visualiseur d'images.
6. Créer une première affaire complète.
7. Tester la boucle d'enquête avec de vrais utilisateurs.
8. Créer le système de déblocage.
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
