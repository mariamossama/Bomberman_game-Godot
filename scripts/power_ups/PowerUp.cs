using Godot;

public abstract partial class PowerUp : Area2D, IDestroyable{
	
	public abstract void ApplyPowerUp(Player player);

	public void Destroy()
	{
		QueueFree();
	}
	
	private void OnBodyEntered(Node2D body)
	{
		GD.Print("body entered");
		if (body is Player){
			GD.Print("it's player entered");
			ApplyPowerUp((Player) body);
			QueueFree();
		}
	}
	
		


}

