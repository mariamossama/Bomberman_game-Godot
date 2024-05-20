extends GdUnitTestSuite

# Test Case 1: Initial conditions
func test_initial_conditions() -> void:
	var player := scene_runner("res://Player.tscn")
	assert_bool(player != null) 
	assert_bool(player.get_property("maxBombCount")=="0")



# Test Case 2: Applying power-up increases player's range
func test_apply_power_up_increases_range() -> void:
	var runner := scene_runner("res://Asset/BombIncreasePowerUp.tscn")
	assert_bool(runner != null) 
	var player := scene_runner("res://Player.tscn")
	runner.invoke("ApplyPowerUp",player)
	assert_bool(player.get_property("maxBombCount")=="1")
#
