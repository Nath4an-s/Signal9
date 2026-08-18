# SIGNAL 9 — Document de conception initial

## 1. Vision du projet

**SIGNAL 9** est un jeu d'enquête narratif en vue ordinateur, inspiré dans sa boucle de gameplay de *The Operator*, mais avec un univers, une intrigue, des personnages, des affaires et des mécaniques propres.

Le joueur incarne un analyste/opérateur travaillant pour une société privée spécialisée dans l'analyse d'incidents et la recherche de personnes.

Le gameplay repose presque entièrement sur l'utilisation d'un ordinateur :

- consulter des dossiers ;
- rechercher des personnes ;
- analyser des photos ;
- regarder des vidéos ;
- écouter des appels ;
- analyser des fichiers audio ;
- lire des métadonnées ;
- comparer des visages ;
- rechercher des plaques d'immatriculation ;
- consulter des bases de données ;
- relier des indices ;
- communiquer avec des agents sur le terrain ;
- résoudre progressivement des affaires.

Le principe narratif central est :

> **Certaines personnes n'existent que dans les données. D'autres existent réellement, mais semblent avoir été effacées des données.**

Le jeu commence comme un thriller policier réaliste et évolue progressivement vers un mystère technologique beaucoup plus vaste.

---

# 2. Objectif d'expérience

Le joueur doit ressentir trois choses :

### 2.1. La satisfaction de l'enquête

Chaque découverte doit donner envie de chercher la suivante.

Exemple :

**Photo → plaque → véhicule → propriétaire → entreprise → ancien contrat → autre affaire**

Le joueur doit avoir le sentiment d'avoir lui-même découvert la connexion.

### 2.2. La paranoïa

Au début, les événements semblent être des erreurs administratives ou des coïncidences.

Puis les coïncidences deviennent trop nombreuses.

Le joueur commence à se demander :

- Qui contrôle les données ?
- Qui falsifie les dossiers ?
- Qui observe qui ?
- Pourquoi certaines personnes sont-elles effacées ?
- Est-ce que les bases de données disent la vérité ?
- Peut-on faire confiance à ce que l'ordinateur affiche ?

### 2.3. Le doute

La dernière partie doit remettre en question le rôle du joueur lui-même.

Le joueur n'est plus uniquement celui qui enquête.

Il devient potentiellement **un élément de l'enquête**.

---

# 3. Boucle de gameplay principale

La boucle fondamentale est :

```text
NOUVEL INCIDENT
      ↓
RECEVOIR LES INFORMATIONS
      ↓
OUVRIR LE DOSSIER
      ↓
ANALYSER LES INDICES
      ↓
RECHERCHER DANS LES BASES
      ↓
TROUVER UNE CORRESPONDANCE
      ↓
OBTENIR DE NOUVEAUX INDICES
      ↓
RELIER LES AFFAIRES
      ↓
PRENDRE UNE DÉCISION
      ↓
NOUVELLE CONSÉQUENCE
```

Chaque affaire doit idéalement produire un ou plusieurs éléments qui seront utiles plus tard.

---

# 4. Interface principale

Le jeu se déroule principalement sur un bureau informatique.

L'écran principal pourrait contenir :

- **Dossiers**
- **Base de données**
- **Recherche**
- **Analyse photo**
- **Analyse vidéo**
- **Analyse audio**
- **Téléphone**
- **Messagerie**
- **Carte**
- **Graphe des connexions**
- **Journal de l'enquête**
- **Fichiers reçus**

L'ordinateur doit donner l'impression d'être un véritable outil professionnel plutôt qu'un simple menu de jeu.

---

# 5. Applications de l'ordinateur

## 5.1. CASES

Application permettant d'ouvrir les affaires.

Chaque affaire contient :

- résumé ;
- niveau de priorité ;
- personne responsable ;
- historique ;
- pièces jointes ;
- communications ;
- indices découverts ;
- statut.

