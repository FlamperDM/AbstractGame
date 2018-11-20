using UnityEngine;

namespace AbstractGame.ThirdPerson
{
    public class EnergyPlayer : MonoBehaviour
    {
        public float timeLoad;

        public bool available { get; private set; }

        void Start()
        {
            HUD.MagicBG.fillAmount = 1;
            HUD.MagicOn.fillAmount = 0;

            FillMagicButton();
        }

        public void UseMagic()
        {
            if (available)
            {
                available = false;

                HUD.SetMagicButtonAvailable(available);

                FillMagicButton();
            }
        }

        public void HitHealth()
        {
            GameMaster.playerFX.transform.position = transform.position;
            GameMaster.playerFX.GetComponent<ParticleSystem>().Play();
            GameMaster.playerSong.Play();

            StopAllCoroutines();
            gameObject.SetActive(false);
            GameMaster.GameOverScreen();
        }

        void FillMagicButton()
        {
            LeanTween.value(1, 0, timeLoad).setOnUpdate((value) => HUD.MagicBG.fillAmount = value);
            LeanTween.value(0, 1, timeLoad)
                     .setOnUpdate((value) => HUD.MagicOn.fillAmount = value)
                     .setOnComplete(() =>
                     {
                         available = true;
                         HUD.SetMagicButtonAvailable(available);
                     });
        }
    }
}