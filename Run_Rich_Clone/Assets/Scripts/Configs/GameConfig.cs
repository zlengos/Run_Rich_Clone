using System;
using Player;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(order = 51, fileName = "SO_Config", menuName = "RunRich_Config")]
    public class GameConfig : ScriptableObject
    {
        #region Fields

        [Header("Player settings")] 
        [SerializeField] private float turnDuration = 1f;
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private float forwardSpeed = 3f;
        [SerializeField] private float moveSensitivity = 0.0001f;
        [SerializeField] private int initialMoney = 40;
        
        [Header("Player Model settings")] 
        [SerializeField] private float tiltAngle = 15f; 
        [SerializeField] private float tiltDuration = 0.2f; 
        
        [Header("All models")] 
        [SerializeField] private Model[] allModels;
        
        [Header("All money items")] 
        [SerializeField] private MoneyItem[] allMoneyItems;

        #region API

        public float TurnDuration => turnDuration;
        public float MovementSpeed => movementSpeed;
        public float ForwardSpeed => forwardSpeed;
        public float MoveSensitivity => moveSensitivity;
        public float TiltAngle => tiltAngle;
        public float TiltDuration => tiltDuration;
        public int InitialMoney => initialMoney;
        public Model[] AllModels => allModels;
        public MoneyItem[] AllMoneyItems => allMoneyItems;

        #endregion
        
        #endregion
    }

    [Serializable]
    public class MoneyItem
    {
        #region Fields

        [Header("Money item properties")] 
        [SerializeField] private string name;
        [SerializeField] private int moneyChangeCount; // +20 -20

        #region Properties

        public string Name => name;
        public int MoneyChangeCount => moneyChangeCount;

        #endregion

        #endregion
    }

    [Serializable]
    public class Model
    {
        #region Fields

        [Header("Model Properties")] 
        [SerializeField] private float moneyToSwap;
        [SerializeField] private PlayerModel playerModel; // default: 4 items (0 - poor, 1 - descent, 2 - millionaire, 3 - rich)

        #region Properties

        public float MoneyToSwap => moneyToSwap;
        public PlayerModel PlayerModel => playerModel;

        #endregion

        #endregion
    }
}