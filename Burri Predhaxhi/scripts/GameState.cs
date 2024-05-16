using Godot;
using Godot.Collections;

public class GameState {
    public GameState(){
        RayCastIgnores = new();
        score = 0;
    }
    public Array<CollisionObject2D> RayCastIgnores {get;set;}
    private int score {get;set;}

    public void IncreaseScore(int scoreToAdd) {
        score += scoreToAdd;
    }
    public int GetScore() {
        return score;
    }
} 

public class GameStateSingleton {
    private static GameState GameState;

    public static GameState FetchGameState(){
        GameState ??= new ();
        return GameState;
    }
}