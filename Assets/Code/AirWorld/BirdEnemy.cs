﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdEnemy : MonoBehaviour {

	// Variáveis que controlam a movimentação do inimigo
	public float speed = 1.5f;
	public int direction = -1;
	public bool followPlayer = false;
	public Rigidbody2D enemyRigidbody;
	public Transform enemyTransform;
	public Transform target;

	// Variáveis que de controle da vida do inimigo
	public bool enemyIsAlive = true;
	public float enemyLife = 10f;

	void Start () {
		enemyRigidbody = GetComponent<Rigidbody2D> ();
		enemyTransform = GetComponent<Transform> ();

		// Definindo o jogador como alvo
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update () {
		
		if(enemyLife <= 0){
			EnemyDead ();
		}

		if(enemyIsAlive){
			if(followPlayer){
				enemyTransform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

			}
			else{
				enemyRigidbody.velocity = new Vector2 (speed * direction, enemyRigidbody.velocity.y);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if(coll.gameObject.CompareTag("Player")){
			followPlayer = true;
		}
		if(coll.CompareTag("EnemyArea")){
			direction = -direction;
			Flip ();
		}
	}

	void OnTriggerExit2D(Collider2D coll){
		if(coll.gameObject.CompareTag("Player")){
			followPlayer = false;
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		if(coll.gameObject.CompareTag("Player")){
			Atack ();
		}
	}

	void Flip(){
		Vector3 scale = enemyTransform.localScale;
		scale.x *= -1;
		enemyTransform.localScale = scale;
	}

	void Atack(){

	}

	void EnemyDead (){
		enemyIsAlive = false;
		Destroy (gameObject);
	}
}
