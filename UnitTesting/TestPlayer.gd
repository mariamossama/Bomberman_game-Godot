extends GdUnitTestSuite

func test_destroy() -> void:
	var runner := scene_runner("res://Player.tscn")
	assert_bool(runner != null)
	runner.invoke("Destroy")
	assert_bool(runner.get_property("dead")=="true")

#func test_player_movement_input() -> void:
	#var runner := scene_runner("res://Player.tscn")
	#assert_bool(runner != null)
	#
	## Simulate movement input
	#runner.simulate_action_pressed("ui_up")
	#runner.simulate_action_pressed("ui_right")
#
	#runner.invoke("_PhysicsProcess",0.016) # Assuming 60 FPS, delta time is 1/60
	#var expected_direction = Vector2(1, -1).normalized()
	#var dir= runner.get_property("direction")
	#assert_vector(dir).is_equal(expected_direction)
	#
	


