using System;
using UnityEngine;

public abstract class Enemy
{
	private float _health;
	private float _speed;
	private float _dammage;
	private EnemyType _type;
	private Vector3 _position;

	public Enemy()
	{
		
	}

	public abstract void TakeDamage(float amount);

	public abstract void ApplyEffect(EffectType effect);

	public abstract void Move();
}

