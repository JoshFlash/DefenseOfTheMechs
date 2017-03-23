using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseTower : MonoBehaviour {

	public float fireRate, fireRateBoost;       //in shots per second
	public float towerRange, towerRangeBoost;
	public float projectileThrust, projectileDamage;
	public int projectileMultiHit = 1;

	private int alphaUpgradeLevel, betaUpgradeLevel = 0;
	private int _enemiesSpawned;
	private bool hasEnemyTarget;
	private bool isFiringAtEnemy;

	private enum TargetPriority { FIRST, LAST, STRONG, FAST };

	private SpriteRenderer spriteRend;
	private GameObject towerRadial;
	private List<Enemy> targetableEnemies;
	private Enemy enemyTarget;

	[SerializeField] private float gunLength, gunOffset;
	[SerializeField] private int maxAlphaUpgradeLevel, maxBetaUpgradeLevel;
	[SerializeField] private TargetPriority targetPriority;
	[SerializeField] private Projectile projectilePrefab;
	[SerializeField] private List<Sprite> upgradeSprites;

	private void Awake() {
		spriteRend = GetComponent<SpriteRenderer>();
		towerRadial = transform.GetChild(0).gameObject;
		tag = "owned tower";
	}

	private void Start() {
		spriteRend.sortingLayerName = "towers";
		enemyTarget = null;
		towerRadial.SetActive(false);
	}

	void Update() {

		TargetEnemy();
		if (enemyTarget != null) LookAtEnemy();
		if (targetableEnemies.Count == 0) ClearEnemyTarget();
		ShootAtEnemy();
		ShowContextIfActive();

	}

	void OnMouseDown() {
		UserController.selectedTower = this.gameObject;
	}

	void ShowContextIfActive() {
		if (UserController.selectedTower == this.gameObject) {
			towerRadial.SetActive(true);
		}
		else { towerRadial.SetActive(false); }
	}

	void ClearEnemyTarget() {
		enemyTarget = null;
		hasEnemyTarget = false;
	}

	void LookAtEnemy() {
		transform.up = enemyTarget.transform.position - transform.position;
	}

	void TargetEnemy() {

		if (_enemiesSpawned != EnemySpawner.enemiesSpawned) {
			EnemySpawner.ClearNullEnemies();
			_enemiesSpawned = EnemySpawner.enemiesSpawned;
		}

		targetableEnemies = new List<Enemy>();

		foreach (Enemy enemy in EnemySpawner.allEnemies) {
			if (enemy != null && Vector2.Distance(enemy.transform.position, this.transform.position) < towerRange) {
				targetableEnemies.Add(enemy);
			}
		}

		SortEnemiesByPriority(targetPriority);

		if (targetableEnemies.Count > 0) {
			enemyTarget = targetableEnemies[0];
			hasEnemyTarget = true;
		}
	}

	static int SortByTraversed(Enemy e1, Enemy e2) {
		return e1.distanceTraversed.CompareTo(e2.distanceTraversed);
	}

	static int SortByHealth(Enemy e1, Enemy e2) {
		return e1.health.CompareTo(e2.health);
	}

	static int SortBySpeed(Enemy e1, Enemy e2) {
		return e1.speed.CompareTo(e2.speed);
	}

	void SortEnemiesByPriority(TargetPriority targetPriority) {
		if (targetPriority == TargetPriority.FIRST) {
			targetableEnemies.Sort(SortByTraversed);
			targetableEnemies.Reverse();
		}
		if (targetPriority == TargetPriority.LAST) {
			targetableEnemies.Sort(SortByTraversed);
		}
		if (targetPriority == TargetPriority.STRONG) {
			targetableEnemies.Sort(SortByHealth);
			targetableEnemies.Reverse();
		}
		if (targetPriority == TargetPriority.FAST) {
			targetableEnemies.Sort(SortBySpeed);
			targetableEnemies.Reverse();
		}
	}

	public void ChangeTargetPriorty(string priority) {
		if		(priority == "FIRST")	{ targetPriority = TargetPriority.FIRST; } 
		else if (priority == "LAST")	{ targetPriority = TargetPriority.LAST; } 
		else if (priority == "STRONG")	{ targetPriority = TargetPriority.STRONG; } 
		else if (priority == "FAST")	{ targetPriority = TargetPriority.FAST; }
	}

	public virtual void ShootAtEnemy() {
		if (hasEnemyTarget && !isFiringAtEnemy) {
			StartCoroutine(FireProjectile(projectilePrefab, 1 / (1.4f*fireRate)));
			isFiringAtEnemy = true;
		}
	}

	private IEnumerator FireProjectile(Projectile proj, float delay) {
		yield return new WaitForSeconds(delay);
		while (hasEnemyTarget) {
			proj = Instantiate(projectilePrefab, this.transform.position + this.transform.right * gunOffset + this.transform.up * gunLength , this.transform.rotation);
			InitProjectile(proj);
			delay = 1 / fireRate;
			yield return new WaitForSeconds(delay);
		}
		isFiringAtEnemy = false;
	}

	public void InitProjectile(Projectile proj) {
		Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
		rb.AddForce(transform.up * projectileThrust);
		proj.damage = this.projectileDamage;
		proj.range = this.towerRange + proj.extraRange;
		proj.multiHit = this.projectileMultiHit;
		proj.speed = ( projectileThrust / rb.mass ) * Time.fixedDeltaTime;
	}

	//upgrades on aplha path for tower - varies by tower type
	public virtual void UpgradeAlphaPath() {
		if (alphaUpgradeLevel < maxAlphaUpgradeLevel) {
			switch (alphaUpgradeLevel) {
				case 0:
					fireRate += fireRateBoost;
					projectileThrust *= 1.2f;
					break;
				case 1:

					maxBetaUpgradeLevel = 1;
					break;
				case 2:

					break;
			}
			alphaUpgradeLevel++;
			SetTowerSprite();
		}
	}

	//upgrades on beta path for tower - varies by tower type
	public virtual void UpgradeBetaPath() {
		if (betaUpgradeLevel < maxBetaUpgradeLevel) {
			switch (betaUpgradeLevel) {
				case 0:
					towerRange += towerRangeBoost;
					GetComponentInChildren<TowerRadial>().UpgradeSight();
					break;
				case 1:

					maxAlphaUpgradeLevel = 1;
					break;
				case 2:

					break;
			}
			betaUpgradeLevel++;
			SetTowerSprite();
		}
	}

	public virtual void SetTowerSprite() {
		if (alphaUpgradeLevel == 0 ) {
			spriteRend.sprite = upgradeSprites[betaUpgradeLevel];
		}
		if (alphaUpgradeLevel == 1) {
			spriteRend.sprite = upgradeSprites[betaUpgradeLevel + 4];
		}
		if (alphaUpgradeLevel == 2) {
			spriteRend.sprite = upgradeSprites[8 + betaUpgradeLevel];
		}
		if (alphaUpgradeLevel == 3) {
			spriteRend.sprite = upgradeSprites[10 + betaUpgradeLevel];
		}


		/// <summary>
		/// Below is the sprite matrix for the List<Sprite> 'upgradeSprites' of the base tower class
		/// (numbers in boxes correnspond to List index for appropriate sprite given alpa-beta levels)
		/// 
		///		| beta	|		|		|		|	
		///	alfa|	0	|	1	|	2	|	3	|	
		/// ---------------------------------------
		///		|		|		|		|		|	
		///	0	|	0	|	1	|	2	|	3	|	
		///		|		|		|		|		|	
		///	---------------------------------------
		///		|		|		|		|		|	
		///	1	|	4	|	5	|	6	|	7	|	
		///		|		|		|		|		|	
		///	---------------------------------------
		///		|		|		|		|		|	
		///	2	|	8	|	9	|		|		|	
		///		|		|		|		|		|	
		/// ---------------------------------------
		/// 	|		|		|		|		|	
		///	3	|	10	|	11	|		|		|	
		///		|		|		|		|		|	
		/// ---------------------------------------
		/// 
		/// 
		/// </summary> 

	}
}