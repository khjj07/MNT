using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    public Player player;
    public GameObject Armskin;
    public GameObject Elbow;
    public GameObject Bow;
    public string type;
    public void Move()
    {
        if(player.focus)
        {
            Vector3 mPosition = Input.mousePosition; //마우스 좌표 저장
            Vector3 oPosition = transform.position; //게임 오브젝트 좌표 저장
            mPosition.z = oPosition.z - Camera.main.transform.position.z;
            Vector3 target = Camera.main.ScreenToWorldPoint(mPosition);
            float dy = target.y - oPosition.y;
            float dx = target.x - oPosition.x;
            float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
           
            if (target.x > oPosition.x)
            {
                player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree);
                //Armskin.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            { 
                player.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                transform.rotation = Quaternion.Euler(180f, 0f, rotateDegree*-1);
                //Armskin.GetComponent<SpriteRenderer>().flipX = true;
            }
            if (transform.rotation.z > -50 && transform.rotation.z > 0)
            {
                Armskin.GetComponent<SpriteRenderer>().sortingLayerName = "6";
                if (Bow)
                    Bow.GetComponent<SpriteRenderer>().sortingLayerName = "6";
            }
            else
            {
                if (Bow)
                    Bow.GetComponent<SpriteRenderer>().sortingLayerName = "6";
                if (type == "left")
                    Armskin.GetComponent<SpriteRenderer>().sortingLayerName = "1";
                else if (type == "left")
                    Armskin.GetComponent<SpriteRenderer>().sortingLayerName = "6";
            }
        }
    }
}
