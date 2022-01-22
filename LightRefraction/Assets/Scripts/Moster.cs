using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay {
    public class Moster : Unit {
        // 徘徊移动速度和追击移动速度
        [SerializeField, Label("徘徊速度")]
        float normalMoveSpeed = 5F;

        [SerializeField, Label("追击速度")]
        float angryMoveSpeed = 10F;
        [SerializeField, Label("触发追击距离")]
        float viewDistance = 3F;

        // 徘徊地点
        [SerializeField]
        Vector3 startPatrolPosition;

        [SerializeField]
        Vector3 endPatrolPosition;

        // 是否在向终点走
        bool toEnd = true;
        // 是否处于追击状态
        bool isAngry = false;

        void Start() {
            this.transform.position = startPatrolPosition;
        }

        void move(Vector3 dst) {
            this.transform.right = (dst - this.transform.position).normalized;
            if(transform.right.z != 0) 
                print("?");
            print("=========================");
            print(transform.right);
            print(transform.position);
            this.transform.position += this.transform.right * getSpeed() * Time.deltaTime;
        }

        float getSpeed() {
            if(isAngry) 
                return angryMoveSpeed;
            return normalMoveSpeed;
        }

        bool checkShouldChange(Vector3 dst) {
            if((this.transform.position - dst).magnitude < 1) 
                return true;
            if(Vector3.Dot(dst - this.transform.position, this.transform.right) < 0) 
                return true;
            return false;
        }

        void Update() {
            if(isAngry) {
                if(!seePlayer()) {
                    isAngry = false;
                    return;
                }
                move(GameManager.Player.transform.position);
            }
            else {
                if(seePlayer()) {
                    isAngry = true;
                    return;
                }
                if(toEnd) {
                    if(!checkShouldChange(endPatrolPosition)) {
                        move(endPatrolPosition);
                    }
                    else {
                        toEnd = false;
                        move(startPatrolPosition);
                    }
                }
                else {
                    if(!checkShouldChange(startPatrolPosition)) {
                        move(startPatrolPosition);
                    }
                    else {
                        toEnd = true;
                        move(endPatrolPosition);
                    }
                }
            }
        }

        /* 是否能看到玩家 */
        bool seePlayer() {
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, this.transform.right, (this.transform.position - GameManager.Player.transform.position).magnitude);
            if(hit.collider != null && hit.collider.isTrigger) {
                return false;
            }
            return (this.transform.position - GameManager.Player.transform.position).magnitude < viewDistance && Vector3.Dot((GameManager.Player.transform.position - this.transform.position), this.transform.right) > 0;
        }

        public void destory() {
            Destroy(this.gameObject);
        }
    }
}