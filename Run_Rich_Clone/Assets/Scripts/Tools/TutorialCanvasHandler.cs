using Player;
using UnityEngine;

namespace Tools
{
    public class TutorialCanvasHandler : MonoBehaviour
    {
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private CanvasGroup tutorialCanvas;
        [SerializeField] private CanvasGroup runtimeCanvas;

        private void Update()
        {
            if (playerMovement.PreventMovement)
            {
                CanvasHandler.ShowCanvas(tutorialCanvas);
                CanvasHandler.HideCanvas(runtimeCanvas);
                
            }
            else
            {
                CanvasHandler.ShowCanvas(runtimeCanvas);
                CanvasHandler.HideCanvas(tutorialCanvas, () => Destroy(gameObject));
            }
        }
    }
}