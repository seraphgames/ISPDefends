using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SUPPORTER
{
    PondOfWater,
    MineOnTheRoad,
}
public class Supporter : MonoBehaviour
{
    public SUPPORTER eSupporter;
    private float fTimelife = 30f;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Enemy>())
        {
            if (eSupporter == SUPPORTER.MineOnTheRoad)
            {
                TheSkillManager.Instance.PostPowerUpEvent(TheEnumManager.POWER_UP.mine_on_road, transform.position);
                //eff
                GameObject _eff = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.Explosion_of_mine).GetItem();
                if (_eff)
                {
                    _eff.transform.position = transform.position;
                    _eff.SetActive(true);
                }

                TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.explosion_mine_on_road);//sound
                DestroyThis();
            }
            else if (eSupporter == SUPPORTER.PondOfWater)
            {
                other.GetComponent<Enemy>().HitPondOfPoison();
            }
        }
    }


    private void OnEnable()
    {
        switch (eSupporter)
        {
            case SUPPORTER.PondOfWater:
                fTimelife = 10f;
                Invoke("DestroyThis", fTimelife);
                break;
            case SUPPORTER.MineOnTheRoad:
                break;

        }
    }


    private void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