Exemple :

```text
CASE #0017

INCIDENT:
Disparition

LOCATION:
Rennes

STATUS:
ACTIVE

ASSIGNED AGENT:
M. Laurent

ATTACHED FILES:
- CCTV_01.mp4
- HOUSE_PHOTO.jpg
- VEHICLE.jpg
- POLICE_REPORT.pdf
```

---

# 6. Base de données

Une base de données constitue l'un des principaux outils du joueur.

Elle permet de rechercher :

- personnes ;
- véhicules ;
- plaques ;
- entreprises ;
- adresses ;
- numéros de téléphone ;
- dossiers médicaux ;
- décès ;
- contrats ;
- employés ;
- propriétés ;
- événements.

Les recherches peuvent donner des résultats incomplets.

Exemple :

```text
SEARCH:
Thomas Vale

RESULTS:

Thomas Vale
DOB: 14/05/1978
STATUS: DECEASED
DATE OF DEATH: 12/08/2010

PHOTO:
[IMAGE]
```

Puis le joueur trouve exactement la même personne dans une vidéo enregistrée en 2026.

---

# 7. Analyse photographique

Les photos sont des puzzles.

Le joueur peut :

- zoomer ;
- recadrer ;
- augmenter le contraste ;
- améliorer certains détails ;
- rechercher des visages ;
- lire des plaques ;
- lire des panneaux ;
- analyser les métadonnées ;
- comparer deux images.

Il ne faut pas rendre tous les puzzles artificiellement difficiles.

La majorité des indices doivent être logiques.

Exemple :

Une photographie semble banale.

En zoomant :

- un panneau indique une rue ;
- une voiture possède une plaque ;
- une fenêtre reflète une silhouette ;
- une affiche indique une date ;
- les métadonnées donnent le modèle de caméra.

Plusieurs de ces éléments peuvent être utiles.

---

# 8. Analyse vidéo

Le joueur peut :

- lire ;
- mettre en pause ;
- avancer image par image ;
- accélérer ;
- ralentir ;
- zoomer ;
- extraire une image ;
- analyser l'audio ;
- consulter les métadonnées.

Les vidéos doivent contenir des informations qui ne sont pas forcément visibles au premier visionnage.

Exemple :

Une vidéo de surveillance montre une rue pendant 30 secondes.

À première vue : rien.

À 27 secondes :

- un véhicule passe ;
- une personne apparaît dans un reflet ;
- une enseigne devient lisible ;
- une seconde caméra est visible.

Le joueur doit choisir ce qui est pertinent.

---

# 9. Analyse audio

Les appels et enregistrements audio permettent d'introduire d'autres types de puzzles.

Possibilités :

- voix ;
- bruit de fond ;
- téléphone ;
- train ;
- sirène ;
- musique ;
- annonce publique ;
- bruit industriel ;
- accent ;
- heure audible ;
- conversation partiellement inaudible.

Exemple :

Un appel d'urgence semble impossible à localiser.

En analysant l'arrière-plan, le joueur entend un train.

Il identifie :

1. le type de train ;
2. la ligne ;
3. la station ;
4. l'heure approximative ;
5. la localisation de l'appelant.

---

# 10. Métadonnées

Les métadonnées doivent être une mécanique importante.

Un fichier peut révéler :

- date de création ;
- appareil utilisé ;
- localisation ;
- logiciel utilisé ;
- auteur ;
- date de modification ;
- historique d'édition.

Exemple :

```text
FILE:
CCTV_07.mp4

CREATED:
Friday, 21:14

MODIFIED:
Friday, 21:19

CAMERA:
Morrow Security System

LOCATION:
UNKNOWN
```

Le joueur découvre ensuite que l'événement censé avoir été filmé vendredi a officiellement eu lieu mardi.

---

# 11. Communications

Le joueur reçoit régulièrement des messages et des appels.

