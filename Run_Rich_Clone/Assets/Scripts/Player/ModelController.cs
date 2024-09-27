using System.Collections.Generic;
using System.Linq;
using Configs;
using UnityEngine;

namespace Player
{
    public class ModelController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameConfig moneyGameConfig;
        [SerializeField] private PlayerEffects effects;
    
        #region Runtime

        private PlayerModel _currentModelPrefab;
        private PlayerModel _currentModel;

        #region Cache

        private List<PlayerModel> _modelsInGame = new ();
        private Animator _currentAnimator;
        private static readonly int Idle = Animator.StringToHash("idle");
        private static readonly int Spinning = Animator.StringToHash("spinning");
        private static readonly int Walking = Animator.StringToHash("walking");
        private static readonly int Dancing = Animator.StringToHash("dancing");

        #endregion

        #endregion
    
        #endregion

        #region Listening

        private void Start()
        {
            EventsManager.OnTutorialEnded += TutorialEnded;
            EventsManager.OnMoneyChanged += CheckForModel;
        }

        private void OnDisable()
        {
            EventsManager.OnTutorialEnded -= TutorialEnded;
            EventsManager.OnMoneyChanged -= CheckForModel;
        } 

        #endregion
    
        private void Awake()
        {
            _currentModelPrefab = moneyGameConfig.AllModels[1].PlayerModel;

            _currentModel = Instantiate(_currentModelPrefab, transform);
            _modelsInGame.Add(_currentModel);

            _currentAnimator = _currentModel.Animator;
        
            _currentAnimator.SetBool(Idle, true);
        }
    
        public void CheckForModel(int currentMoney)
        {
            var sortedModels = moneyGameConfig.AllModels.OrderByDescending(model => model.MoneyToSwap).ToList();

            PlayerModel bestModelPrefab = null;

            foreach (var model in sortedModels)
            {
                if (currentMoney >= model.MoneyToSwap)
                {
                    bestModelPrefab = model.PlayerModel;
                    break;
                }
            }

            if (bestModelPrefab == null)
                bestModelPrefab = sortedModels.Last().PlayerModel;

            // Сравниваем префабы
            Debug.Log($"{_currentModelPrefab}");
            Debug.Log($"{bestModelPrefab}");

            if (bestModelPrefab == null || _currentModelPrefab == bestModelPrefab) return;

            effects.PlayDressUp();
            ChangeModel(bestModelPrefab);
        }


        public void LevelFinished()
        {
            _currentAnimator.SetBool(Dancing, true);
        }

        private void ChangeModel(PlayerModel model)
        {
            if (_currentModel != null)
            {
                _currentModel.gameObject.SetActive(false);
            }

            PlayerModel newModel = null;

            _currentModelPrefab = model;
            if (_modelsInGame.Contains(model))
            {
                newModel = _modelsInGame.Find(m => m == model);
            }
            else
            {
                newModel = Instantiate(model, transform);
                _modelsInGame.Add(newModel);
            }

            ActivateModel(newModel);
        }

        private void ActivateModel(PlayerModel model)
        {
            _currentModel = model;  

            _currentModel.gameObject.SetActive(true);
            _currentAnimator = _currentModel.Animator;
    
            _currentAnimator.SetTrigger(Spinning); 
        }


        private void TutorialEnded()
        {
            _currentAnimator.SetBool(Walking, true);
        }
        
        public void RotateModel(Vector3 movementDirection, float _movementSpeed)
        {
            if (_currentModel != null)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
                _currentModel.transform.rotation = Quaternion.Slerp(_currentModel.transform.rotation, targetRotation, Time.deltaTime * _movementSpeed);
            }
        }

    }
}