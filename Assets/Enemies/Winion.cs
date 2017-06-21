using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winion : Enemy
{

	[SerializeField]
	WinLoseManager winManager;


	protected override void Start()
	{
		winManager = FindObjectOfType<WinLoseManager>();
		winManager.Win();
	}

	protected override void Update()
	{
		distanceTraversed += speed * Time.deltaTime;

	}

	protected override void OnTriggerEnter2D(Collider2D collider)
	{
		Projectile proj = collider.gameObject.GetComponent<Projectile>();
		if (proj != null) {
			TakeDamage(0.0001f);
		}
	}

}
