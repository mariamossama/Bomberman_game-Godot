using Godot;

public abstract partial class PowerUp : Area2D, IDestroyable{
	
	public abstract void ApplyPowerUp(Player player);

	public void Destroy()
	{
		QueueFree();
	}
	
	private void OnBodyEntered(Node2D body)
	{
		if (body is Player){
			ApplyPowerUp((Player) body);
			QueueFree();
		}
	}

}

