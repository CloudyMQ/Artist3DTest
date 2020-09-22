using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController cc;
    Animator anim;
    public float speed = 2f; //скорость ходьбы
    
    //Параметры геймплея персонажа
    private float gravityForce;//гравитация
    private Vector3 moveVector;//направление движения

    //Ссылки на компоненты
    private CharacterController ch_controller;
    private  Animator ch_animator;
    private MobileController mController;

    void Start()
    {
        ch_controller = GetComponent<CharacterController>();
        ch_animator = GetComponent<Animator>();
        mController = GameObject.FindGameObjectWithTag("Joystick").GetComponent<MobileController>();
    }

    void Update()
    {
        CharacterMove();
        GamingGravity();
    }

    //Метод перемещения персонажа
    private void CharacterMove()
    {
        //Перемещение по поверхности
        moveVector = Vector3.zero;
        moveVector.x = mController.Horizontal() * speed;
        moveVector.z = mController.Vertical() * speed;
        
        //Анимация передвижения персонажа
        if(moveVector.x !=0 || moveVector.z!=0) ch_animator.SetBool("walk",true);
        else ch_animator.SetBool("walk",false);

        //Поворот персонажа в сторону направления перемещения
        if(Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, speed, 0.0f);
            transform.rotation = Quaternion.LookRotation(direct);
        }

        moveVector.y = gravityForce;
        ch_controller.Move(moveVector * Time.deltaTime);
    }

    private void GamingGravity()
    {
        if(!ch_controller.isGrounded) gravityForce -= 20f * Time.deltaTime;
        else gravityForce = -1f;
        
    }
}