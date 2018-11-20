using UnityEngine;
using UnityEngine.SceneManagement;

namespace AbstractGame.ThirdPerson
{
    public class GameMaster : MonoBehaviour
    {
        public static GameObject Player;

        public static GameObject bulletFX;
        public static GameObject enemyFX;
        public static GameObject playerFX;

        public static AudioSource playerSong;

        public static ParticleSystem powerFX;

        public static int idPool_bullet;
        public static int idPool_enemies = 1;
        public static int idPool_bulletFX = 2;
        public static int idPool_enemyDeathFX = 3;
        public static int idPool_playerDeathFX = 4;

        public static int enemiesKilled;

        public GameObject _author;
        public static GameObject author;

        WeaponController weaponController;

        void Awake()
        {
            Player = GameObject.FindWithTag("Player");

            weaponController = Player.GetComponentInChildren<WeaponController>();

            bulletFX = Resources.Load("FX/BulletCollisionFX") as GameObject;
            enemyFX = Resources.Load("FX/EnemyDeathFX") as GameObject;

            playerFX = GameObject.Find("PlayerDeathFX");
            playerSong = playerFX.GetComponent<AudioSource>();

            powerFX = GameObject.Find("PowerFX").GetComponent<ParticleSystem>();

            author = _author;

            author.SetActive(false);
            ObjectsPool.CreatePools(5);
        }

        public void Shoot(bool shoot)
        {
            weaponController.Shoot(shoot);
        }

        public void UsePower()
        {
            weaponController.UsePower();
        }

        public void PlayAgain()
        {
            ReloadScene();
        }

        public static void ReloadScene()
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        public static void CountKill()
        {
            enemiesKilled += 1;
            HUD.Kills.text = enemiesKilled.ToString();
        }

        public static void GameOverScreen()
        {
            author.SetActive(true);
            LeanTween.value(0, 1, 3).setOnComplete(() => { ReloadScene(); });
        }
    }
}