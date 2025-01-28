using TMPro;
using UnityEngine;


public class UIController : MonoBehaviour {
    [Header("Game references")]//Will be replaced by game manager later
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Goal goal;

    [Header("UI references")]
    [SerializeField] private GameObject ClickTutorial;//Text to show the user what to do
    [SerializeField] private TextMeshProUGUI WinnerDisplay;//Text that displays the winning agent

    private void Start(){
        
        if(goal != null){
            goal.AgentArrivedAction += ShowWinner;
        }
        else{
            Debug.LogWarning("Missing goal reference");
        }
    }

    private void HideTutorial(){
        if(ClickTutorial.activeInHierarchy){
            ClickTutorial.SetActive(false);
        }
    }
    private void ShowWinner(string WinnerName){
        if(WinnerDisplay != null){
            WinnerDisplay.gameObject.SetActive(true);
            WinnerDisplay.SetText($"{WinnerName} won");
        }
    }
}