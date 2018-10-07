using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class JoinButton : MonoBehaviour {
    private Text buttonText;
    private MatchInfoSnapshot match;
    public GameObject background;
    public GameObject connectMenu;
    //CustomNetworkManager customNetworkManager;

    private void Awake() {
        background = GameObject.Find("Background");
        connectMenu = GameObject.Find("ConnectMenu");
        buttonText = GetComponentInChildren<Text>();
        GetComponent<Button>().onClick.AddListener(JoinMatch);
        //customNetworkManager = FindObjectOfType<CustomNetworkManager>();
    }

    public void Initialize(MatchInfoSnapshot match, Transform panelTransform) {
        this.match = match;
        buttonText.text = match.name;
        transform.SetParent(panelTransform);
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;
        transform.localPosition = Vector3.zero;
    }

    private void JoinMatch() {

        background.SetActive(false);
        connectMenu.SetActive(false);
        FindObjectOfType<CustomNetworkManager>().JoinMatch(match);
    }
}
