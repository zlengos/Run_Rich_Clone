using UnityEngine;

namespace Player
{
    public class PlayerEffects : MonoBehaviour
    {
        [SerializeField] private ParticleSystem moneyPlus;
        [SerializeField] private ParticleSystem moneyPlusBlow;
        [SerializeField] private ParticleSystem moneyMinus;
        [SerializeField] private ParticleSystem dressUp;
        [SerializeField] private ParticleSystem goodChoice;
        [SerializeField] private ParticleSystem badChoice;

        public void PlayMoneyPlus()
        {
            PlayEffect(moneyPlus);
            PlayEffect(moneyPlusBlow);
        }
    
        public void PlayMoneyMinus()
        {
            PlayEffect(moneyMinus);
        }

        public void PlayDressUp()
        {
            PlayEffect(dressUp);
        }
    
        public void PlayGoodChoice()
        {
            PlayEffect(goodChoice);
        }
    
        public void PlayBadChoice()
        {
            PlayEffect(badChoice);
        }

        public void PlayEffect(ParticleSystem effect)
        {
            effect.Play();
        }
    }
}