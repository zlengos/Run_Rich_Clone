using System.Linq;
using Configs;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerMoneyController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private ModelController modelController;
        [SerializeField] private GameConfig gameConfig;
        [SerializeField] private PlayerEffects effects;
    
        // + or - when picked
        [SerializeField] private Transform currentMoneyUp;
        [SerializeField] private TextMeshProUGUI textCurrentMoneyChanges;
        [SerializeField] private Color colorPlus = Color.green;
        [SerializeField] private Color colorMinus = Color.red;
        [SerializeField] private CanvasGroup currentMoneyChanges;

        [SerializeField] private TextMeshProUGUI textCurrentMoney;
        [SerializeField] private Image progressbar;
    
        #region Runtime

        private int _moneyChange;
        private int _currentMoneyCount;
        private int _tempEquippedMoneyCount;
    
        #endregion
    
        #endregion

        private void Awake() => _currentMoneyCount = gameConfig.InitialMoney;

        private void ChangeMoneyCount(int count)
        {
            _currentMoneyCount += count;
            _tempEquippedMoneyCount += count;
            textCurrentMoney.text = _currentMoneyCount.ToString();
            progressbar.fillAmount = _currentMoneyCount / gameConfig.AllModels.Max(m => m.MoneyToSwap);

            if (count > 0)
            {
                textCurrentMoneyChanges.color = colorPlus;
                effects.PlayMoneyPlus();
            }
            else
            {
                textCurrentMoneyChanges.color = colorMinus;
                effects.PlayMoneyMinus();
            }

            textCurrentMoneyChanges.text = _tempEquippedMoneyCount.ToString();
            currentMoneyChanges.DOKill();

            currentMoneyChanges.DOFade(1f, 1f)
                .OnComplete(HideCurrentChanges);
        
            currentMoneyUp.DOShakeScale(0.5f, Vector3.one, 5);
            EventsManager.OnMoneyChanged?.Invoke(_currentMoneyCount);
        }

        private void HideCurrentChanges()
        {
            currentMoneyChanges.DOFade(0, 1f);
            _tempEquippedMoneyCount = 0;
        }
    
        private void OnTriggerEnter(Collider other)
        {
            int? moneyChange = GetMoneyChangeFromTag(other.tag);
            if (moneyChange.HasValue)
            {
                Destroy(other.gameObject);
                ChangeMoneyCount(moneyChange.Value);
            }
        }

        private int? GetMoneyChangeFromTag(string tag)
        {
            return tag switch
            {
                "Bottle" => GetMoneyChangeCount("Bottle"),
                "Bills" => GetMoneyChangeCount("Bills"),
                "GoodChoice" => GetMoneyChangeCount("GoodChoice"),
                "BadChoice" => GetMoneyChangeCount("BadChoice"),
                _ => null
            };
        }

        private int GetMoneyChangeCount(string itemName)
        {
            return gameConfig.AllMoneyItems.FirstOrDefault(b => b.Name == itemName)?.MoneyChangeCount ?? 0;
        }
    }
}