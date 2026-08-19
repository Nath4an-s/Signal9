# SIGNAL 9 — Affaire #1 : "La maison vide"

> Document de conception détaillé pour la première affaire jouable du MVP.
> Complète `SIGNAL_9_GDD_Initial.md` (Section 15, 5.1, 36) et `SIGNAL_9_Suivi_Avancement.md`.
> **Mis à jour** avec le code réel (`CaseData.cs`, `DatabaseRecord.cs`) et les données réelles (`case_0017.json`, `database_plates.json`) fournis par l'auteur. Le schéma ci-dessous colle exactement au code existant.

---

## 1. Constat de départ

`CASE #0017` (le cas de test technique déjà fonctionnel) correspond très probablement, dans l'intention du GDD, à "la maison vide" :

- GDD Section 5.1 : `CASE #0017`, `Disparition`, `Rennes`, `M. Laurent`, fichiers `CCTV_01.mp4 / HOUSE_PHOTO.jpg / VEHICLE.jpg / POLICE_REPORT.pdf`.
- GDD Section 36 : exemple JSON `"title": "The Empty House"`, `requiredDiscoveries` incluant `"reflection_face"`.
- GDD Section 15 : le scénario complet du mystère (famille disparue, reflet dans une photo, correspondance faciale avec l'agent assigné).

**Recommandation** : ne pas créer un nouveau cas — enrichir `case_0017.json` avec le vrai contenu ci-dessous. Cela évite de disperser le travail (cf. principe MVP, Section 39/41 du GDD).

---

## 2. Scénario (contenu narratif)

### Identité de l'affaire

| Champ | Valeur |
|---|---|
| N° affaire | CASE #0017 |
| Incident | Disparition |
| Lieu | Rennes — 14 rue des Lilas |
| Statut | ACTIVE |
| Agent assigné | M. Laurent |

### La famille disparue

> Noms alignés sur l'entrée déjà existante dans `database_plates.json` (plaque `61-LE-335`, propriétaire `Julien Vasseur`, `14 rue des Lilas, Rennes`) — pas besoin de toucher à la base de données.

- **Julien Vasseur** (42 ans) — père, ingénieur.
- **Claire Vasseur** (39 ans) — mère.
- **Noé Vasseur** (11 ans) — fils.

### Chronologie interne (pour vous, pas affichée telle quelle au joueur)

1. **18h02** — la caméra de rue capte la voiture familiale (Peugeot grise, plaque `61-LE-335` — déjà présente dans votre `database_plates.json` de test) se garant devant la maison.
2. **18h07** — la caméra du voisin capte les trois membres de la famille entrant dans la maison.
3. **18h07 → aucune sortie enregistrée**, sur aucune caméra du quartier, à aucun moment ultérieur.
4. La maison est retrouvée en état normal : repas en préparation, télévision allumée, téléphones sur la table, aucune trace d'effraction.
5. Une photo prise par la police lors du constat (angle salon → fenêtre) contient, dans le reflet de la vitre, une silhouette qui ne devrait pas être là.
6. L'analyse faciale de cette silhouette renvoie une correspondance à 97.8% avec **M. Laurent** — l'agent qui, au moment où le joueur reçoit ce résultat, est en train de lui parler dans le jeu.

### Ce que le joueur doit faire (mappé sur les 7 étapes du GDD Section 15)

1. Ouvrir le dossier CASE #0017 depuis Sidebar → Dossiers.
2. Consulter les fichiers joints : `HOUSE_PHOTO.jpg`, `VEHICLE.jpg`, `SALON_PHOTO.jpg` (nouvelle — celle avec le reflet), `POLICE_REPORT.pdf` (texte, pas nécessairement un vrai PDF — peut être une simple fenêtre de texte).
3. Zoomer sur `VEHICLE.jpg` → lire la plaque `61-LE-335`.
4. Rechercher la plaque dans la Base de données → obtenir le propriétaire (Julien Mercier, déjà cohérent avec la famille).
5. Ouvrir `SALON_PHOTO.jpg` dans l'Image Viewer, zoomer sur la fenêtre en arrière-plan.
6. Cliquer sur la zone du reflet (hotspot invisible) → lance l'analyse faciale.
7. Résultat : `MATCH: 97.8% — M. LAURENT`. Anomalie découverte.

---

## 3. Structure de données — `case_0017.json`

Schéma vérifié contre `CaseData.cs` réel : `attachedFiles` est une simple `List<string>` de noms de fichiers (pas d'objets structurés — c'est `FileEntryUI` qui gère l'affichage/le mapping vers les sprites). Version corrigée :

```json
{
  "caseId": "CASE_0017",
  "title": "La maison vide",
  "location": "Rennes",
  "status": "ACTIVE",
  "assignedAgent": "M. Laurent",
  "attachedFiles": [
    "HOUSE_PHOTO.jpg",
    "VEHICLE.jpg",
    "SALON_PHOTO.jpg",
    "POLICE_REPORT.pdf"
  ],
  "evidence": [
    "photo_house_01",
    "vehicle_01",
    "police_report_01"
  ],
  "requiredDiscoveries": [
    "vehicle_owner",
    "reflection_face"
  ]
}
```

Changements par rapport au fichier actuel :
- `title` : `"Disparition"` → `"La maison vide"` (le nom court "Disparition" peut rester comme sous-titre/type d'incident si votre UI l'affiche ainsi — à vous de voir selon `CaseWindowUI`).
- **`CCTV_01.mp4` retiré** de `attachedFiles` : aucun système d'analyse vidéo n'existe encore (le bouton Sidebar "Analyse vidéo" n'est pas câblé). Le laisser cliquable ne mènerait nulle part. À réintroduire quand ce système existera.
- **`SALON_PHOTO.jpg` ajouté** : nouveau fichier, celui qui contient le reflet.
- `evidence` et `requiredDiscoveries` remplis à titre de données structurantes (pas encore consommées par une logique de jeu — aucun "DiscoveryManager" n'existe dans votre code actuel ; ces champs préparent le futur système de déblocage, Section 37 du GDD).

> Note : j'ai retiré `"dead_person_match"` de l'exemple GDD Section 36 — ce discovery appartient au mystère #2 ("le conducteur fantôme", Section 16), pas à celui-ci. Ne pas le mélanger dans CASE_0017 sous peine de complexifier le MVP.

### `database_plates.json`

**Aucune modification nécessaire.** L'entrée `61-LE-335 / Julien Vasseur / 14 rue des Lilas, Rennes` colle déjà parfaitement au scénario — c'est elle qui a servi de base aux noms choisis ci-dessus plutôt que l'inverse.

### Point d'attention technique : `FileEntryUI`

Le Suivi mentionne un *"mapping manuel temporaire"* entre nom de fichier `.jpg` et sprite affiché dans l'Image Viewer. Ajouter `SALON_PHOTO.jpg` à `attachedFiles` nécessite donc d'ajouter aussi son entrée dans ce mapping (probablement un dictionnaire ou une série de champs dans `FileEntryUI.cs` ou `ImageViewerController.cs`) — sinon le clic sur ce fichier n'ouvrira rien.

---

## 4. Nouveau mini-système : analyse faciale

C'est la seule brique technique manquante. Elle doit rester **volontairement simple** — pas de vraie reconnaissance faciale, juste une mécanique scriptée cohérente avec ce que fait déjà `DatabaseManager` (recherche dans une petite base JSON).

### 4.1. Principe de fonctionnement

- Une zone invisible (bouton transparent) est positionnée par-dessus `SALON_PHOTO.jpg` dans l'Image Viewer, à l'endroit exact du reflet.
- Le joueur doit avoir zoomé suffisamment près pour "voir" la zone cliquable (optionnel : ne l'activer qu'au-delà d'un certain niveau de zoom, pour forcer l'exploration).
- Un clic déclenche l'ouverture d'une fenêtre `FaceAnalysisWindow`, avec un court état "Analyse en cours..." (1–2s), puis affiche le résultat.

### 4.2. Nouveaux scripts à créer

Dans `_Scripts/Data/` :
```
FaceRecord.cs          // structure sérialisable : faceId, identityName, role, matchPercent, notes
```

Dans `_Scripts/Managers/` :
```
FaceDatabaseManager.cs // singleton, charge face_database.json via Resources.Load, Awake()
                        // méthode: FaceRecord GetMatch(string faceId)
```

Dans `_Scripts/UI/` :
```
PhotoHotspot.cs         // attaché au bouton invisible sur la photo
                         // champ public: string faceId ("reflet_salon")
                         // OnClick -> appelle FaceAnalysisWindowUI.Open(faceId)

FaceAnalysisWindowUI.cs // gère l'affichage : état "loading" puis résultat
                         // affiche: pourcentage, nom identifié, notes
                         // même style visuel que DatabaseWindow (cohérence UI)
```

### 4.3. `face_database.json` (dans `_Data/Resources/`)

```json
{
  "records": [
    {
      "faceId": "reflet_salon",
      "identityName": "M. LAURENT",
      "role": "Agent de terrain — dossier CASE_0017",
      "matchPercent": 97.8,
      "notes": "ANOMALIE : l'identité correspondante est actuellement en communication active avec l'utilisateur."
    }
  ]
}
```

Cette structure suit exactement le même principe que `database_plates.json` : facile d'ajouter d'autres `FaceRecord` pour de futures affaires sans toucher au code.

### 4.4. Prefab / UI à créer

- `FaceAnalysisWindowFrame` (copier le style de `DatabaseWindowFrame` : TitleBar + bouton fermeture + zone de contenu).
- Un état "scanning" simple suffit (texte `ANALYSE EN COURS...` qui pulse, ou une barre de progression basique) — pas besoin d'animation complexe pour le MVP.
- Résultat affiché sous forme de bloc texte façon terminal, cohérent avec les exemples du GDD (`MATCH: 97.8% / IDENTITY: [...]`).

---

## 5. Assets à produire

| Fichier | Type | Contenu attendu |
|---|---|---|
| `HOUSE_PHOTO.jpg` | Photo | Façade de maison, banale, RAS visible |
| `VEHICLE.jpg` | Photo | Voiture grise, plaque `61-LE-335` lisible au zoom |
| `SALON_PHOTO.jpg` | Photo | Intérieur salon, fenêtre en arrière-plan avec reflet subtil d'une silhouette |
| `POLICE_REPORT.pdf` | Texte/PDF | Constat factuel, sobre, sans donner la réponse (cf. Section 27 du GDD : ne jamais expliquer, seulement montrer). Mentionne la famille Vasseur, l'heure d'entrée (18h07), l'absence de sortie enregistrée. |
| Portrait M. Laurent (optionnel) | Photo | Pour affichage dans le résultat d'analyse faciale, si vous voulez un visuel plutôt qu'un simple texte |

> Rappel Section 25 du GDD (design des photos) : chaque photo doit porter une info principale, une secondaire, et peut contenir un détail caché. Ici : `SALON_PHOTO.jpg` porte le détail caché (le reflet) — ne pas le rendre trop évident ni trop illisible.

---

## 6. Checklist d'implémentation (à intégrer dans `SIGNAL_9_Suivi_Avancement.md`)

- [x] Champs réels de `CaseData.cs` vérifiés — schéma ci-dessus déjà conforme.
- [ ] Mettre à jour `case_0017.json` (nouveau `title`, `attachedFiles`, `evidence`, `requiredDiscoveries` ci-dessus).
- [x] `database_plates.json` déjà correct — aucune modification nécessaire.
- [ ] Produire les 3 nouvelles photos (`SALON_PHOTO.jpg` en particulier) + le rapport texte.
- [ ] Ajouter `SALON_PHOTO.jpg` au mapping manuel fichier→sprite (`imageMappings` sur `CaseWindowUI`).
- [x] Créer `FaceRecord.cs`.
- [x] Créer `FaceDatabaseManager.cs` + `face_database.json`.
- [x] Créer `PhotoHotspot.cs` et l'attacher sur `SALON_PHOTO.jpg` (zone du reflet) — avec en plus un champ `requiredFileName` pour que le hotspot ne soit cliquable que sur la bonne image (non prévu dans la conception initiale, ajouté en cours d'implémentation).
- [x] Créer `FaceAnalysisWindowUI.cs` + prefab de fenêtre (réutilise le style `DatabaseWindowFrame`).
- [x] Tester en Play : ouvrir dossier → zoomer véhicule → lire plaque → rechercher BDD → propriétaire → ouvrir salon → zoomer reflet → cliquer hotspot → résultat facial. (Testé avec les données de test `reflet_salon` / M. LAURENT du `face_database.json` d'exemple ci-dessus — reste à retester une fois `case_0017.json` et les vraies photos en place.)
- [ ] Une fois validé : passer à l'étape 7 de la roadmap (tests avec de vrais utilisateurs).

### Ajouts non prévus dans la conception initiale

- [x] **`DiscoveryManager.cs`** (nouveau, hors scope de ce document à l'origine) : consigne les découvertes (`vehicle_owner`, `reflection_face`) et déclenche automatiquement la résolution du case (`CaseWindowUI.MarkCaseResolved()`, status → `RÉSOLU`) une fois `requiredDiscoveries` entièrement débloqué. Répond à la question "où consigner les infos trouvées pour valider le case" qui n'était pas encore tranchée dans ce document.
- [x] Animation d'analyse faciale : remplace le texte statique "ANALYSE EN COURS..." (1,5s) par une animation de 3s (barre de progression, curseur, compteur %, phrases de statut par palier) — plus cohérent avec le ton "logiciel professionnel" du GDD (Section 32) qu'un simple texte figé.