Les agents sur le terrain peuvent :

- demander une identité ;
- demander une adresse ;
- transmettre une photo ;
- demander une analyse ;
- demander une recherche ;
- confirmer ou contester une découverte.

Les conversations peuvent aussi évoluer selon les informations découvertes.

Un agent peut devenir :

- allié ;
- méfiant ;
- suspect ;
- victime ;
- source d'informations.

---

# 12. Le graphe des connexions

Une mécanique centrale.

Chaque personne, lieu, entreprise, véhicule, fichier et affaire peut devenir un nœud.

Exemple :

```text
              MORROW SYSTEMS
                 /       \
                /         \
        SECURITY CAM     CONTRACT
             |               |
          HOUSE           POLICE
             |               |
       DISAPPEARANCE      BODY
             \             /
              \           /
               UNKNOWN MAN
```

Le joueur peut créer manuellement des connexions.

Certaines connexions sont correctes.

Certaines sont des fausses pistes.

Certaines sont ambiguës.

L'objectif n'est donc pas uniquement de collecter des informations, mais de **comprendre leur relation**.

---

# 13. Structure narrative

Le jeu est divisé en plusieurs actes.

## ACTE I — Incidents

Le joueur apprend les outils.

Les affaires sont principalement réalistes.

Exemples :

- disparition ;
- cambriolage ;
- accident ;
- personne recherchée ;
- véhicule suspect.

Objectif : apprendre au joueur à enquêter.

---

## ACTE II — Anomalies

Des incohérences apparaissent.

Exemples :

- personne officiellement décédée retrouvée sur une caméra ;
- véhicule détruit apparaissant encore dans les données ;
- vidéo dont la date est impossible ;
- personne possédant deux identités ;
- dossier administratif contradictoire.

Le joueur commence à comprendre que quelque chose ne va pas.

---

## ACTE III — Morrow Systems

Une même entreprise apparaît dans plusieurs affaires.

Morrow Systems semble être une société technologique spécialisée dans :

- sécurité ;
- surveillance ;
- analyse vidéo ;
- intelligence artificielle ;
- stockage de données.

Elle semble parfaitement légitime.

Mais elle apparaît partout.

---

## ACTE IV — ARCHIVE

Le joueur découvre l'existence d'un projet secret :

# ARCHIVE

ARCHIVE est un système capable de reconstruire une personne à partir de ses traces numériques.

Sources utilisées :

- photos ;
- vidéos ;
- téléphones ;
- GPS ;
- achats ;
- dossiers médicaux ;
- communications ;
- réseaux sociaux ;
- données administratives.

L'objectif officiel est de retrouver des personnes disparues.

Mais le projet a évolué.

---

# 13.1. Chronologie d'ARCHIVE (éléments fixés)

Ces points sont désormais fixés en interne, pour guider l'écriture, même si le joueur n'en aura jamais une vision complète et certaine :

- **Origine (années 1990) :** ARCHIVE trouve son origine dans un programme de recherche militaire/universitaire des années 1990, bien avant l'existence de Morrow Systems sous sa forme actuelle. Le projet moderne récupère et prolonge des travaux plus anciens.
- **Dérive de l'objectif :** ARCHIVE n'a pas été conçu dès le départ comme un outil de prédiction déguisé. Son objectif officiel (retrouver des personnes disparues) était sincère à l'origine. C'est une petite équipe interne à Morrow qui, avec le temps, a progressivement détourné le système vers la prédiction de comportements — sans mandat officiel, et probablement sans que toute la hiérarchie de Morrow le sache.
- **L'accident de 2009 (conducteur fantôme, Section 16) :** son lien avec ARCHIVE doit rester volontairement incertain. Ni le joueur ni la bible narrative ne doivent trancher avec certitude s'il s'agit d'un test lié au système ou d'une coïncidence troublante. Les indices peuvent pencher dans un sens ou dans l'autre selon les affaires, mais aucune scène ne doit confirmer l'un ou l'autre de façon définitive.

