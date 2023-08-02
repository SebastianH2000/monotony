using UnityEngine;

namespace RyansNamespace {
    public class MonsterSpawner : MonoBehaviour
    {
        [SerializeField] private float timeBetweenPotentialSpawns;
        [Range(0f, 1f)]
        [SerializeField] private float chanceToSpawn;
        [SerializeField] private GameObject monsterPrefab;
        private float timer;

        private Camera mainCamera;
        private Vector2 screenBoundsMin;
        private Vector2 screenBoundsMax;

        // Start is called before the first frame update
        void Start()
        {
            mainCamera = Camera.main;

            screenBoundsMin = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, -mainCamera.transform.position.z));
            screenBoundsMax = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, -mainCamera.transform.position.z));
        }

        // Update is called once per frame
        void Update()
        {
            if (timer <= 0)
            {
                if (Random.Range(0f, 1f) <= chanceToSpawn)
                {
                    SpawnMonster();
                }

                timer = timeBetweenPotentialSpawns;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }

        private void SpawnMonster() {
            Vector2 spawnPosition = new Vector2(Random.Range(screenBoundsMin.x, screenBoundsMax.x), Random.Range(screenBoundsMin.y, screenBoundsMax.y));
            Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
