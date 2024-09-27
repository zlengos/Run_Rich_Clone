using UnityEngine;

namespace Player
{
    public class PlayerModel : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Animator animator;

        #region API

        public Animator Animator => animator;

        #endregion

        #endregion
    }
}