Ces décisions servent de socle pour écrire les affaires et les dossiers de façon cohérente, sans pour autant résoudre le mystère central (voir Section 21).

---

# 14. Le véritable secret d'ARCHIVE

ARCHIVE ne se contente plus d'identifier les personnes.

Il tente de **prédire leurs comportements**.

Avec suffisamment de données, le système peut simuler les actions futures d'un individu.

Cependant, certains résultats sont impossibles.

Le système commence à prédire :

- des personnes qui n'existent pas ;
- des événements qui ne se sont jamais produits ;
- des individus avant leur naissance ;
- des personnes officiellement mortes ;
- des événements qui se produiront plusieurs jours plus tard.

La question devient :

> ARCHIVE prédit-il le futur, ou le futur suit-il les prédictions d'ARCHIVE ?

---

# 15. Le premier grand mystère : la maison vide

Une famille disparaît.

La maison est parfaitement normale.

À l'intérieur :

- nourriture fraîche ;
- télévision allumée ;
- téléphones ;
- voitures ;
- vêtements ;
- ordinateurs.

Les caméras montrent la famille entrant dans la maison.

Aucune caméra ne montre la famille en sortir.

Le joueur doit :

1. analyser les caméras ;
2. identifier les véhicules ;
3. rechercher les téléphones ;
4. examiner les photos ;
5. rechercher les membres de la famille ;
6. comparer les horaires ;
7. trouver une anomalie.

Une photographie révèle une deuxième personne dans un reflet.

L'analyse faciale retourne :

```text
MATCH: 97.8%

IDENTITY:
[AGENT RESPONSABLE DU DOSSIER]
```

Le problème :

**l'agent est actuellement en train de communiquer avec le joueur.**

---

# 16. Deuxième mystère : le conducteur fantôme

Une caméra de péage enregistre plusieurs fois la même voiture.

Même modèle.

Même plaque.

Même conducteur.

Mais les passages sont espacés de 17 ans.

La recherche indique :

```text
VEHICLE:
DESTROYED — 2009
```

Une vieille photographie de l'accident montre le conducteur.

Il ressemble exactement à l'homme de la vidéo récente.

---

# 17. Troisième mystère : la vidéo impossible

Une vidéo de surveillance doit être datée.

Le joueur utilise :

- horloge ;
- ombres ;
- météo ;
- véhicules ;
- panneaux ;
- métadonnées.

Il conclut :

```text
TUESDAY — 23:17
```

Mais les métadonnées indiquent :

```text
CREATED:
FRIDAY — 04:32
```

La vidéo a donc été modifiée.

En examinant chaque image, une personne apparaît pendant une seule frame.

---

# 18. Quatrième mystère : l'appel impossible

Une personne appelle les secours pour signaler un accident.

Le joueur recherche :

- numéro ;
- antenne ;
- localisation ;
- bruit de fond ;
- heure.

L'appel provient d'un téléphone actuellement conservé comme pièce à conviction dans un commissariat.

---

# 19. Cinquième mystère : le faux mort

Un cadavre non identifié est découvert.

Résultats :

```text
FINGERPRINT: NO MATCH
DNA: NO MATCH
FACE: NO MATCH
```

Une photographie scolaire ancienne permet cependant de reconnaître le visage.

L'identité est retrouvée.

La personne est officiellement morte à 14 ans.

Le cadavre a environ 40 ans.

---

# 20. Le twist personnel

Dans la dernière partie, le joueur effectue une recherche dans ARCHIVE.

Il trouve :

```text
OPERATOR PROFILE

NAME:
[PLAYER]

DATE OF BIRTH:
[PLAYER]

STATUS:
DECEASED

DATE OF DEATH:
[3 DAYS FROM NOW]
```

Le dossier contient :

