using UnityEngine;
using UnityEngine.UI;

namespace AbstractGame.ThirdPerson
{
    public class HUD : MonoBehaviour
    {
        public static Text Kills;

        public static Image MagicOn;
        public static Image MagicBG;

        [SerializeField] Color32 _colorMagicOn;
        [SerializeField] Color32 _colorMagicOff;

        [SerializeField] Material _materialMagicOn;
        [SerializeField] Material _materialMagicOff;

        static Color32 colorMagicOn;
        static Color32 colorMagicOff;

        static Material materialMagicOn;
        static Material materialMagicOff;


        void Awake()
        {
            Kills = GameObject.Find("Kills").GetComponent<Text>();

            MagicOn = GameObject.Find("MagicOn").GetComponent<Image>();
            MagicBG = GameObject.Find("MagicBG").GetComponent<Image>();

            colorMagicOn = _colorMagicOn;
            colorMagicOff = _colorMagicOff;
            materialMagicOn = _materialMagicOn;
            materialMagicOff = _materialMagicOff;

            MagicOn.color = colorMagicOff;
            MagicOn.material = materialMagicOff;

            Kills.text = GameMaster.enemiesKilled.ToString();
        }

        public static void SetMagicButtonAvailable(bool available)
        {
            if (available)
            {
                MagicOn.material = materialMagicOn;
                MagicOn.color = colorMagicOn;
            }
            else
            {
                MagicOn.color = colorMagicOff;
                MagicOn.material = materialMagicOff;
            }
        }
    }
}