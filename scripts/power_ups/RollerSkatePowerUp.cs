using Godot;
using System;

public partial class RollerSkatePowerUp : PowerUp
{

	public override void ApplyPowerUp(Player player)
	{
		player.ChangeSpeed(200);
	}


}

