using Godot;

public class Utils{

	TileMap tileMap;

	public Utils(TileMap tileMap){
		this.tileMap = tileMap;
	}

	public Vector2 convertedCoords(int x,int y){ //overloaded for ease of use 
		return tileMap.ToGlobal(tileMap.MapToLocal(new Vector2I(x, y))); //in case you like using coordinates as ints more
	}

	public Vector2 convertedCoords(Vector2I pos){
		return tileMap.ToGlobal(tileMap.MapToLocal(pos));
	}

	public Vector2I toTwoICoords(Vector2 pos){
		return tileMap.LocalToMap(pos);
	}
	

}