- photo du joueur enfant ;
- adresse ;
- historique professionnel ;
- conversations ;
- déplacements ;
- données personnelles.

Le joueur n'a jamais fourni ces informations.

Il découvre ensuite que certaines de ses actions dans le jeu ont déjà été enregistrées dans ARCHIVE.

---

# 21. Idée de révélation finale

La révélation doit rester ambiguë.

Trois interprétations doivent être possibles :

### Option A — ARCHIVE prédit réellement le futur

Le système possède suffisamment de données pour prévoir les comportements humains.

### Option B — ARCHIVE manipule les événements

Les prédictions sont envoyées aux bonnes personnes afin qu'elles deviennent réalité.

### Option C — Le système n'est pas ce que le joueur croit

ARCHIVE n'analyse pas uniquement des données.

Quelque chose utilise ARCHIVE.

**Décision : l'ambiguïté est volontaire et définitive — y compris en interne.** Il n'existe pas de réponse canon parmi A, B ou C que l'équipe garderait secrète. L'écriture doit rester cohérente avec les trois interprétations simultanément, sans jamais trancher, ni dans le jeu ni dans la bible narrative.

Le jeu ne doit pas forcément donner une réponse définitive.

---

# 22. Types de puzzles

Le jeu peut utiliser plusieurs familles de puzzles.

## Identification

Trouver une personne à partir de :

- photo ;
- plaque ;
- téléphone ;
- empreinte ;
- voix ;
- adresse.

## Géolocalisation

Déterminer un lieu grâce à :

- panneau ;
- architecture ;
- météo ;
- bruit ;
- ligne de train ;
- plaque ;
- paysage.

## Chronologie

Déterminer si les événements sont cohérents.

## Métadonnées

Découvrir qu'un fichier n'est pas ce qu'il prétend être.

## Comparaison

Comparer :

- deux visages ;
- deux photos ;
- deux véhicules ;
- deux documents ;
- deux signatures ;
- deux vidéos.

## Recherche documentaire

Trouver une information cachée dans des documents.

## Déduction

Faire plusieurs recherches et comprendre la relation entre les résultats.

---

# 23. Fausse piste

Il est important que toutes les informations ne soient pas directement utiles.

Exemple :

Une photo contient cinq détails intéressants.

Seul un est important.

Le joueur doit apprendre à déterminer ce qui compte.

Cela rend l'enquête plus naturelle.

---

# 24. Difficulté progressive

## Niveau 1

Un indice mène directement à un résultat.

```text
PLAQUE → PROPRIÉTAIRE
```

## Niveau 2

Deux ou trois recherches.

```text
PHOTO
 ↓
PLAQUE
 ↓
VÉHICULE
 ↓
PROPRIÉTAIRE
```

## Niveau 3

Plusieurs sources.

```text
PHOTO
 + VIDÉO
 + MÉTADONNÉES
 + BASE DE DONNÉES
        ↓
     CONCLUSION
```

## Niveau 4

Informations contradictoires.

Le joueur doit déterminer laquelle est fiable.

## Niveau 5

Le jeu ne dit plus clairement ce qui est vrai.

Le joueur doit construire sa propre théorie.

---

# 25. Design des photos

Les photos doivent être conçues comme de véritables scènes d'enquête.

Chaque image peut contenir :

### Information principale

Ce que le joueur est censé remarquer.

### Information secondaire

Utile pour une autre étape.

### Détail caché

Visible seulement en zoomant.

### Détail narratif

Qui ne sert pas immédiatement mais prendra du sens plus tard.

### Faux indice

Une information qui semble importante mais ne l'est pas.

---

# 26. Design des vidéos

Chaque vidéo devrait idéalement avoir plusieurs couches.

### Couche 1

Ce que le joueur voit immédiatement.

### Couche 2

Ce qu'il remarque en mettant pause.

### Couche 3

Ce qu'il découvre en analysant.

### Couche 4

