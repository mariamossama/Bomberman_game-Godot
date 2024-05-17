using System;
using Godot;

public partial class BombIncreasePowerUp : PowerUp
{

	public override void ApplyPowerUp(Player player)
	{
		player.maxBombCount++;
	}


}
