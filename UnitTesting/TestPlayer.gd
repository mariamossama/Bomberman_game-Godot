extends GdUnitTestSuite


# Test case: Player Initialization

func test_player_initialization():
	var runner := scene_runner("res://Player.tscn")
	assert_bool(runner != null)
# Test case: Player Movement

func test_player_movement_right():
	var runner := scene_runner("res://Player.tscn")
	runner.set_property("direction", Vector2.RIGHT)
	runner.invoke("_PhysicsProcess",1.0)
	assert_bool(runner.get_property("position")[0] > 0 == true)
	 
func test_player_movement_left():
	var runner := scene_runner("res://Player.tscn")
	runner.set_property("direction", Vector2.LEFT)
	runner.invoke("_PhysicsProcess",1.0)
	assert_bool(runner.get_property("position")[0] < 0 == true)


func test_player_movement_up():
	var runner := scene_runner("res://Player.tscn")
	runner.set_property("direction", Vector2.UP)
	runner.invoke("_PhysicsProcess",1.0)
	assert_bool(runner.get_property("position")[1] < 0 == true)
	
	
	
func test_player_movement_down():
	var runner := scene_runner("res://Player.tscn")
	runner.set_property("direction", Vector2.DOWN)
	runner.invoke("_PhysicsProcess",1.0)
	assert_bool(runner.get_property("position")[1] > 0 == true)
	
	

## Test case: Player Destruction

func test_player_destruction():
	var runner := scene_runner("res://Player.tscn")
	runner.invoke("Destroy")
	assert_bool(runner.get_property("dead")== "true")
	
func test_player_destruction2():
	var runner := scene_runner("res://Player.tscn")
	runner.invoke("Destroy")
	## Simulate animation finished signal
	runner.invoke("OnAnimationFinished")
	assert_bool(runner.is_queued_for_deletion()==true)

## Test case: Player Collision with Monster

