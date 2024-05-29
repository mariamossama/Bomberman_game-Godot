using Godot;
using System;

public partial class InvincibilityPowerUp : PowerUp
{

	public override void ApplyPowerUp(Player player)
	{
		player.GetInvincible(5);
	}


}

