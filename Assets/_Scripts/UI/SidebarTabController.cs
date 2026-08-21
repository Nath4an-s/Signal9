using System.Collections.Generic;
using UnityEngine;

// Singleton qui garantit qu'un seul onglet de la Sidebar est "actif" à la fois : il gère
// le style visuel actif/inactif de chaque bouton et fait passer la fenêtre associée au
// premier plan (SetAsLastSibling) parmi les fenêtres superposées de WindowsLayer.
public class SidebarTabController : MonoBehaviour
{
    private static SidebarTabController instance;

    // Accesseur paresseux : ne dépend plus de l'ordre d'exécution des Awake() entre ce
    // contrôleur et les SidebarTabButton. Sans ça, un bouton dont l'Awake() s'exécutait avant
    // celui du contrôleur trouvait Instance == null, plantait silencieusement sur Register(),
    // et son onClick ne se câblait jamais — focus qui "marche pour certains boutons, pas d'autres".
    public static SidebarTabController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<SidebarTabController>();
            }
            return instance;
        }
    }

    private readonly List<SidebarTabButton> tabs = new List<SidebarTabButton>();
    private SidebarTabButton activeTab;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogWarning("[Sidebar] Plusieurs SidebarTabController dans la scène — le premier est ignoré.");
            Destroy(this);
            return;
        }
        instance = this;
    }

    public void Register(SidebarTabButton tab)
    {
        if (!tabs.Contains(tab)) tabs.Add(tab);
    }

    // Appelée par SidebarTabButton au clic. Rend cet onglet actif, désactive visuellement
    // les autres, ramène sa fenêtre au premier plan, puis déclenche son onActivate
    // (typiquement : appeler XxxWindowUI.Open() sur la fenêtre concernée).
    public void Activate(SidebarTabButton tab)
    {
        if (activeTab == tab)
        {
            // Onglet déjà actif : on ramène quand même sa fenêtre au premier plan,
            // au cas où une autre fenêtre (non liée à la Sidebar) l'aurait recouverte.
            tab.BringWindowToFront();
            return;
        }

        foreach (SidebarTabButton other in tabs)
        {
            if (other != tab) other.SetActiveVisual(false);
        }

        activeTab = tab;
        tab.SetActiveVisual(true);
        tab.BringWindowToFront();
        tab.onActivate?.Invoke();
    }
}