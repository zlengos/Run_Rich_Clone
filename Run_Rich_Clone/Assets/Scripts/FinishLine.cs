using Player;
using Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    #region Fields

    [SerializeField] private CanvasGroup runtimeCanvas;
    [SerializeField] private CanvasGroup gameCanvas;
    [SerializeField] private CanvasGroup finishCanvas;
    [SerializeField] private ModelController modelController;
    [SerializeField] private PlayerMovement playerMovement;

    #endregion
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanvasHandler.HideCanvas(runtimeCanvas);
            CanvasHandler.HideCanvas(gameCanvas);
            CanvasHandler.ShowCanvas(finishCanvas);
            
            
            modelController.LevelFinished();
            playerMovement.PreventPlayerMovement(true);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}