Ce qu'il comprend seulement après avoir obtenu d'autres informations.

Ainsi, une même vidéo peut redevenir intéressante plusieurs heures plus tard.

---

# 27. Principe narratif important

**Ne jamais expliquer immédiatement ce que le joueur vient de découvrir.**

Si le joueur voit une personne morte depuis 17 ans :

Ne pas afficher :

> "C'est impossible, cette personne est morte."

Afficher :

```text
MATCH FOUND

STATUS:
DECEASED

DATE:
2009
```

Puis laisser le joueur comprendre lui-même.

---

# 28. Personnages principaux

## Le joueur

Analyste/opérateur.

Compétent mais relativement nouveau dans l'organisation.

Il ne se déplace presque jamais physiquement.

Sa force est son accès aux données.

---

## Agent Laurent

Agent de terrain principal.

Pragmatique.

Il fournit les premiers dossiers.

Relation initiale professionnelle.

Il devient progressivement méfiant envers son propre employeur.

---

## Maya Chen

Analyste technique.

Spécialiste des systèmes et de la cybersécurité.

Elle aide à comprendre ARCHIVE.

Elle pourrait être la première personne à soupçonner que les fichiers du joueur sont surveillés.

---

## Directeur Hale

Supérieur hiérarchique.

Calme et professionnel.

Il minimise toujours les anomalies.

Plus le jeu avance, plus ses réponses deviennent étranges.

Il sait beaucoup plus de choses qu'il ne le dit.

---

# 29. Morrow Systems

Morrow doit sembler parfaitement crédible.

Activités officielles :

- sécurité ;
- vidéosurveillance ;
- analyse de données ;
- infrastructures ;
- IA ;
- systèmes urbains.

Pas de logo caricatural.

Pas de laboratoire secret immédiatement évident.

L'entreprise doit donner l'impression d'être une vraie société technologique.

Le joueur doit pouvoir consulter :

- site web fictif ;
- communiqués de presse ;
- rapports annuels ;
- offres d'emploi ;
- brevets ;
- contrats publics ;
- articles de presse.

Ces éléments peuvent servir de puzzles.

---

# 30. Architecture narrative

Une bonne structure pourrait être :

```text
AFFAIRE 01
  ↓
AFFAIRE 02
  ↓
AFFAIRE 03
  ↓
PETITE ANOMALIE
  ↓
AFFAIRE 04
  ↓
MORROW SYSTEMS
  ↓
ARCHIVE
  ↓
DONNÉES IMPOSSIBLES
  ↓
PROFIL DU JOUEUR
  ↓
DATE DU DÉCÈS
```

L'escalade doit être lente.

Le jeu ne doit pas révéler son véritable genre dans les premières heures.

---

# 31. Atmosphère

Le jeu doit être relativement calme.

Pas besoin de combat.

Pas besoin de monstres.

Pas besoin de jumpscares constants.

La tension vient de :

- sons du bureau ;
- téléphone ;
- notifications ;
- écran qui change ;
- fichiers qui apparaissent ;
- messages inattendus ;
- incohérences ;
- silence.

Une anomalie informatique peut être plus inquiétante qu'un monstre.

---

# 32. Direction artistique

Style réaliste mais légèrement froid.

Interface :

- sobre ;
- professionnelle ;
- fonctionnelle ;
- crédible ;
- légèrement vieillissante.

Éviter l'esthétique « hacker cyberpunk ».

Le monde doit ressembler à une infrastructure informatique réaliste.

---

# 33. Son

Le son est très important.

Sons récurrents :

- ventilateur du PC ;
- clavier ;
- souris ;
- notifications ;
- téléphone ;
- disque dur ;
- appels ;
- radio ;
- bruit de bureau ;
- pluie ;
- circulation.

Au début, ces sons sont rassurants.

Plus tard, certains peuvent devenir associés aux anomalies.

## 33.1. Doublage

