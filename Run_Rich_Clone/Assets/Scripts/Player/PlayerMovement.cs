using Configs;
using DG.Tweening;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        #region Fields

        [SerializeField] private ModelController modelController;
        [SerializeField] private GameConfig gameConfig;

        #region Runtime

        private bool _canMoveLeft = true;
        private bool _canMoveRight = true;
        private bool _preventMovement = true;

        private bool _isDragging = false;
        private Vector3 _startMousePosition;
        private Vector3 _startPlayerPosition;

        #region Cache

        private float _turnDuration = 1f;
        private float _movementSpeed = 5f;
        private float _forwardSpeed = 3f;
        private float _moveSensitivity = 0.0001f; 

        #endregion

        public bool PreventMovement => _preventMovement;
    
        #endregion
    
        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _turnDuration = gameConfig.TurnDuration;
            _movementSpeed = gameConfig.MovementSpeed;
            _forwardSpeed = gameConfig.ForwardSpeed;
            _moveSensitivity = gameConfig.MoveSensitivity;
        }

        private void Update()
        {
            if (_preventMovement)
            {
                CheckTutorialEnd();
            }
            else
            {
                HandleMovement();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            switch (other.tag)
            {
                case "LeftBoundary":
                    _canMoveLeft = false;
                    break;

                case "RightBoundary":
                    _canMoveRight = false;
                    break;

                case "TurnLeft":
                    ExecuteTurn(TurnDirection.Left);
                    break;

                case "TurnRight":
                    ExecuteTurn(TurnDirection.Right);
                    break;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            switch (other.tag)
            {
                case "LeftBoundary":
                    _canMoveLeft = true;
                    break;

                case "RightBoundary":
                    _canMoveRight = true;
                    break;

                case "TurnLeft":
                case "TurnRight":
                    Destroy(other.gameObject); // Destroy turn triggers after use
                    break;
            }
        }

        #endregion

        #region Movement Logic

        public void PreventPlayerMovement(bool prevent)
        {
            _preventMovement = prevent;
        }
    
        private void CheckTutorialEnd()
        {
            if (Input.GetMouseButton(0))
            {
                _preventMovement = false;
                EventsManager.OnTutorialEnded?.Invoke();
            }
        }

        private void HandleMovement()
        {
            Vector3 forwardMovement = transform.forward * (_forwardSpeed * Time.deltaTime);
            float sidewaysMovement = CalculateSidewaysMovement();

            Vector3 newPosition = transform.position + forwardMovement + transform.right * sidewaysMovement;

            if (CanMoveSideways(newPosition))
            {
                transform.position = newPosition;
            }
            else
            {
                transform.position += forwardMovement;
            }

            Vector3 movementDirection = forwardMovement + transform.right * sidewaysMovement;
            if (movementDirection != Vector3.zero)
            {
                modelController.RotateModel(movementDirection, _movementSpeed);
            }
        }


        private float CalculateSidewaysMovement()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isDragging = true;
                _startMousePosition = Input.mousePosition;
                _startPlayerPosition = transform.position;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isDragging = false;
            }

            if (_isDragging)
            {
                Vector3 deltaMouse = Input.mousePosition - _startMousePosition;
                return deltaMouse.x * _moveSensitivity;
            }

            return 0f;
        }

        private bool CanMoveSideways(Vector3 newPosition)
        {
            if (!_canMoveLeft && newPosition.x < transform.position.x)
                return false;
            if (!_canMoveRight && newPosition.x > transform.position.x)
                return false;

            return true;
        }

        #endregion

        #region Rotation Logic

        private enum TurnDirection { Left, Right }

        private void ExecuteTurn(TurnDirection turnDirection)
        {
            float degrees = turnDirection == TurnDirection.Left ? -90 : 90;
            Vector3 targetRotation = transform.eulerAngles + new Vector3(0, degrees, 0);
        
            _preventMovement = true;
        
            transform.DORotate(targetRotation, _turnDuration, RotateMode.FastBeyond360)
                .OnComplete(() => _preventMovement = false);
        }

        #endregion
    }
}
