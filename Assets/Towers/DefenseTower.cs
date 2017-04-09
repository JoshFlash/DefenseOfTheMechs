using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DefenseTower : MonoBehaviour
{

	public float gunWarmTime;               //time (in seconds) before first shooting at enemies after targeting
	public float fireRate, fireRateBoost;   //in shots per second
	public float towerRange, towerRangeBoost;
	public float projectileThrust, projectileDamage;
	public int projectileMultiHit = 1;
	public int alphaUpgradeLevel = 0;
	public int betaUpgradeLevel = 0;
	public int alphaUpgradeCost, betaUpgradeCost;
	public int[] alphaUpgradeCosts, betaUpgradeCosts;
	public int sellValue;

	public int priorityIndex;

	protected bool doubleShot = false;

	protected bool hasEnemyTarget;
	protected bool isFiringAtEnemy;

	public enum TargetPriority { FIRST, LAST, STRONG, FAST };
	public TargetPriority targetPriority;

	protected SpriteRenderer spriteRend;
	protected GameObject towerRadial;

	[SerializeField]
	protected List<Enemy> targetableEnemies;
	[SerializeField]
	protected Enemy enemyTarget;

	[SerializeField]
	protected float gunLength, gunOffset;
	[SerializeField]
	protected int maxAlphaUpgradeLevel, maxBetaUpgradeLevel;
	[SerializeField]
	protected Projectile projectilePrefab;
	[SerializeField]
	protected List<Sprite> upgradeSprites;

	protected void Awake() {
		spriteRend = GetComponent<SpriteRenderer>();
		towerRadial = transform.GetChild(0).gameObject;
		tag = "owned tower";
	}

	protected virtual void Start() {
		alphaUpgradeCost = alphaUpgradeCosts[0];
		betaUpgradeCost = betaUpgradeCosts[0];
		spriteRend.sortingLayerName = "towers";
		enemyTarget = null;
		towerRadial.SetActive(false);
		priorityIndex = 0;
	}

	protected void Update() {

		TargetEnemy();
		if (enemyTarget != null) LookAtEnemy();
		if (targetableEnemies.Count == 0) ClearEnemyTarget();
		StartShootAtEnemy();
		ShowRadialIfSelected();

	}

	protected void OnMouseDown() {
		UserController.selectedTower = this.gameObject;
	}

	protected void ShowRadialIfSelected() {
		if (UserController.selectedTower == this.gameObject) {
			towerRadial.SetActive(true);
		} else { towerRadial.SetActive(false); }
	}

	protected void ClearEnemyTarget() {
		enemyTarget = null;
		hasEnemyTarget = false;
	}

	protected void LookAtEnemy() {
		transform.up = enemyTarget.transform.position - transform.position;
	}

	void TargetEnemy() {

		EnemySpawner.ClearNullEnemies();

		targetableEnemies = new List<Enemy>();

		foreach (Enemy enemy in EnemySpawner.allEnemies) {
			if (Vector2.Distance(enemy.transform.position, this.transform.position) < towerRange) {
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

	protected void SortEnemiesByPriority(TargetPriority targetPriority) {
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

	public void ChangeTargetPriorty(int priority) {
		if		(priority % 4 == 0)	{ targetPriority = DefenseTower.TargetPriority.FIRST; } 
		else if (priority % 4 == 1) { targetPriority = DefenseTower.TargetPriority.LAST; } 
		else if (priority % 4 == 2) { targetPriority = DefenseTower.TargetPriority.STRONG; } 
		else if (priority % 4 == 3) { targetPriority = DefenseTower.TargetPriority.FAST; }
	}

	protected void StartShootAtEnemy() {
		if (hasEnemyTarget && !isFiringAtEnemy) {
			StartCoroutine(ShootAtEnemy(gunWarmTime));
			isFiringAtEnemy = true;
		}
	}

	protected virtual IEnumerator ShootAtEnemy(float delay) {
		yield return new WaitForSeconds(delay);
		while (hasEnemyTarget) {
			FireProjectile();
			delay = 1 / fireRate;
			if (doubleShot) {
				yield return new WaitForSeconds(0.1f);
				FireProjectile();
			}
			yield return new WaitForSeconds(delay);
		}
		isFiringAtEnemy = false;
	}

	protected virtual void FireProjectile() {
		Vector3 gunBarrellEnd = this.transform.position + this.transform.right * gunOffset + this.transform.up * gunLength;
		Projectile proj = Instantiate(projectilePrefab, gunBarrellEnd, this.transform.rotation);
		InitProjectile(proj);
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
					gunWarmTime *= 0.8f;
					MoneyManager.SpendInLevelCash(alphaUpgradeCost);
					sellValue += (int)( 0.7 * alphaUpgradeCost );
					alphaUpgradeCost = alphaUpgradeCosts[alphaUpgradeLevel+1];
					break;
				case 1:
					fireRate += fireRateBoost;
					maxBetaUpgradeLevel = 1;
					gunWarmTime *= 0.55f;
					MoneyManager.SpendInLevelCash(alphaUpgradeCost);
					sellValue += (int)( 0.7 * alphaUpgradeCost );
					alphaUpgradeCost = alphaUpgradeCosts[alphaUpgradeLevel+1];
					break;
				case 2:
					doubleShot = true;
					MoneyManager.SpendInLevelCash(alphaUpgradeCost);
					sellValue += (int)( 0.7 * alphaUpgradeCost );
					break;
				default: break;
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
					projectileThrust *= 1.5f;
					gunWarmTime *= 0.85f;
					GetComponentInChildren<TowerRadial>().UpgradeSight();
					MoneyManager.SpendInLevelCash(betaUpgradeCost);
					sellValue += (int)( 0.7 * betaUpgradeCost );
					betaUpgradeCost = betaUpgradeCosts[betaUpgradeLevel+1];
					break;
				case 1:
					towerRangeBoost *= 1.42f;
					towerRange += towerRangeBoost;
					gunLength *= 1.8f;
					projectileThrust *= 1.5f;
					GetComponentInChildren<TowerRadial>().UpgradeSight();
					maxAlphaUpgradeLevel = 1;
					transform.localScale = new Vector3(transform.localScale.x * 0.96f, transform.localScale.y * 0.96f, 1);
					MoneyManager.SpendInLevelCash(betaUpgradeCost);
					sellValue += (int)( 0.7 * betaUpgradeCost );
					betaUpgradeCost = betaUpgradeCosts[betaUpgradeLevel + 1];
					break;
				case 2:
					gunWarmTime *= 0.8f;
					projectileThrust *= 1.5f;
					projectileDamage *= 2.02f;
					projectileMultiHit = 2;
					MoneyManager.SpendInLevelCash(betaUpgradeCost);
					sellValue += (int)( 0.7 * betaUpgradeCost );
					break;
				default: break;
			}
			betaUpgradeLevel++;
			SetTowerSprite();
		}
	}

	protected virtual void SetTowerSprite() {
		if (alphaUpgradeLevel == 0) {
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

	public virtual string AlphaUpgradeText(int alphaLevel) {
		switch (alphaLevel) {
			case 0:
				return "Increases Fire Rate of this Mech - " + alphaUpgradeCost + " Ƀ";
			case 1:
				if (betaUpgradeLevel >= 2) {
					return "UPGRADE UNAVAILABLE";
				}
				return "Further increases Fire Rate and reduces Aim Time - " + alphaUpgradeCost + " Ƀ";
			case 2:
				if (betaUpgradeLevel >= 2) {
					return "UPGRADE UNAVAILABLE";
				}
				return "DoubleShot: This Mech will fire two lazers with each shot! - " + alphaUpgradeCost + " Ƀ";
			case 3:
				return "NO FURTHER UPGRADES AVAILABLE";
			default: return "ERr0R %<\0>";
		}
	}

	public virtual string BetaUpgradeText(int betaLevel) {
		switch (betaLevel) {
			case 0:
				return "Increases Range of this Mech - " + betaUpgradeCost + " Ƀ";
			case 1:
				if (alphaUpgradeLevel >= 2) {
					return "UPGRADE UNAVAILABLE";
				}
				return "Greatly increases Range of this mech & lazers fly faster - " + betaUpgradeCost + " Ƀ";
			case 2:
				if (alphaUpgradeLevel >= 2) {
					return "UPGRADE UNAVAILABLE";
				}
				return "Lazer Blazer: Mech's lazers can hit 2 enemies and deal double damage! - " + betaUpgradeCost + " Ƀ";
			case 3:
				return "NO FURTHER UPGRADES AVAILABLE";
			default: return "ERr0R %<\0>";
		}
	}

	public string TargetPriorityText(TargetPriority priority) {
		string text = priority.ToString();
		return "TARGET: \n" + text;

	}

}