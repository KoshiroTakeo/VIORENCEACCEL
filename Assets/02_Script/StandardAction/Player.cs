using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;



namespace StandardAction
{
    
    public class Player : Character
    {
        GameManager gameManager;
        InputAction move, attack, jump;
        



        private void Start()
        {
            gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

            InputSetting();

            bulletSpeed = speed * 10;
        }
    


        private void Update()
        {
            // 移動
            Move(move.ReadValue<Vector2>(), jump.triggered);
            
            // 攻撃
            Fire(mazule, bulletSpeed, bullet, gameObject.tag, attack.triggered);

            // ゲームオーバー判定
            gameManager.GameOverPlayer(hp);
        }

        // 入力設定
        void InputSetting()
        {
            //move = GetComponent<PlayerInput>().currentActionMap["Move"];
            //attack = GetComponent<PlayerInput>().currentActionMap["Attack"];
            //jump = GetComponent<PlayerInput>().currentActionMap["Jump"];
        }


        


    }

   

    
}

