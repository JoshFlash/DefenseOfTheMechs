using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperTower : DefenseTower 
{

	public GameObject gunSparks;

	private float critChance = 0;
	private float stunTime = 0f;

	protected override void Start() {
		base.Start();
		towerRadial.GetComponent<TowerRadial>().SetToOriginalScale();
	}

	protected override void FireProjectile() {
		Vector3 gunBarrellEnd = this.transform.position + this.transform.right * gunOffset + this.transform.up * gunLength;
		if (enemyTarget) {
			enemyTarget.TakeDamage(this.projectileDamage);
			RollCriticalStrike();
			StunTargetOnHit();
			GameObject spark = Instantiate(gunSparks, gunBarrellEnd, this.transform.rotation);
			Destroy(spark, 0.9f);
		}
	}

	private void RollCriticalStrike() {
		if (critChance > 0) {
			float critRoll = Random.Range(0f, .99f);
			if (critRoll < critChance) {
				enemyTarget.TakeDamage(this.projectileDamage * 1.03f);
			}
		}
	}

	private void StunTargetOnHit() {
		if (stunTime > 0) {
			enemyTarget.StartCoroutine(enemyTarget.GetStunned(stunTime));
		}
	}

	//upgrades on aplha path for tower - varies by tower type
	public override void UpgradeAlphaPath() {
		if (alphaUpgradeLevel < maxAlphaUpgradeLevel) {
			switch (alphaUpgradeLevel) {
				case 0:
					fireRate += fireRateBoost;
					gunWarmTime *= 0.9f;
					MoneyManager.SpendInLevelCash(alphaUpgradeCost);
					sellValue += (int)( 0.7 * alphaUpgradeCost );
					alphaUpgradeCost = alphaUpgradeCosts[alphaUpgradeLevel + 1];
					break;
				case 1:
					critChance = 0.33f;
					maxBetaUpgradeLevel = 1;
					MoneyManager.SpendInLevelCash(alphaUpgradeCost);
					sellValue += (int)( 0.7 * alphaUpgradeCost );
					alphaUpgradeCost = alphaUpgradeCosts[alphaUpgradeLevel + 1];
					break;
				case 2:
					critChance = 1f;
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
	public override void UpgradeBetaPath() {
		if (betaUpgradeLevel < maxBetaUpgradeLevel) {
			switch (betaUpgradeLevel) {
				case 0:
					projectileDamage = 1.6f;
					MoneyManager.SpendInLevelCash(betaUpgradeCost);
					sellValue += (int)( 0.7 * betaUpgradeCost );
					betaUpgradeCost = betaUpgradeCosts[betaUpgradeLevel + 1];
					break;
				case 1:
					stunTime = 1.1f;
					maxAlphaUpgradeLevel = 1;
					MoneyManager.SpendInLevelCash(betaUpgradeCost);
					sellValue += (int)( 0.7 * betaUpgradeCost );
					betaUpgradeCost = betaUpgradeCosts[betaUpgradeLevel + 1];
					break;
				case 2:
					stunTime = 2.2f;
					projectileDamage = 2.2f;
					MoneyManager.SpendInLevelCash(betaUpgradeCost);
					sellValue += (int)( 0.7 * betaUpgradeCost );
					break;
				default: break;
			}
			betaUpgradeLevel++;
			SetTowerSprite();
		}
	}

	protected override void SetTowerSprite() {
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
		/// Below is the sprite matrix for the List<Sprite> 'upgradeSprites' of the sniper tower class
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

	public override string AlphaUpgradeText(int alphaLevel) {
		switch (alphaLevel) {
			case 0:
				return "Increases Fire Rate of this Sniper Mech - " + alphaUpgradeCost + " Ƀ";
			case 1:
				if (betaUpgradeLevel >= 2) {
					return "UPGRADE UNAVAILABLE";
				}
				return "Occasionally critically strikes for extra damage - " + alphaUpgradeCost + " Ƀ";
			case 2:
				if (betaUpgradeLevel >= 2) {
					return "UPGRADE UNAVAILABLE";
				}
				return "Marksmech: Sniper Mech will always critically strike! - " + alphaUpgradeCost + " Ƀ";
			case 3:
				return "NO FURTHER UPGRADES AVAILABLE";
			default: return "ERr0R %<\0>";
		}
	}

	public override string BetaUpgradeText(int betaLevel) {
		switch (betaLevel) {
			case 0:
				return "Slightly increases Damage of this Sniper Mech - " + betaUpgradeCost + " Ƀ";
			case 1:
				if (alphaUpgradeLevel >= 2) {
					return "UPGRADE UNAVAILABLE";
				}
				return "Shots now Stun enemies - " + betaUpgradeCost + " Ƀ";
			case 2:
				if (alphaUpgradeLevel >= 2) {
					return "UPGRADE UNAVAILABLE";
				}
				return "Full Metal Mech: More Damage, longer Stuns - " + betaUpgradeCost + " Ƀ";
			case 3:
				return "NO FURTHER UPGRADES AVAILABLE";
			default: return "ERr0R %<\0>";
		}
	}
}