**Décision : les personnages sont entièrement doublés (voix complètes).**

Cela concerne notamment Laurent, Maya Chen, Directeur Hale, ainsi que les appels et communications reçus par le joueur. Contrairement à *The Operator* (texte uniquement), SIGNAL 9 mise sur la voix pour renforcer l'immersion et la tension (silences, hésitations, ton qui change progressivement au fil des actes).

Impact à prévoir : casting vocal, direction d'acteurs, budget audio plus important, et prise en compte du doublage dans le planning de production (Section 46).

---

# 34. Économie des assets

Le concept permet de produire beaucoup de contenu sans créer énormément de personnages 3D.

Les principaux assets sont :

- photos ;
- vidéos ;
- documents ;
- interfaces ;
- icônes ;
- voix ;
- fichiers audio ;
- cartes ;
- captures de caméra ;
- portraits.

Cela permet de concentrer les efforts sur l'écriture et le design des énigmes.

---

# 35. Unity — architecture technique confirmée

**Décision : Unity est le moteur confirmé pour le développement.**

Une architecture simple peut suffire pour un premier prototype.

## Systèmes principaux

```text
GameManager
├── CaseManager
├── DatabaseManager
├── EvidenceManager
├── MediaViewer
│   ├── ImageViewer
│   ├── VideoViewer
│   └── AudioPlayer
├── SearchSystem
├── CommunicationSystem
├── ConnectionGraph
├── SaveSystem
└── NarrativeManager
```

---

# 36. Système de données

Les affaires devraient être pilotées par des données plutôt que codées individuellement.

Par exemple :

```json
{
  "caseId": "CASE_001",
  "title": "The Empty House",
  "status": "active",
  "evidence": [
    "photo_house_01",
    "cctv_house_01",
    "vehicle_01",
    "police_report_01"
  ],
  "requiredDiscoveries": [
    "vehicle_owner",
    "dead_person_match",
    "reflection_face"
  ]
}
```

Cela permet de créer de nouvelles affaires sans modifier toute la logique du jeu.

---

# 37. Système de déblocage

Une découverte peut débloquer :

- un nouveau fichier ;
- une nouvelle personne ;
- une nouvelle recherche ;
- un nouvel appel ;
- une nouvelle localisation ;
- une nouvelle affaire.

Exemple :

```text
DISCOVERY:
Vehicle owner identified

UNLOCK:
Person: Thomas Vale
Company: Morrow Systems
File: 2009 Accident Report
```

---

# 38. Sauvegarde

Le jeu doit sauvegarder :

- affaires ouvertes ;
- indices découverts ;
- recherches effectuées ;
- fichiers consultés ;
- connexions créées ;
- conversations ;
- décisions ;
- état narratif.

Une sauvegarde automatique après chaque découverte importante est recommandée.

---

# 39. MVP — premier prototype

Ne pas commencer par tout développer.

Le premier prototype devrait contenir uniquement :

### Interface

- bureau ;
- fenêtre de dossier ;
- base de données ;
- visualiseur d'image.

### Gameplay

Une seule affaire.

### Puzzle

```text
PHOTO
 ↓
ZOOM
 ↓
PLAQUE
 ↓
BASE DE DONNÉES
 ↓
PROPRIÉTAIRE
 ↓
DOCUMENT
 ↓
ANOMALIE
```

### Objectif

Prouver que la boucle :

> **observer → chercher → découvrir → connecter**

est amusante avant de produire le reste du jeu.

---

# 40. Vertical Slice

Après le prototype, créer une mini-version de 30 à 60 minutes comprenant :

- 3 affaires ;
- photos ;
- 1 vidéo ;
- 1 appel ;
- base de données ;
- recherche ;
- quelques faux indices ;
- premier lien avec Morrow Systems ;
- première grosse anomalie.

Le joueur doit terminer le vertical slice avec la question :

