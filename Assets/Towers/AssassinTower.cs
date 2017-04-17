using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinTower : DefenseTower 
{

	[SerializeField]
	private float poisonDamage, poisonInterval;
	[SerializeField]
	private float projectileExtraRange;
	[SerializeField]
	private int poisonTicks;

	private float slowAmount;

	// only targets poisoned enemies after second upgrade (either tree) for reasons(TM)
	// note that it still proiritises non-poisoned enemies in this case
	//only the last applied poison will affect the enemy
	private bool targetPoisonedEnemies = false; 

	protected override void Start() {
		base.Start();
	}

	protected override void TargetEnemy() {

		EnemySpawner.ClearNullEnemies();

		targetableEnemies = new List<Enemy>();

		foreach (Enemy enemy in EnemySpawner.allEnemies) {
			if (Vector2.Distance(enemy.transform.position, this.transform.position) < towerRange) {
				if (!enemy.isPoisoned) {
					targetableEnemies.Add(enemy);
				}
			}
		}

		if (targetableEnemies.Count == 0 && targetPoisonedEnemies) {
			foreach (Enemy enemy in EnemySpawner.allEnemies) {
				if (Vector2.Distance(enemy.transform.position, this.transform.position) < towerRange) {
					targetableEnemies.Add(enemy);
				}
			}
		}

		SortEnemiesByPriority(targetPriority);

		if (targetableEnemies.Count > 0) {
			enemyTarget = targetableEnemies[0];
			hasEnemyTarget = true;
		}
	}

	public override void InitProjectile(Projectile proj) {
		base.InitProjectile(proj);
		proj.isPoisonous = true;
		proj.poisonDamage = this.poisonDamage;
		proj.poisonInterval = this.poisonInterval;
		proj.poisonTicks = this.poisonTicks;
		proj.extraRange = this.projectileExtraRange;
		if (slowAmount > 0f) {
			proj.isSlowing = true;
			proj.slowAmount = this.slowAmount;
		}
	}

	 public override void UpgradeAlphaPath() {
		if (alphaUpgradeLevel < maxAlphaUpgradeLevel) {
			switch (alphaUpgradeLevel) {
				case 0:
					poisonDamage *= 1.5f;
					gunWarmTime *= 0.9f;
					MoneyManager.SpendInLevelCash(alphaUpgradeCost);
					sellValue += (int)( 0.7 * alphaUpgradeCost );
					alphaUpgradeCost = alphaUpgradeCosts[alphaUpgradeLevel + 1];
					break;
				case 1:
					targetPoisonedEnemies = true;
					poisonDamage *= 1.34f;
					fireRate += fireRateBoost;
					MoneyManager.SpendInLevelCash(alphaUpgradeCost);
					sellValue += (int)( 0.7 * alphaUpgradeCost );
					alphaUpgradeCost = alphaUpgradeCosts[alphaUpgradeLevel + 1];
					break;
				case 2:
					projectileDamage = poisonTicks*poisonDamage;
					gunWarmTime *= 0.9f;
					gunLength += 0.2f;
					MoneyManager.SpendInLevelCash(alphaUpgradeCost);
					sellValue += (int)( 0.7 * alphaUpgradeCost );
					break;
				default: break;
			}
			alphaUpgradeLevel++;
			SetTowerSprite();
		}
	}

	public override void UpgradeBetaPath() {
		if (betaUpgradeLevel < maxBetaUpgradeLevel) {
			switch (betaUpgradeLevel) {
				case 0:
					slowAmount = 0.3f;
					MoneyManager.SpendInLevelCash(betaUpgradeCost);
					sellValue += (int)( 0.7 * betaUpgradeCost );
					betaUpgradeCost = betaUpgradeCosts[betaUpgradeLevel + 1];
					break;
				case 1:
					targetPoisonedEnemies = true;
					poisonTicks += 3;
					projectileThrust *= 1.8f;
					maxAlphaUpgradeLevel = 1;
					gunLength += 0.2f;
					MoneyManager.SpendInLevelCash(betaUpgradeCost);
					sellValue += (int)( 0.7 * betaUpgradeCost );
					betaUpgradeCost = betaUpgradeCosts[betaUpgradeLevel + 1];
					break;
				case 2:
					poisonTicks += 4;
					poisonInterval *= 0.84f;
					slowAmount = 0.6f;
					MoneyManager.SpendInLevelCash(betaUpgradeCost);
					sellValue += (int)( 0.7 * betaUpgradeCost );
					break;
				default: break;
			}
			betaUpgradeLevel++;
			SetTowerSprite();
		}
	}
	public override string AlphaUpgradeText(int alphaLevel) {
		switch (alphaLevel) {
			case 0:
				return "Increases Poison damage over time of Assassin Mech - " + alphaUpgradeCost + " Ƀ";
			case 1:
				if (betaUpgradeLevel >= 2) {
					return "UPGRADE UNAVAILABLE";
				}
				return "More Poison damage, faster Fire Rate - " + alphaUpgradeCost + " Ƀ";
			case 2:
				if (betaUpgradeLevel >= 2) {
					return "UPGRADE UNAVAILABLE";
				}
				return "Belcher's Bite: Poison now deals 100% additional damage up front!  - " + alphaUpgradeCost + " Ƀ";
			case 3:
				return "NO FURTHER UPGRADES AVAILABLE";
			default: return "ERr0R %<\0>";
		}
	}

	public override string BetaUpgradeText(int betaLevel) {
		switch (betaLevel) {
			case 0:
				return "Poison now also Slows enemy movement - " + betaUpgradeCost + " Ƀ";
			case 1:
				if (alphaUpgradeLevel >= 2) {
					return "UPGRADE UNAVAILABLE";
				}
				return "Assassin Mech's Slowing Poison lasts longer - " + betaUpgradeCost + " Ƀ";
			case 2:
				if (alphaUpgradeLevel >= 2) {
					return "UPGRADE UNAVAILABLE";
				}
				return "A Slow Death: Poison and slow are much stronger and last much longer!- " + betaUpgradeCost + " Ƀ";
			case 3:
				return "NO FURTHER UPGRADES AVAILABLE";
			default: return "ERr0R %<\0>";
		}
	}

}


