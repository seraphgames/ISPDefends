using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMove : MonoBehaviour
{

    private Vector2 vStartPos;
    private Vector2 vTargetPos;
    public float fSpeed;
    private Vector2 vCurrentPos;
    private bool bPlay = false;
    private Transform m_transform;

    private void Awake()
    {
        m_transform = transform;
        vCurrentPos = m_transform.position;

    }


    // Update is called once per frame
    void Update()
    {
        if (!bPlay) return;
        vCurrentPos = Vector2.MoveTowards(vCurrentPos, vTargetPos, Time.deltaTime * fSpeed);
        if (vCurrentPos == vTargetPos)
        {
            GameObject _eff = TheObjPoolingManager.Instance.GetObj(TheEnumManager.ITEMS_POOLING.Explosion_of_mine).GetItem();
            if (_eff)
            {
                _eff.transform.position = vCurrentPos;
                _eff.transform.localScale = Vector3.one * 1.0f;
                _eff.SetActive(true);
            }

            TheSkillManager.Instance.PostPowerUpEvent(TheEnumManager.POWER_UP.fire_of_lord, vCurrentPos);
            gameObject.SetActive(false);
            TheSound.Instance.PlaySoundInGame(TheSound.SOUND_IN_GAME.explosion);//sound

        }

        m_transform.position = vCurrentPos;
    }

    public void Play(Vector2 _targetPos)
    {
        vTargetPos = _targetPos;
        vStartPos = new Vector2(_targetPos.x + 5, _targetPos.y + 8.0f);
        m_transform.position = vStartPos;

        //Euler
        float _delta = Mathf.Rad2Deg * Mathf.Atan((vStartPos.y - vTargetPos.y) / (vStartPos.x - vTargetPos.x));
        m_transform.eulerAngles = new Vector3(0, 0, _delta - 90);


        vCurrentPos = vStartPos;
        bPlay = true;
    }

}
