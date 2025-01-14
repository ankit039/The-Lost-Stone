﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMov : MonoBehaviour {

	public Rigidbody2D bossRigidbody;

	// Variáveis que controlam a movimentação do Boss
	public float speed = 0.5f;
	public int direction = -1;
	public bool canWalk = true;

	// Variáveis que de controle da vida do Boss
	public bool bossIsAlive = true;
	public float bossLife = 10f;

	//Variável de criação do segundo Boss
	public Transform secondBossCreation;
	public GameObject secondBoss;
    //Variável do audio
    public AudioClip soundBoss1;
    public AudioClip soundBoss2;

    Animator AnimController;

	void Start () {
		// Torna possível a manipulação das propriedades do componente rigidbody do Boss
		bossRigidbody = GetComponent<Rigidbody2D> ();
        AnimController = GetComponent<Animator>();

        canWalk = false;
	}

	void Update () {
		// Se a vida do Boss chegar a 0, o método responsável por sua morte é chamado
		if(bossLife <= 0){
			BossDead ();
		}
		// Se o Boss estiver vivo e longe do personagem, ele se movimenta para a esquerda e direita
		if(bossIsAlive && canWalk){
           
            bossRigidbody.velocity = new Vector2 (speed * direction, bossRigidbody.velocity.y);
		}
	}

	void OnTriggerExit2D(Collider2D collision){
		// Quando o Boss sai do espaço delimitado de sua movimentação a sua direção é alterada
		if(collision.CompareTag("BossLimit") && bossIsAlive){
			direction = -direction;
            SoundManager.instance.RandomizeSfx(soundBoss1, soundBoss2);
        }
	}

	void OnCollisionEnter2D(Collision2D collision){
		// Quando um tiro atinge o Boss, ele recebe 0.5 de dano
		if(collision.gameObject.CompareTag("Shot")){
        
			bossLife -= 0.5f;
		}
	}

	// Morte do Boss
	void BossDead() {
        if (bossIsAlive)
        {
            AnimController.SetBool("secondForm", true);

            bossRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;          // Congela posição atual
            GetComponent<PolygonCollider2D>().enabled = false;                          // Desliga os colliders
            
            bossIsAlive = false;
        }
    }

	/* Criação do GameObject da segunda forma do Boss. Esse método é chamado quando o Boss na 
	 * primeira forma é morto
	 */
	void SecondBossCreation(){
       
        var cloneSecondBoss = Instantiate(secondBoss, secondBossCreation.position, Quaternion.identity) as GameObject;
	}

    // Impede que o boss comece sua rotina antes de spawnar
    void Spawning()
    {
        canWalk = true;
    }	
}