> **« Attends... comment cette personne peut-elle être là ? »**

---

# 41. Principes de design à conserver

## 1. Le joueur doit faire les connexions

Ne pas afficher immédiatement les réponses.

## 2. Les outils doivent être utiles

Chaque application de l'ordinateur doit avoir une fonction réelle.

## 3. Les indices doivent être cohérents

Même si la solution est surprenante, elle doit être logique rétrospectivement.

## 4. Les anomalies doivent augmenter progressivement

Pas de surnaturel évident au début.

## 5. Les anciennes affaires doivent redevenir pertinentes

Un fichier banal du début peut devenir essentiel à la fin.

## 6. Le joueur doit pouvoir avoir ses propres théories

Le jeu doit laisser suffisamment de place au doute.

---

# 42. Référence de ton

Le ton recherché :

**Thriller d'enquête + paranoïa technologique + mystère scientifique.**

Pas :

- horreur classique ;
- science-fiction spectaculaire ;
- cyberpunk ;
- enquête policière traditionnelle.

L'horreur éventuelle doit venir de l'idée que :

> **les données qui décrivent notre existence pourraient nous connaître mieux que nous-mêmes.**

---

# 43. Question centrale du jeu

Toute l'histoire doit progressivement revenir à cette question :

> **Si une base de données possède suffisamment d'informations sur vous, est-ce qu'elle peut déterminer qui vous êtes ?**

Puis :

> **Si elle peut déterminer qui vous êtes, peut-elle déterminer ce que vous allez faire ?**

Et finalement :

> **Si elle peut déterminer ce que vous allez faire, avez-vous encore réellement le choix ?**

---

# 44. Potentiel de suite

Le premier jeu peut se terminer sans résoudre complètement ARCHIVE.

Cela permettrait éventuellement une suite :

**SIGNAL 9 — ARCHIVE**

où le joueur découvrirait :

- l'origine du système ;
- les premiers sujets ;
- les expériences ;
- les prédictions historiques ;
- l'identité du véritable créateur ;
- pourquoi le système possède les données du joueur.

---

# 45. Résumé du concept

**SIGNAL 9** est un thriller d'enquête basé sur un ordinateur.

Le joueur analyse des photos, vidéos, appels, documents et bases de données afin de résoudre des incidents.

Au début, il enquête sur des disparitions et des anomalies ordinaires.

Puis il découvre que plusieurs affaires sont liées à une entreprise appelée **Morrow Systems**.

Morrow développe **ARCHIVE**, un système capable de reconstruire et de prédire les comportements humains à partir des traces numériques.

Le système commence toutefois à produire des résultats impossibles.

Des morts apparaissent vivants.

Des personnes inexistantes apparaissent dans les bases.

Des vidéos semblent provenir du futur.

Et finalement, le joueur découvre son propre profil dans ARCHIVE.

Le statut du profil :

```text
DECEASED
```

La date du décès :

```text
DANS 3 JOURS
```

Le joueur doit alors déterminer si ARCHIVE connaît réellement le futur, s'il manipule les événements, ou si quelque chose d'autre utilise le système.

---

# 46. Prochaine étape de développement recommandée

Avant de programmer le jeu complet, créer ces éléments dans l'ordre :

1. **Définir l'identité visuelle de l'interface.**
2. **Créer le prototype Unity du bureau.**
3. **Créer la base de données fictive.**
4. **Créer le système de recherche.**
5. **Créer le visualiseur d'images.**
6. **Créer une première affaire complète.**
7. **Tester la boucle d'enquête avec de vrais utilisateurs.**
8. **Créer le système de déblocage.**
9. **Créer le graphe des connexions.**
10. **Écrire les 5 premières affaires.**
11. **Définir précisément la chronologie d'ARCHIVE.**
12. **Construire le vertical slice.**

La priorité absolue est de réussir **une seule enquête très satisfaisante** avant de construire tout l'univers.

