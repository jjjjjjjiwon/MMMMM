#define ENABLE_CHEATS  // "치트 활성화" 깃발 세움
#define BETA_VERSION   // "베타 버전" 깃발 세움
#define USE_OLD_API



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcecessExample : MonoBehaviour
{
    // if
    void Start()
    {
#if ENABLE_CHEATS
        Debug.Log("치트 모드 활성화!");
#endif

#if BETA_VERSION
        Debug.Log("베타 테스트 중입니다");
#endif
    }


    // warning / error
    public class Player
    {
        public void Attack()
        {
#warning "TODO: 공격 애니메이션 추가 필요"

//#error "공부를 위해 일부로 에러를 발생"
        }
    }


    // region / endregion
    public class Playerstate
    {
        #region 체력 관련
        private int health = 100;
        private int maxHealth = 100;

        public void TakeDamage(int damage)
        {
            health -= damage;
        }

        public void Heal(int amount)
        {
            health += amount;
        }
        #endregion

        #region 공격 관련
        private int attackPower = 10;

        public void Attack()
        {
            // 공격 로직
        }
        #endregion
    }
    

 }



