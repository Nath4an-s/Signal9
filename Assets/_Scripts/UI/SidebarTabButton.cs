using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// À attacher sur chacun des 7 boutons de la Sidebar (Btn_Dossiers, Btn_BaseDeDonnees, ...).
// Ne s'occupe PAS d'ouvrir la fenêtre lui-même : ça reste la responsabilité de onActivate
// (câblé en Inspector vers ex. DatabaseWindowUI.Open()), pour ne pas dépendre d'une interface
// commune entre les différentes XxxWindowUI qui n'existe pas dans le code actuel.
public class SidebarTabButton : MonoBehaviour
{
    [Header("Références")]
    public Button button;

    [Tooltip("Objet dont l'ORDRE PARMI SES FRÈRES SOUS WindowsLayer détermine le premier plan.\n" +
             "— CaseWindow / DatabaseWindowFrame : cet objet lui-même.\n" +
             "— ImageWindowFrame / FaceAnalysisWindowFrame / GraphWindowFrame : PAS le Frame — " +
             "assignez le wrapper (ImageWindowRoot / FaceAnalysisWindowRoot / GraphWindowRoot), " +
             "sinon SetAsLastSibling() n'a aucun effet (le Frame est fils unique de son Root).")]
    public GameObject windowFrame;

    [Header("Style actif / inactif (Section 32 : fond #242527 + bordure gauche #4A7FB5 quand actif)")]
    public Image background;
    public Image leftBorder;
    public TMPro.TMP_Text label;

    public Color inactiveBackground = new Color32(0x14, 0x15, 0x16, 0x00);
    public Color activeBackground   = new Color32(0x24, 0x25, 0x27, 0xFF);
    public Color inactiveBorder     = new Color32(0x14, 0x15, 0x16, 0x00);
    public Color activeBorder       = new Color32(0x4A, 0x7F, 0xB5, 0xFF);
    public Color inactiveText       = new Color32(0xA5, 0xA7, 0xAA, 0xFF);
    public Color activeText         = new Color32(0xC5, 0xC6, 0xC8, 0xFF);

    [Tooltip("Coché uniquement sur l'onglet ouvert par défaut au lancement (ex: Btn_Dossiers).")]
    public bool isInitiallyActive;

    [Tooltip("Appelée quand cet onglet devient actif — câblez ici XxxWindowUI.Open().")]
    public UnityEvent onActivate;

    void Awake()
    {
        if (SidebarTabController.Instance == null)
        {
            Debug.LogError($"[Sidebar] Aucun SidebarTabController trouvé dans la scène — " +
                            $"'{name}' ne peut pas s'enregistrer. Ajoutez un GameObject avec ce " +
                            $"composant quelque part dans la scène (ex: à côté de GameManager).",
                            this);
            return;
        }

        SidebarTabController.Instance.Register(this);
        button.onClick.AddListener(() => SidebarTabController.Instance.Activate(this));
    }

    void Start()
    {
        if (isInitiallyActive)
        {
            SidebarTabController.Instance.Activate(this);
        }
        else
        {
            SetActiveVisual(false);
        }
    }

    public void BringWindowToFront()
    {
        if (windowFrame != null) windowFrame.transform.SetAsLastSibling();
    }

    public void SetActiveVisual(bool active)
    {
        if (background != null) background.color = active ? activeBackground : inactiveBackground;
        if (leftBorder != null) leftBorder.color = active ? activeBorder : inactiveBorder;
        if (label != null) label.color = active ? activeText : inactiveText;
    }
}