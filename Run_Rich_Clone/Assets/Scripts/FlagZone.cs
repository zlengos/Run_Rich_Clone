using UnityEngine;

public class FlagZone : MonoBehaviour
{
    #region Fields

    [SerializeField] private Animator animatorFlag1;
    [SerializeField] private Animator animatorFlag2;
    
    private static readonly int Open = Animator.StringToHash("Open");
    
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animatorFlag1.SetTrigger(Open);
            animatorFlag2.SetTrigger(Open);
        }
    }
}