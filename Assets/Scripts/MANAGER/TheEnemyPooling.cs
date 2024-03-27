using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEnemyPooling : MonoBehaviour
{
    public static TheEnemyPooling Instance;
    private int iTotalKindOfEnemy;

    //ZOMBIE POOL
    [System.Serializable]
    public class ClassUnitPool
    {
        public TheEnumManager.ENEMY eEnemy;
        public List<Enemy> LIST_ENEMY;
        private int iTotalZombie;

        public void Init(int _number)
        {
            LIST_ENEMY = new List<Enemy>();
            GameObject _enemy = null;

            for (int i = 0; i < _number; i++)
            {
                _enemy = Instantiate(TheObjPoolingManager.Instance.GetEnemyPrefab(eEnemy), Vector2.one * 1000, Quaternion.identity);
                _enemy.SetActive(false);
                LIST_ENEMY.Add(_enemy.GetComponent<Enemy>());
            }
            iTotalZombie = LIST_ENEMY.Count;
        }

        public Enemy GetEnemy()
        {
            for (int i = 0; i < iTotalZombie; i++)
            {
                if (!LIST_ENEMY[i].gameObject.activeInHierarchy)
                {
                    return LIST_ENEMY[i];
                }
            }


            //more
            GameObject _enemy = Instantiate(TheObjPoolingManager.Instance.GetEnemyPrefab(eEnemy), Vector2.one * 1000, Quaternion.identity);
            _enemy.SetActive(false);
            LIST_ENEMY.Add(_enemy.GetComponent<Enemy>());
            iTotalZombie++;
            return _enemy.GetComponent<Enemy>();
        }
    }
    public List<ClassUnitPool> LIST_ENEMY_POOL;
    public ClassUnitPool GetZombiePool(TheEnumManager.ENEMY _enemy)
    {
        for (int i = 0; i < iTotalKindOfEnemy; i++)
        {
            if (_enemy == LIST_ENEMY_POOL[i].eEnemy)
            {
                return LIST_ENEMY_POOL[i];
            }
        }
        return null;
    }



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }



    public void Init(List<TheEnumManager.ENEMY> _list)
    {
        int _total = _list.Count;
        for (int i = 0; i < _total; i++)
        {
            ClassUnitPool _EnemyPool = new ClassUnitPool();
            _EnemyPool.eEnemy = _list[i];
            _EnemyPool.Init(10);
            LIST_ENEMY_POOL.Add(_EnemyPool);
        }

        iTotalKindOfEnemy = LIST_ENEMY_POOL.Count;
    }




    #region ENEMY: FUNTIONS
    [Space(10)]

    public List<Enemy> LIST_ENEMY_IN_GAMEPLAY;
    private void AddToEnemylistGameplay(Enemy _enemy)
    {
        if (!LIST_ENEMY_IN_GAMEPLAY.Contains(_enemy))
            LIST_ENEMY_IN_GAMEPLAY.Add(_enemy);
    }
    private void RemoveEnemyFormListGameplay(Enemy _enemy)
    {
        if (LIST_ENEMY_IN_GAMEPLAY.Contains(_enemy))
            LIST_ENEMY_IN_GAMEPLAY.Remove(_enemy);
    }
    public Enemy FindNearestEnemy(Vector2 _fromPos, float _range, TheEnumManager.ENEMY_KIND eKindOfEnemy = TheEnumManager.ENEMY_KIND.All)
    {

        Enemy _nearertEnemy = null;
        float _tempRange = _range;

        int _total = LIST_ENEMY_IN_GAMEPLAY.Count;
        for (int i = 0; i < _total; i++)
        {

            if (Vector2.Distance(_fromPos, LIST_ENEMY_IN_GAMEPLAY[i].GetCurrentPos()) < _tempRange)
            {
                switch (eKindOfEnemy)
                {
                    case TheEnumManager.ENEMY_KIND.All:
                        _tempRange = Vector2.Distance(_fromPos, LIST_ENEMY_IN_GAMEPLAY[i].GetCurrentPos());

                        if (LIST_ENEMY_IN_GAMEPLAY[i].isInGameplay())
                            _nearertEnemy = LIST_ENEMY_IN_GAMEPLAY[i];
                        break;
                    case TheEnumManager.ENEMY_KIND.Airforce:
                        if (LIST_ENEMY_IN_GAMEPLAY[i].DATA.bIsAirForece)
                        {
                            _tempRange = Vector2.Distance(_fromPos, LIST_ENEMY_IN_GAMEPLAY[i].GetCurrentPos());
                            if (LIST_ENEMY_IN_GAMEPLAY[i].isInGameplay())
                                _nearertEnemy = LIST_ENEMY_IN_GAMEPLAY[i];
                        }
                        break;
                    case TheEnumManager.ENEMY_KIND.Infantry:
                        if (LIST_ENEMY_IN_GAMEPLAY[i].DATA.bIsInfantry)
                        {
                            _tempRange = Vector2.Distance(_fromPos, LIST_ENEMY_IN_GAMEPLAY[i].GetCurrentPos());
                            if (LIST_ENEMY_IN_GAMEPLAY[i].isInGameplay())
                                _nearertEnemy = LIST_ENEMY_IN_GAMEPLAY[i];
                        }
                        break;
                }

            }
        }
        return _nearertEnemy;
    }
    #endregion


    private void OnEnable()
    {
        TheEventManager.OnEnemyIsBorn += AddToEnemylistGameplay;
        TheEventManager.OnEnemyIsDetroyOnRoad += RemoveEnemyFormListGameplay;
        TheEventManager.OnEnemyCompletedRoad += RemoveEnemyFormListGameplay;
    }

    private void OnDisable()
    {
        TheEventManager.OnEnemyIsBorn -= AddToEnemylistGameplay;
        TheEventManager.OnEnemyIsDetroyOnRoad -= RemoveEnemyFormListGameplay;
        TheEventManager.OnEnemyCompletedRoad -= RemoveEnemyFormListGameplay;
    }

}
