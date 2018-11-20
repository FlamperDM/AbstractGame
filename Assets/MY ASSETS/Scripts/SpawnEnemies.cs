using System.Collections;
using UnityEngine;
namespace AbstractGame.ThirdPerson
{
    public class SpawnEnemies : MonoBehaviour
    {
        public GameObject enemyPrefab;

        public float spawnEnemyRate = .4f;

        Vector2 margin = Vector2.one * .5f;
        Vector2 screenSize;
        Vector2 positionEnemy;

        public static bool availableSpawn = true;

        void Awake()
        {
            GameMaster.enemiesKilled = 0;

            screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            StartCoroutine(Spawning());
        }

        IEnumerator Spawning()
        {
            while (availableSpawn)
            {
                int side = Random.Range(0, 2) == 0 ? -1 : 1;
                int axis = Random.Range(0, 2);
                if (axis == 0)
                {
                    float width = Random.Range(-screenSize.x, screenSize.x);
                    positionEnemy = new Vector2(width, screenSize.y * side);
                }
                else
                {
                    float height = Random.Range(-screenSize.y, screenSize.y);
                    positionEnemy = new Vector2(screenSize.x * side, height);
                }
                positionEnemy += (Vector2)GameMaster.Player.transform.position + margin * side;
                ObjectsPool.Spawn(enemyPrefab, GameMaster.idPool_enemies, positionEnemy, transform.rotation, transform);
                yield return new WaitForSeconds(spawnEnemyRate);
            }
            yield return null;
        }
    }
}