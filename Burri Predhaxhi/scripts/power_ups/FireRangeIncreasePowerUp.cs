

public partial class FireRangeIncreasePowerUp : PowerUp
{

	public override void ApplyPowerUp(Player player)
	{
		player.nrOfRangeIncreasePowerUps += 1;
	}
}
